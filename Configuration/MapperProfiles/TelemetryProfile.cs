using Athena.Dtos;
using Athena.Models;
using AutoMapper;

namespace Athena.Configuration.MapperProfiles
{
    public class TelemetryProfile : Profile
    {
        public TelemetryProfile()
        {
            CreateMap<TelemetryDto, Telemetry>()
                .ForMember(d => d.Pool, opt => opt.Ignore());
            CreateMap<Telemetry, TelemetryDto>();
        }
    }
}
