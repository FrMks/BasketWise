using CatalogService.Api;
using Serilog;
using Serilog.Events;
using Shared.Framework.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProgramDependencies(builder.Configuration);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.Debug()
    .WriteTo.Seq(builder.Configuration.GetConnectionString("Seq")
                 ?? throw new ArgumentNullException("Seq"))
    .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Warning)
    .CreateLogger();

builder.Host.UseSerilog();

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
