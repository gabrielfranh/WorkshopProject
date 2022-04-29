using AgendamentoAPI.DTO;
using AgendamentoAPI.Model;
using AgendamentoAPI.Model.Context;
using AgendamentoAPI.Repository.Interface;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AgendamentoAPI.Repository
{
    public class AgendamentoRepository : IAgendamentoRepository
    {
        private readonly MySQLContext _context;
        private readonly IMapper _mapper;

        public AgendamentoRepository(MySQLContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AgendamentoDTO> Create(AgendamentoDTO AgendamentoDTO)
        {
            var agendamento = _mapper.Map<Agendamento>(AgendamentoDTO);
            await _context.Agendamentos.AddAsync(agendamento);
            await _context.SaveChangesAsync();
            return _mapper.Map<AgendamentoDTO>(agendamento);
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var agendamento = await _context.Agendamentos.Where(p => p.Id == id).FirstOrDefaultAsync();

                if (agendamento == null)
                    return false;
                else
                {
                    _context.Agendamentos.Remove(agendamento);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<List<AgendamentoDTO>> GetAll()
        {
            var agendamentos = await _context.Agendamentos.ToListAsync();
            return _mapper.Map<List<AgendamentoDTO>>(agendamentos);
        }

        public async Task<AgendamentoDTO> GetById(int id)
        {
            var agendamento = await _context.Agendamentos.FirstOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<AgendamentoDTO>(agendamento);
        }

        public async  Task<AgendamentoDTO> Update(AgendamentoDTO AgendamentoDTO)
        {
            var agendamento = _mapper.Map<Agendamento>(AgendamentoDTO);
            _context.Agendamentos.Update(agendamento);
            await _context.SaveChangesAsync();
            return _mapper.Map<AgendamentoDTO>(agendamento);
        }
    }
}
