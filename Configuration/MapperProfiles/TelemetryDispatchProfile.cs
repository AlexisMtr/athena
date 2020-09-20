using Athena.Dtos;
using Athena.Models;
using AutoMapper;
using System.Collections.Generic;

namespace Athena.Configuration.MapperProfiles
{
    public class TelemetryDispatchProfile : Profile
    {
        public TelemetryDispatchProfile()
        {
            CreateMap<(Pool, IEnumerable<Telemetry>, DeviceConfiguration), TelemetryDispatchDto>()
                .ForMember(d => d.Pool, opt => opt.MapFrom(s => s.Item1))
                .ForMember(d => d.Telemetries, opt => opt.MapFrom(s => s.Item2))
                .ForMember(d => d.Configuration, opt => opt.MapFrom(s => s.Item3));
        }
    }
}
