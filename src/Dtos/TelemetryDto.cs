using Athena.Models;

namespace Athena.Dtos
{
    public class TelemetryDto
    {
        public TelemetryType Type { get; set; }
        public double Value { get; set; }
        public string Unit { get; set; }
    }
}
