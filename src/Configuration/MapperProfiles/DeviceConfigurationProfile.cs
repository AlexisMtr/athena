using System;

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

            CreateMap<DeviceConfigurationDto, DeviceConfiguration>()
                .ForMember(d => d.PublicationDelay, opt => opt.MapFrom(s => TimeSpan.FromSeconds(s.PublicationDelay)))
                .ForMember(d => d.ConfigurationUpdateCheckDelay, opt => opt.MapFrom(s => TimeSpan.FromSeconds(s.ConfigurationUpdateCheckDelay)))
                .ForMember(d => d.SleepModeStart, opt => opt.MapFrom(s => DateTimeOffset.FromUnixTimeSeconds(s.PublicationDelay)));
        }
    }
}
