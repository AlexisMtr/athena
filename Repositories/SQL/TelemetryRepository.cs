using Athena.Configuration;
using Athena.Models;
using System;

namespace Athena.Repositories.SQL
{
    public class TelemetryRepository : ITelemetryRepository
    {
        private readonly AthenaContext context;

        public TelemetryRepository(AthenaContext context)
        {
            this.context = context;
        }

        public void Add(Telemetry telemetry)
        {
            context.Telemetries.Add(telemetry);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
