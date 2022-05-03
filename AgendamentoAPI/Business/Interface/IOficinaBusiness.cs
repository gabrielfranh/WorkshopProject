using AgendamentoAPI.DTO;

namespace AgendamentoAPI.Business.Interface
{
    public interface IOficinaBusiness
    {
        public Task<List<OficinaDTO>> GetAll();
        public Task<OficinaDTO> GetById(int id);
        public Task<OficinaDTO> Create(OficinaDTO OficinaDTO);
        public Task<OficinaDTO> Update(OficinaDTO OficinaDTO);
        public Task<bool> Delete(int id);
        public Task<bool> Login(LoginDTO loginDTO);
    }
}
