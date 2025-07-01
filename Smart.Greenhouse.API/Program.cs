using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Smart.Greenhouse.API.API.Middleware;
using Smart.Greenhouse.API.Core.Interfaces;
using Smart.Greenhouse.API.Core.Interfaces.Repositories;
using Smart.Greenhouse.API.Core.Services;
using Smart.Greenhouse.API.Infrastructure.Data;
using Smart.Greenhouse.API.Infrastructure.Data.Repositories;
using Smart.Greenhouse.API.Infrastructure.Logging;
using Smart.Greenhouse.API.Infrastructure.Mappings;
using System.Reflection;

// Configure logging
LoggingConfiguration.ConfigureLogging();

try
{
    Log.Information("Starting Smart Greenhouse API");

    var builder = WebApplication.CreateBuilder(args);

    // Add Serilog to the application
    builder.Host.UseSerilog();

    // Add services to the container
    builder.Services.AddControllers();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Smart Greenhouse API",
            Version = "v1",
            Description = "API for Smart Greenhouse monitoring system"
        });

        // Enable XML comments in Swagger
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        if (File.Exists(xmlPath))
        {
            c.IncludeXmlComments(xmlPath);
        }
    });

    // TODO: api deploy edildiğinde dashboard frontend url değiştirilecek
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowSpecificOrigin",
            builder => builder
                .WithOrigins("http://localhost:3000") // Dashboard frontend URL
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
    });

    // Configure DbContext with AWS RDS SQL Server
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<SensorDataContext>(options =>
        options.UseSqlServer(connectionString, sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null);
        }));

    // Configure AutoMapper
    builder.Services.AddAutoMapper(typeof(MappingProfile));

    // Register repositories
    builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    builder.Services.AddScoped<ISensorRepository, SensorRepository>();

    // Register services
    builder.Services.AddScoped<ISensorService, SensorService>();

    var app = builder.Build();

    // Configure the HTTP request pipeline
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Smart Greenhouse API v1");
            c.RoutePrefix = string.Empty; // Set Swagger UI at app root
        });
    }
    else
    {
        // In production, use HTTPS redirection
        app.UseHttpsRedirection();
    }

    // Use global exception handling
    app.UseExceptionMiddleware();

    // Enable CORS
    app.UseCors("AllowSpecificOrigin");

    app.UseAuthorization();

    app.MapControllers();

    using (var scope = app.Services.CreateScope())
    {
        try
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<SensorDataContext>();
            dbContext.Database.Migrate();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred while migrating database");
        }
    }

    app.Run();
}
catch (Exception ex)
{
    LoggingConfiguration.LogUnhandledException(ex);
}
finally
{
    LoggingConfiguration.CloseAndFlushLogger();
}
