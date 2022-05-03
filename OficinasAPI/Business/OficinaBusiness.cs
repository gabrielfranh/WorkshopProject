using OficinasAPI.Business.Interface;
using OficinasAPI.DTO;
using OficinasAPI.Repository.Interface;

namespace OficinasAPI.Business
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
           return _oficinaRepository.Update(OficinaDTO);
        }
    }
}
