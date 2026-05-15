using CatalogService.Api;
using CatalogService.Core.Database;
using CatalogService.Infrastructure.PostgreSQL;
using Serilog;
using Serilog.Events;
using Shared.Core.Database;
using Shared.Framework.Middlewares;

var builder = WebApplication.CreateBuilder(args);

if (File.Exists(".env"))
{
    foreach (var line in File.ReadAllLines(".env"))
    {
        var parts = line.Split('=', 2);
        if (parts.Length == 2)
            Environment.SetEnvironmentVariable(parts[0], parts[1]);
    }
}

var seqUrl = Environment.GetEnvironmentVariable("Seq")
    ?? throw new ArgumentException("Seq missing");
var connectionString = Environment.GetEnvironmentVariable("CatalogServiceDb")
    ?? throw new ArgumentException("CatalogServiceDb environment variable is missing");

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.Seq(seqUrl)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddProgramDependencies(builder.Configuration);

builder.Services.AddScoped<CatalogServiceDbContext>(_ =>
    new CatalogServiceDbContext(connectionString));
builder.Services.AddScoped<IReadDbContext, CatalogServiceDbContext>(_ =>
    new CatalogServiceDbContext(connectionString));
builder.Services.AddScoped<IDbConnectionFactory, CatalogServiceDbContext>(_ =>
    new CatalogServiceDbContext(connectionString));

var app = builder.Build();

app.UseSharedExceptionHandling();

app.UseHttpLogging();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "CatalogService"));
}

app.UseSerilogRequestLogging();

app.UseCors(DependencyInjection.GetClientCorsPolicyName());

app.MapControllers();

app.Run();

namespace CatalogService.Api
{
    public partial class Program;
}
