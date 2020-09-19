using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using AutoMapper;
using Athena.Configuration.MapperProfiles;
using Microsoft.Extensions.DependencyInjection;
using Athena.Configuration;
using Athena.Repositories;
using Athena.Repositories.SQL;
using Athena.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

[assembly: FunctionsStartup(typeof(Athena.Startup))]
namespace Athena
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddLogging(c => c.SetMinimumLevel(LogLevel.Information));

            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddUserSecrets<Startup>()
                .AddEnvironmentVariables()
                .Build();

            builder.Services.AddSingleton(configuration);

            builder.Services.AddDbContext<AthenaContext>(opt
                => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            // or 
            //  => opt.UseInMemoryDatabase(databaseName: "poseidon"));

            builder.Services.AddScoped<IPoolRepository, PoolRepository>();
            builder.Services.AddScoped<ITelemetryRepository, TelemetryRepository>();
            builder.Services.AddScoped<IDeviceConfigurationRepository, DeviceConfigurationRepository>();

            builder.Services.AddScoped<PoolService>();
            builder.Services.AddScoped<TelemetryService>();
            builder.Services.AddScoped<DeviceConfigurationService>();
            builder.Services.AddScoped<ProcessDataService>();

            builder.Services.AddAutoMapper(conf =>
            {
                conf.AddProfile<TelemetryProfile>();
                conf.AddProfile<PoolProfile>();
                conf.AddProfile<DeviceConfigurationProfile>();
                conf.AddProfile<TelemetryForwardProfile>();
            });
        }
    }
}
