using Athena.Dtos;
using Athena.Models;
using AutoMapper;
using System.Collections.Generic;

namespace Athena.Configuration.MapperProfiles
{
    public class TelemetryForwardProfile : Profile
    {
        public TelemetryForwardProfile()
        {
            CreateMap<(Pool, IEnumerable<Telemetry>, DeviceConfiguration), TelemetryForwardDto>()
                .ForMember(d => d.Pool, opt => opt.MapFrom(s => s.Item1))
                .ForMember(d => d.Telemetries, opt => opt.MapFrom(s => s.Item2))
                .ForMember(d => d.Configuration, opt => opt.MapFrom(s => s.Item3));
        }
    }

    internal class TelemetryForward
    {
        internal Pool Pool { get; set; }
        internal IEnumerable<Telemetry> Telemetries { get; set; }
        internal DeviceConfiguration Configuration { get; set; }
    }
}
