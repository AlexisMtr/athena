using System.Collections.Generic;

namespace Athena.Dtos
{
    public class TelemetriesSetDto
    {
        public IEnumerable<TelemetryDto> Telemetries { get; set; }
    }
}
