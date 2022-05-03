using AutoMapper;
using AgendamentoAPI.DTO;
using AgendamentoAPI.Model;

namespace AgendamentoAPI.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<AgendamentoDTO, Agendamento>().ReverseMap();
                config.CreateMap<Oficina, OficinaDTO>().ForMember(x => x.Senha, t => t.Ignore()).ReverseMap();
            });
            return mappingConfig;
        }
    }
}
