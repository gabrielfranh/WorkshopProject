using AgendamentoAPI.DTO;

namespace AgendamentoAPI.Repository.Interface
{
    public interface IAgendamentoRepository
    {
        public Task<List<AgendamentoDTO>> GetAll();

        public Task<AgendamentoDTO> GetById(int id);

        public List<AgendamentoDTO> GetByDate(DateTime data);

        public Task<AgendamentoDTO> Create(AgendamentoDTO AgendamentoDTO);

        public Task<AgendamentoDTO> Update(AgendamentoDTO AgendamentoDTO);

        public Task<bool> Delete(int id);
    }
}
