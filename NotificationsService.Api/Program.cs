using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NotificationsService.Api.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Products API",
        Version = "v1",
        Description = "API para administrar productos con autenticacion JWT y base de datos MySQL en Docker."
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingresa el token JWT obtenido en /api/Auth/login."
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            new string[] { }
        }
    });

});

var connectionString = builder.Configuration.GetConnectionString("MySql")
    ?? "Server=localhost;Port=3306;Database=productsdb;User=productsuser;Password=productspass;";

builder.Services.AddDbContext<ProductDbContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 4, 0))));

var jwtKey = builder.Configuration["Jwt:Key"] ?? "exam-products-api-secret-key-change-me";
var issuer = builder.Configuration["Jwt:Issuer"] ?? "ProductsApi";
var audience = builder.Configuration["Jwt:Audience"] ?? "ProductsApiUsers";

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

await EnsureDatabaseCreatedAsync(app);

app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Products API v1");
});

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

static async Task EnsureDatabaseCreatedAsync(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    for (var attempt = 1; attempt <= 10; attempt++)
    {
        try
        {
            await dbContext.Database.EnsureCreatedAsync();
            logger.LogInformation("Base de datos MySQL lista.");
            return;
        }
        catch (Exception ex) when (attempt < 10)
        {
            logger.LogWarning(ex, "MySQL aun no esta listo. Reintento {Attempt}/10.", attempt);
            await Task.Delay(TimeSpan.FromSeconds(5));
        }
    }

    await dbContext.Database.EnsureCreatedAsync();
}
