using Athena.Dtos;
using Athena.Models;
using AutoMapper;

namespace Athena.Configuration.MapperProfiles
{
    public class PoolProfile : Profile
    {
        public PoolProfile()
        {
            CreateMap<Pool, PoolDto>();
        }
    }
}
