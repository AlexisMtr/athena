using AutoMapper;
using AutoMapper.Configuration;
using Athena.Dtos;
using Athena.Models;
using System.Collections.Generic;

namespace Athena.Configuration
{
    public class MapperConfiguration
    {
        private static bool isInitialized;
        public static void ConfigureMapper()
        {
            if (isInitialized) return;

            MapperConfigurationExpression config = new MapperConfigurationExpression();

            config.CreateMap<TelemetryDto, Telemetry>()
                .ForMember(d => d.Pool, opt => opt.Ignore());

            Mapper.Initialize(config);
            isInitialized = true;
        }
    }
}
