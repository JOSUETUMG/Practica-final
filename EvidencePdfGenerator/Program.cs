using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

var rootPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));
var outputPath = Path.Combine(rootPath, "EVIDENCIA_EXAMEN.pdf");
var markdownPath = Path.Combine(rootPath, "EVIDENCIA_EXAMEN.md");
var imageFolder = Path.Combine(rootPath, "evidence_images");

bool FileExists(string filePath) => File.Exists(filePath);

var markdownText = File.Exists(markdownPath)
    ? File.ReadAllText(markdownPath)
    : "No se encontró el archivo EVIDENCIA_EXAMEN.md en el directorio del proyecto.";

QuestPDF.Settings.License = LicenseType.Community;

var document = Document.Create(container =>
{
    container.Page(page =>
    {
        page.Size(PageSizes.A4);
        page.Margin(20);
        page.PageColor(Colors.White);
        page.DefaultTextStyle(x => x.FontSize(11).FontColor(Colors.Black));

        page.Content().Column(column =>
        {
            column.Spacing(10);
            column.Item().Text("EVIDENCIA EXAMEN").FontSize(24).Bold();
            column.Item().Text($"Fecha de generación: {DateTime.Now:yyyy-MM-dd HH:mm:ss}").FontSize(10).FontColor(Colors.Grey.Darken1);
            column.Item().Text("Documento de evidencia de Swagger, MySQL en Docker y autenticación JWT.").FontSize(12);
            column.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten2);
            column.Item().Text("1. Contenido del archivo EVIDENCIA_EXAMEN.md").FontSize(16).Bold();
            column.Item().Text(markdownText).FontSize(10).LineHeight(1.2f);

            column.Item().Text("2. Capturas de evidencia").FontSize(16).Bold();

            AddImageSection(column, "Swagger UI", Path.Combine(imageFolder, "swagger_ui.png"));
            AddImageSection(column, "Docker Compose ps", Path.Combine(imageFolder, "evidencia_docker_ps.png"));
            AddImageSection(column, "MySQL ping", Path.Combine(imageFolder, "evidencia_mysql_ping.png"));
            AddImageSection(column, "Respuesta login JWT", Path.Combine(imageFolder, "evidencia_login_response.png"));
            AddImageSection(column, "Respuesta GET /api/Products", Path.Combine(imageFolder, "evidencia_products.png"));
        });
    });
});

document.GeneratePdf(outputPath);
Console.WriteLine($"PDF generado con éxito en: {outputPath}");

void AddImageSection(QuestPDF.Fluent.ColumnDescriptor column, string title, string imagePath)
{
    column.Item().Text(title).FontSize(14).SemiBold();

    if (FileExists(imagePath))
    {
        column.Item().Image(imagePath).FitArea();
    }
    else
    {
        column.Item().Text($"No se encontró la imagen: {imagePath}").FontColor(Colors.Red.Medium).FontSize(10);
    }
}

