using Athena.Models;
using Athena.Repositories;

namespace Athena.Services
{
    public class TelemetryService
    {
        private readonly ITelemetryRepository telemetryRepository;

        public TelemetryService(ITelemetryRepository telemetryRepository)
        {
            this.telemetryRepository = telemetryRepository;
        }

        public Telemetry Add(Telemetry telemetry)
        {
            telemetryRepository.Add(telemetry);
            telemetryRepository.SaveChanges();

            return telemetry;
        }
    }
}
