using AgendamentoAPI.DTO;
using AgendamentoAPI.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AgendamentoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendamentoController : ControllerBase
    {
        private readonly IAgendamentoRepository _repository;

        public AgendamentoController(IAgendamentoRepository repository)
        {
            _repository = repository;
        }

        // GET: api/<AgendamentoController>
        [HttpGet]
        public async Task<ActionResult<List<AgendamentoDTO>>> Get()
        {
            var agendamentos = await _repository.GetAll();
            if(agendamentos.Any())
                return Ok(agendamentos);
            
            return NoContent();
        }

        // GET api/<AgendamentoController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AgendamentoDTO>> GetById(int id)
        {
            var agendamento = await _repository.GetById(id);
            if (agendamento == null)
                return NotFound();
            return Ok(agendamento);
        }

        // POST api/<AgendamentoController>
        [HttpPost]
        public async Task<ActionResult<AgendamentoDTO>> Post([FromBody] AgendamentoDTO agendamentoDTO)
        {
            if (agendamentoDTO == null)
                return BadRequest();

            var agendamento = await _repository.Create(agendamentoDTO);
            return Ok(agendamento);
        }

        // PUT api/<AgendamentoController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<AgendamentoDTO>> Put([FromBody] AgendamentoDTO agendamentoDTO)
        {
            if (agendamentoDTO == null)
                return BadRequest();

            var agendamento = await _repository.Update(agendamentoDTO);
            return Ok(agendamento);
        }

        // DELETE api/<AgendamentoController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var response = await _repository.Delete(id);

            if (response)
                return Ok(response);
            else
                return NotFound();
        }
    }
}
