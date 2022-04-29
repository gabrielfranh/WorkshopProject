using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OficinasAPI.DTO;
using OficinasAPI.Model;
using OficinasAPI.Model.Context;
using OficinasAPI.Repository.Interface;

namespace OficinasAPI.Repository
{
    public class OficinaRepository : IOficinaRepository
    {
        private readonly MySQLContext _context;
        private readonly IMapper _mapper;

        public OficinaRepository(MySQLContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OficinaDTO> Create(OficinaDTO oficinaDTO)
        {
            var oficina = _mapper.Map<Oficina>(oficinaDTO);
            await _context.Oficinas.AddAsync(oficina);
            await _context.SaveChangesAsync();
            return _mapper.Map<OficinaDTO>(oficina);
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var oficina = await _context.Oficinas.Where(p => p.Id == id).FirstOrDefaultAsync();

                if (oficina == null)
                    return false;
                else
                {
                    _context.Oficinas.Remove(oficina);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<List<OficinaDTO>> GetAll()
        {
            var oficinas = await _context.Oficinas.ToListAsync();
            return _mapper.Map<List<OficinaDTO>>(oficinas);
        }

        public async Task<OficinaDTO> GetById(int id)
        {
            var oficina = await _context.Oficinas.FirstOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<OficinaDTO>(oficina);
        }

        public async Task<OficinaDTO> Update(OficinaDTO oficinaDTO)
        {
            var oficina = _mapper.Map<Oficina>(oficinaDTO);
            _context.Oficinas.Update(oficina);
            await _context.SaveChangesAsync();
            return _mapper.Map<OficinaDTO>(oficina);
        }
    }
}
