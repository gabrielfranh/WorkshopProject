using AgendamentoAPI.Business.Interface;
using AgendamentoAPI.DTO;
using AgendamentoAPI.Repository.Interface;
using System.Text.Json;

namespace AgendamentoAPI.Business
{
    public class AgendamentoBusiness : IAgendamentoBusiness
    {
        private readonly IAgendamentoRepository _agendamentoRepository;
        private readonly IOficinaRepository _oficinaRepository;

        public AgendamentoBusiness(IAgendamentoRepository agendamentoRepository, IOficinaRepository oficinaRepository)
        {
            _agendamentoRepository = agendamentoRepository;
            _oficinaRepository = oficinaRepository;
        }

        public async Task<AgendamentoDTO> Create(AgendamentoDTO agendamentoDTO)
        {
            // Verificar final de semana
            if (agendamentoDTO.Data.DayOfWeek == DayOfWeek.Saturday || agendamentoDTO.Data.DayOfWeek == DayOfWeek.Sunday)
                throw new Exception();

            var agendamentosNoDia = _agendamentoRepository.GetByDate(agendamentoDTO.Data.Date);

            var unidadesAgendadas = agendamentosNoDia?.Count > 0 ? agendamentosNoDia?.Sum(x => x.UnidadeTrabalhoServico) : 0;

            var oficina = await _oficinaRepository.GetById(agendamentoDTO.OficinaId);

            if (oficina == null)
                throw new Exception();

            if(agendamentoDTO.Data.DayOfWeek == DayOfWeek.Thursday && agendamentoDTO.Data.DayOfWeek == DayOfWeek.Friday)
            {
                var cargaAmpliada = oficina.CargaTrabalhoDiaria + 0.3 * oficina.CargaTrabalhoDiaria;
                if(cargaAmpliada < unidadesAgendadas)
                {
                    throw new Exception();
                }
                
            }
            else if (oficina.CargaTrabalhoDiaria < unidadesAgendadas)
                throw new Exception();

            agendamentoDTO.UnidadeTrabalhoServico = (int)agendamentoDTO.TipoServico;

            return await _agendamentoRepository.Create(agendamentoDTO);
        }

        public async Task<bool> Delete(int id)
        {
            return await _agendamentoRepository.Delete(id);
        }

        public async Task<List<AgendamentoDTO>> GetAll()
        {
                return await _agendamentoRepository.GetAll();
        }

        public async Task<AgendamentoDTO> GetById(int id)
        {
            return await _agendamentoRepository.GetById(id);
        }

        public List<AgendamentoDTO> GetByDate(DateTime date)
        {
            return _agendamentoRepository.GetByDate(date);
        }

        public async Task<AgendamentoDTO> Update(AgendamentoDTO agendamentoDTO)
        {
            if (agendamentoDTO.Data.DayOfWeek == DayOfWeek.Saturday || agendamentoDTO.Data.DayOfWeek == DayOfWeek.Sunday)
                throw new Exception();

            var agendamentosNoDia = _agendamentoRepository.GetByDate(agendamentoDTO.Data.Date);

            var unidadesAgendadas = agendamentosNoDia?.Count > 0 ? agendamentosNoDia?.Sum(x => x.UnidadeTrabalhoServico) : 0;

            var oficina = await _oficinaRepository.GetById(agendamentoDTO.OficinaId);

            if (agendamentoDTO.Data.DayOfWeek == DayOfWeek.Thursday && agendamentoDTO.Data.DayOfWeek == DayOfWeek.Friday)
            {
                var cargaAmpliada = oficina.CargaTrabalhoDiaria + 0.3 * oficina.CargaTrabalhoDiaria;
                if (cargaAmpliada < unidadesAgendadas)
                {
                    throw new Exception();
                }

            }
            else if (oficina.CargaTrabalhoDiaria < unidadesAgendadas)
                throw new Exception();

            return await _agendamentoRepository.Update(agendamentoDTO);
        }

        public async Task<OficinaDTO> OficinaGetById(AgendamentoDTO agendamentoDTO)
        {
            var httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri($"https://localhost:7087/api/Oficina?id={agendamentoDTO.OficinaId}");

            var response = await httpClient.GetAsync(httpClient.BaseAddress);

            response.EnsureSuccessStatusCode();

            string data = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<OficinaDTO>(data);
        }
    }
}
