using AgendamentoAPI.DTO;
using AgendamentoAPI.Model;
using AgendamentoAPI.Model.Context;
using AgendamentoAPI.Repository.Interface;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

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

        public async Task<List<AgendamentoDTO>> GetAgendamentosByDate(DateTime datainicial, DateTime dataFinal)
        {
            var agendamentosFiltrados = await _context.Agendamentos.Where(x => x.Data.Date >= datainicial.Date && x.Data.Date <= dataFinal.Date).ToListAsync();
            return _mapper.Map<List<AgendamentoDTO>>(agendamentosFiltrados);
        }

        public List<AgendamentoDTO> GetByDate(DateTime data)
        {
            var agendamento = _context.Agendamentos.Where(x => x.Data.Date == data.Date).ToList();
            return _mapper.Map<List<AgendamentoDTO>>(agendamento);
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
