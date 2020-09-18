using System.Collections.Generic;

namespace Athena.Dtos
{
    public class TelemetriesSetDto
    {
        public string DeviceId { get; set; }
        public IEnumerable<TelemetryDto> Telemetries { get; set; }
        public IDictionary<string, string> Metadata { get; set; }
    }
}
