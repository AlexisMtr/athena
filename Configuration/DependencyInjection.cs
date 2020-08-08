using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Athena.Repositories;
using Athena.Repositories.SQL;
using Athena.Services;
using System;

namespace Athena.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceProvider ServiceProvider { get; set; }

        public static void InitializeContainer(ILogger logger)
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging();

            serviceCollection.AddSingleton(typeof(ILogger), logger);

            string dbConnectionString = Environment.GetEnvironmentVariable("ConnectionStrings:DefaultConnection");
            serviceCollection.AddDbContext<AthenaContext>(opt => opt.UseSqlServer(dbConnectionString));

            serviceCollection.AddScoped<IPoolRepository, PoolRepository>();
            serviceCollection.AddScoped<IAlarmRepository, AlarmRepository>();
            serviceCollection.AddScoped<ITelemetryRepository, TelemetryRepository>();
            serviceCollection.AddScoped<IDeviceConfigurationRepository, DeviceConfigurationRepository>();

            serviceCollection.AddScoped<PoolService>();
            serviceCollection.AddScoped<AlarmService>();
            serviceCollection.AddScoped<TelemetryService>();
            serviceCollection.AddScoped<DeviceConfigurationService>();
            serviceCollection.AddScoped<ProcessDataService>();

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}
