FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
ARG GITHUB_PACKAGES_USERNAME
ARG GITHUB_PACKAGES_TOKEN
WORKDIR /src
COPY ["Directory.Build.props", "./"]
COPY ["src/CatalogService/CatalogService.Api/CatalogService.Api.csproj", "src/CatalogService/CatalogService.Api/"]
COPY ["src/CatalogService/CatalogService.Core/CatalogService.Core.csproj", "src/CatalogService/CatalogService.Core/"]
COPY ["src/CatalogService/CatalogService.Domain/CatalogService.Domain.csproj", "src/CatalogService/CatalogService.Domain/"]
COPY ["src/CatalogService/CatalogService.Infrastructure.PostgreSQL/CatalogService.Infrastructure.PostgreSQL.csproj", "src/CatalogService/CatalogService.Infrastructure.PostgreSQL/"]

# Create a temporary nuget.config to authenticate with GitHub Packages
RUN echo "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n\
<configuration>\n\
  <packageSources>\n\
    <add key=\"nuget.org\" value=\"https://api.nuget.org/v3/index.json\" protocolVersion=\"3\" />\n\
    <add key=\"github\" value=\"https://nuget.pkg.github.com/FrMks/index.json\" />\n\
  </packageSources>\n\
  <packageSourceCredentials>\n\
    <github>\n\
      <add key=\"Username\" value=\"$GITHUB_PACKAGES_USERNAME\" />\n\
      <add key=\"ClearTextPassword\" value=\"$GITHUB_PACKAGES_TOKEN\" />\n\
    </github>\n\
  </packageSourceCredentials>\n\
</configuration>" > nuget.config

RUN dotnet restore "src/CatalogService/CatalogService.Api/CatalogService.Api.csproj" --configfile nuget.config

# Remove the nuget.config after restore to keep the image clean
RUN rm nuget.config

COPY . .
WORKDIR "/src/src/CatalogService/CatalogService.Api"
RUN dotnet build "./CatalogService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
WORKDIR "/src/src/CatalogService/CatalogService.Api"
RUN dotnet publish "./CatalogService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "CatalogService.Api.dll"]
