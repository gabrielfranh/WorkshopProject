using AgendamentoAPI.Business.Interface;
using AgendamentoAPI.DTO;
using AgendamentoAPI.Repository.Interface;

namespace AgendamentoAPI.Business
{
    public class OficinaBusiness : IOficinaBusiness
    {
        private readonly IOficinaRepository _oficinaRepository;

        public OficinaBusiness(IOficinaRepository oficinaRepository)
        {
            _oficinaRepository = oficinaRepository;
        }

        public Task<OficinaDTO> Create(OficinaDTO OficinaDTO)
        {
            OficinaDTO.SenhaHash = CreatePasswordHash(OficinaDTO.Senha);
            OficinaDTO.Senha = "";
            return _oficinaRepository.Create(OficinaDTO);
        }

        public Task<bool> Delete(int id)
        {
            return _oficinaRepository.Delete(id);
        }

        public Task<List<OficinaDTO>> GetAll()
        {
            return _oficinaRepository.GetAll();
        }

        public Task<OficinaDTO> GetById(int id)
        {
            return _oficinaRepository.GetById(id);
        }

        public Task<OficinaDTO> Update(OficinaDTO OficinaDTO)
        {
            OficinaDTO.SenhaHash = CreatePasswordHash(OficinaDTO.Senha);
            OficinaDTO.Senha = "";
            return _oficinaRepository.Update(OficinaDTO);
        }

        private byte[] CreatePasswordHash(string password)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(password);
            using (var hash = System.Security.Cryptography.SHA512.Create())
            {
                var hashedInputBytes = hash.ComputeHash(bytes);
                return hashedInputBytes;

            }
        }

        public async Task<bool> Login(LoginDTO loginDTO)
        {
            var senhaHash = CreatePasswordHash(loginDTO.Senha);

            var oficinaLogin = _oficinaRepository.GetByCnpjSenha(loginDTO.Cnpj, senhaHash);

            if (oficinaLogin == null)
                return false;
            return true;
        }
    }
}
