using AgendamentoAPI.Business.Interface;
using AgendamentoAPI.DTO;
using AgendamentoAPI.Repository.Interface;
using System.Text.Json;
using Utils;

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
                throw new Exception("Agendamento apenas para dias úteis");

            var agendamentosNoDia = _agendamentoRepository.GetByDate(agendamentoDTO.Data.Date);

            var unidadesAgendadas = agendamentosNoDia?.Count > 0 ? agendamentosNoDia?.Sum(x => x.UnidadeTrabalhoServico) : 0;

            agendamentoDTO.UnidadeTrabalhoServico = (int)Enum.Parse(typeof(TipoServicoEnum), agendamentoDTO.TipoServico);

            var oficina = await _oficinaRepository.GetById(agendamentoDTO.OficinaId);

            if (oficina == null)
                throw new Exception($"Oficina com id {agendamentoDTO.OficinaId} não existe");

            if(agendamentoDTO.Data.DayOfWeek == DayOfWeek.Thursday || agendamentoDTO.Data.DayOfWeek == DayOfWeek.Friday)
            {
                var cargaAmpliada = oficina.CargaTrabalhoDiaria + 0.3 * oficina.CargaTrabalhoDiaria;
                if(cargaAmpliada < unidadesAgendadas + agendamentoDTO.UnidadeTrabalhoServico)
                {
                    throw new Exception("Carga de trabalho deste dia foi excedida");
                }
                
            }
            else if (oficina.CargaTrabalhoDiaria < unidadesAgendadas + agendamentoDTO.UnidadeTrabalhoServico)
                throw new Exception("Carga de trabalho deste dia foi excedida");

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

        public async Task<List<AgendamentoDTO>> GetAgendamentosByDate(DateTime datainicial, DateTime dataFinal)
        {
            return await _agendamentoRepository.GetAgendamentosByDate(datainicial, dataFinal);
        }

        public List<AgendamentoDTO> GetByDate(DateTime date)
        {
            return _agendamentoRepository.GetByDate(date);
        }

        public async Task<AgendamentoDTO> Update(AgendamentoDTO agendamentoDTO)
        {
            if (agendamentoDTO.Data.DayOfWeek == DayOfWeek.Saturday || agendamentoDTO.Data.DayOfWeek == DayOfWeek.Sunday)
                throw new Exception("Agendamento apenas para dias úteis");

            var agendamentosNoDia = _agendamentoRepository.GetByDate(agendamentoDTO.Data.Date);

            var unidadesAgendadas = agendamentosNoDia?.Count > 0 ? agendamentosNoDia?.Sum(x => x.UnidadeTrabalhoServico) : 0;

            var oficina = await _oficinaRepository.GetById(agendamentoDTO.OficinaId);

            agendamentoDTO.UnidadeTrabalhoServico = (int)Enum.Parse(typeof(TipoServicoEnum), agendamentoDTO.TipoServico);

            if (oficina == null)
                throw new Exception($"Oficina com id {agendamentoDTO.OficinaId} não existe");

            if (agendamentoDTO.Data.DayOfWeek == DayOfWeek.Thursday || agendamentoDTO.Data.DayOfWeek == DayOfWeek.Friday)
            {
                var cargaAmpliada = oficina.CargaTrabalhoDiaria + 0.3 * oficina.CargaTrabalhoDiaria;
                if (cargaAmpliada < unidadesAgendadas + agendamentoDTO.UnidadeTrabalhoServico)
                {
                    throw new Exception("Carga de trabalho deste dia foi excedida");
                }

            }
            else if (oficina.CargaTrabalhoDiaria < unidadesAgendadas + agendamentoDTO.UnidadeTrabalhoServico)
                throw new Exception("Carga de trabalho deste dia foi excedida");

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
