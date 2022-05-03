using AgendamentoAPI.Business.Interface;
using AgendamentoAPI.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AgendamentoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendamentoController : ControllerBase
    {
        private readonly IAgendamentoBusiness _agendamentoBusiness;

        public AgendamentoController(IAgendamentoBusiness agendamentoBusiness)
        {
            _agendamentoBusiness = agendamentoBusiness;
        }

        // GET: api/<AgendamentoController>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<AgendamentoDTO>>> GetAll()
        {
            var agendamentos = await _agendamentoBusiness.GetAll();
            if(agendamentos.Any())
                return Ok(agendamentos);
            
            return NoContent();
        }

        // GET api/<AgendamentoController>/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<AgendamentoDTO>> GetById(int id)
        {
            var agendamento = await _agendamentoBusiness.GetById(id);
            if (agendamento == null)
                return NotFound();
            return Ok(agendamento);
        }

        // POST api/<AgendamentoController>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<AgendamentoDTO>> Post([FromBody] AgendamentoDTO agendamentoDTO)
        {
            if (agendamentoDTO == null)
                return BadRequest();

            var agendamento = await _agendamentoBusiness.Create(agendamentoDTO);
            return Ok(agendamento);
        }

        // PUT api/<AgendamentoController>/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<AgendamentoDTO>> Put([FromBody] AgendamentoDTO agendamentoDTO)
        {
            if (agendamentoDTO == null)
                return BadRequest();

            var agendamento = await _agendamentoBusiness.Update(agendamentoDTO);
            return Ok(agendamento);
        }

        // DELETE api/<AgendamentoController>/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            var response = await _agendamentoBusiness.Delete(id);

            if (response)
                return Ok(response);
            else
                return NotFound();
        }
    }
}
