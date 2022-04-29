using AutoMapper;
using OficinasAPI.DTO;
using OficinasAPI.Model;

namespace OficinasAPI.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<OficinaDTO, Oficina>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
