using Serilog;
using Serilog.Events;
using Serilog.Filters;
using System;

namespace Smart.Greenhouse.API.Infrastructure.Logging
{
    /// <summary>
    /// Configure Serilog for application logging
    /// </summary>
    public static class LoggingConfiguration
    {
        /// <summary>
        /// Configure Serilog for the application
        /// </summary>
        public static void ConfigureLogging()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
                // Filter out specific log messages
                .Filter.ByExcluding(Matching.WithProperty<string>("EventId.Name", p => p == "ApplicationStarting"))
                .Filter.ByExcluding(e => e.MessageTemplate.Text.Contains("Content root path:"))
                .Enrich.FromLogContext()
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.File(
                    path: "logs/greenhouse-.log",
                    rollingInterval: RollingInterval.Day,
                    fileSizeLimitBytes: 10 * 1024 * 1024,
                    retainedFileCountLimit: 31,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
                )
                .CreateLogger();
        }

        /// <summary>
        /// Log unhandled exceptions during application startup
        /// </summary>
        /// <param name="ex">The exception that occurred</param>
        public static void LogUnhandledException(Exception ex)
        {
            Log.Fatal(ex, "Application terminated unexpectedly");
        }

        /// <summary>
        /// Clean up and close the logger
        /// </summary>
        public static void CloseAndFlushLogger()
        {
            Log.CloseAndFlush();
        }
    }
} 