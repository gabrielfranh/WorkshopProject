using AgendamentoAPI.DTO;

namespace AgendamentoAPI.Repository.Interface
{
    public interface IOficinaRepository
    {
        public Task<List<OficinaDTO>> GetAll();
        public Task<OficinaDTO> GetById(int id);
        public Task<OficinaDTO> Create(OficinaDTO oficinaDTO);
        public Task<OficinaDTO> Update(OficinaDTO oficinaDTO);
        public Task<bool> Delete(int id);
        public Task<OficinaDTO> GetByCnpjSenha(string cnpj, byte[] senhaHash);
    }
}
