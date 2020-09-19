using System.Collections.Generic;

namespace Athena.Dtos
{
    public class TelemetryForwardDto
    {
        public IEnumerable<TelemetryDto> Telemetries { get; set; }
        public PoolDto Pool { get; set; }
        public DeviceConfigurationDto Configuration { get; set; }
    }
}
