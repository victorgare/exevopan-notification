
using ExevopanNotification.CrossCutting.Extensions;
using ExevopanNotification.Domain.Config;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // configure app variables
        builder.Configuration.AddEnvironmentVariables();
        builder.Services.Configure<ApplicationConfig>(builder.Configuration);

        // Add services to the container.
        ConfigureServices(builder.Services);

        // Configure DI
        builder.Services.AddDependencies(builder.Configuration);

        // configure HealthCheck
        builder.Services.AddCustomHealthCheck(builder.Configuration);

        // configure cron jobs
        builder.Services.AddCustomCronJob();

        // disable model bind validation
        builder.Services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        builder.Services.AddMemoryCache();
        var app = builder.Build();

        // add shutdown handler
        app.UseCustomShutdownHandler();

        // use custom swagger
        app.UseCustomSwagger();

        app.UseHttpsRedirection();

        app.MapControllers();

        // use custom HealthCheck
        app.UseCustomHealthCheck();

        app.Run();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        });


        // add swagger
        services.AddCustomSwagger();
    }
}

