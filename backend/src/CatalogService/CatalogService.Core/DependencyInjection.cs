using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Core.Abstractions;

namespace CatalogService.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = typeof(DependencyInjection).Assembly;
        var redisConnectionString = Environment.GetEnvironmentVariable("Redis")
            ?? throw new ArgumentException("Redis connection string is missig");

        services.Scan(scan => scan.FromAssemblies(assembly)
            .AddClasses(classes => classes
                .AssignableToAny(typeof(ICommandHandler<,>), typeof(ICommandHandler<>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());

        services.Scan(scan => scan.FromAssemblies(assembly)
            .AddClasses(classes => classes
                .AssignableToAny(typeof(IQueryHandler<,>), typeof(IQueryHandler<>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());

        services.AddStackExchangeRedisCache(setup =>
        {
            setup.Configuration = redisConnectionString;
        });

        services.AddHybridCache(options =>
        {
            options.DefaultEntryOptions = new HybridCacheEntryOptions
            {
                LocalCacheExpiration = TimeSpan.FromMinutes(5),
                Expiration = TimeSpan.FromMinutes(5),
            };
        });

        return services;
    }
}
