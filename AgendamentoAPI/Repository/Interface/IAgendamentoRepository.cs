using AgendamentoAPI.DTO;

namespace AgendamentoAPI.Repository.Interface
{
    public interface IAgendamentoRepository
    {
        public Task<List<AgendamentoDTO>> GetAll();
        public Task<List<string>> GetServicosDia();
        public Task<List<AgendamentoDTO>> GetAgendamentosByDate(DateTime datainicial, DateTime dataFinal);
        public List<AgendamentoDTO> GetByDate(DateTime data);
        public Task<AgendamentoDTO> Create(AgendamentoDTO AgendamentoDTO);
        public Task<AgendamentoDTO> Update(AgendamentoDTO AgendamentoDTO);
        public Task<bool> Delete(int id);
    }
}
