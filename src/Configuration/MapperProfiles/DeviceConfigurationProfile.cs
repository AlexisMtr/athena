using Athena.Dtos;
using Athena.Models;
using AutoMapper;

namespace Athena.Configuration.MapperProfiles
{
    public class DeviceConfigurationProfile : Profile
    {
        public DeviceConfigurationProfile()
        {
            CreateMap<DeviceConfiguration, DeviceConfigurationDto>()
                .ForMember(d => d.SleepModeStart, opt => opt.MapFrom(s => s.SleepModeStart.ToUnixTimeSeconds()))
                .ForMember(d => d.PublicationDelay, opt => opt.MapFrom(s => s.PublicationDelay.TotalSeconds))
                .ForMember(d => d.ConfigurationUpdateCheckDelay, opt => opt.MapFrom(s => s.ConfigurationUpdateCheckDelay.TotalSeconds));
        }
    }
}
