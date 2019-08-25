﻿using System.Threading.Tasks;
using Rx.Http.Samples.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging.Debug;
using Microsoft.Extensions.DependencyInjection;
using Rx.Http.Samples.Consumers;

namespace Rx.Http.Samples
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            // Get an instance of the service
            // ILogger instance will be injected by dependency injection
            // Note : if no DI is setup, MyService will work because ILogger is null-checked (ie _logger?.Info(msg))
            var example = serviceProvider.GetService<Example>();

            await example.Execute();

        }

        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddLogging(config =>
            {
                config.AddDebug(); // Log to debug (debug window in Visual Studio or any debugger attached)
                config.AddConsole(); // Log to console (colored !)
            })
            .Configure<LoggerFilterOptions>(options =>
            {
                options.AddFilter<DebugLoggerProvider>(null /* category*/ , LogLevel.Information /* min level */);
                options.AddFilter<ConsoleLoggerProvider>(null  /* category*/ , LogLevel.Information /* min level */);
            })
            .AddTransient<Example>()
            .AddTransient<TheMovieDatabaseConsumer>();

        }

    }
}