using Athena.Models;

namespace Athena.Repositories
{
    public interface ITelemetryRepository
    {
        void Add(Telemetry telemetry);
        void SaveChanges();
    }
}
