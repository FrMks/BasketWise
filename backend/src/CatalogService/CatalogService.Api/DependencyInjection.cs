using CatalogService.Core;
using Microsoft.OpenApi;
using Shared;

namespace CatalogService.Api;

public static class DependencyInjection
{
    private const string CLIENT_CORS_POLICY = "ClientCorsPolicy";
    
    public static IServiceCollection AddProgramDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        var allowedOrigins = configuration.GetSection("AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();

        return services
            .AddWebDependencies(configuration)
            .AddCore(configuration)
            .AddCors(options =>
            {
                options.AddPolicy(CLIENT_CORS_POLICY, policy =>
                {
                    policy
                        .WithOrigins(allowedOrigins)
                        .AllowCredentials()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
    }

    public static string GetClientCorsPolicyName() => CLIENT_CORS_POLICY;

    private static IServiceCollection AddWebDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        var swaggerServerUrl = configuration["Swagger:ServerUrl"];

        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.IncludeFields = true;
            });

        services.AddHttpLogging();

        services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer((document, _, _) =>
            {
                if (!string.IsNullOrWhiteSpace(swaggerServerUrl))
                {
                    document.Servers =
                    [
                        new OpenApiServer
                        {
                            Url = swaggerServerUrl,
                        },
                    ];
                }

                return Task.CompletedTask;
            });
        });

        return services;
    }
}
