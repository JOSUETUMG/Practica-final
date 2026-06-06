FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["NotificationsService.Api.csproj", "./"]
RUN dotnet restore "NotificationsService.Api.csproj"

COPY . .
RUN dotnet publish "NotificationsService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80
ENTRYPOINT ["dotnet", "NotificationsService.Api.dll"]
