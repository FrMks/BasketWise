FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
ARG GITHUB_PACKAGES_USERNAME
ARG GITHUB_PACKAGES_TOKEN
WORKDIR /src
COPY ["src/CatalogService/CatalogService.Api/CatalogService.Api.csproj", "src/CatalogService/CatalogService.Api/"]
COPY ["src/CatalogService/CatalogService.Core/CatalogService.Core.csproj", "src/CatalogService/CatalogService.Core/"]
COPY ["src/CatalogService/CatalogService.Domain/CatalogService.Domain.csproj", "src/CatalogService/CatalogService.Domain/"]
COPY ["src/CatalogService/CatalogService.Infrastructure.PostgreSQL/CatalogService.Infrastructure.PostgreSQL.csproj", "src/CatalogService/CatalogService.Infrastructure.PostgreSQL/"]
RUN dotnet nuget add source "https://nuget.pkg.github.com/FrMks/index.json" \
    --name github \
    --username "$GITHUB_PACKAGES_USERNAME" \
    --password "$GITHUB_PACKAGES_TOKEN" \
    --store-password-in-clear-text
RUN dotnet restore "src/CatalogService/CatalogService.Api/CatalogService.Api.csproj"
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
