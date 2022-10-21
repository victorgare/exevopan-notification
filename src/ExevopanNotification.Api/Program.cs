
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

        // configure hosted services
        builder.Services.AddCustomHostedService();

        // disable model bind validation
        builder.Services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            // use custom swagger
            app.UseCustomSwagger();
        }

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
//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();
