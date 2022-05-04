using AgendamentoAPI.Business.Interface;
using AgendamentoAPI.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<AgendamentoDTO>>> GetAll()
        {
            var agendamentos = await _agendamentoBusiness.GetAll();
            if(agendamentos.Any())
                return Ok(agendamentos);
            
            return NoContent();
        }

        [HttpPost("/Data")]
        [Authorize]
        public async Task<ActionResult<List<AgendamentoDTO>>> AgendamentosByDate([FromBody] FiltroDataDTO datas)
        {
            if (datas == null)
                return BadRequest("Datas não informadas");
            else if (datas.DataFinal < datas.DataInicial)
                return BadRequest("Data final menor que a data inicial");

            var agendamentosFiltrados = await _agendamentoBusiness.GetAgendamentosByDate(datas.DataInicial, datas.DataFinal);
            if (agendamentosFiltrados == null)
                return NotFound();

            return Ok(agendamentosFiltrados);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<AgendamentoDTO>> Post([FromBody] AgendamentoDTO agendamentoDTO)
        {
            if (agendamentoDTO == null)
                return BadRequest("Agendamento vazio");

            var agendamento = await _agendamentoBusiness.Create(agendamentoDTO);
            return Ok(agendamento);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<AgendamentoDTO>> Put([FromBody] AgendamentoDTO agendamentoDTO)
        {
            if (agendamentoDTO == null)
                return BadRequest("Agendamento vazio");

            var agendamento = await _agendamentoBusiness.Update(agendamentoDTO);
            return Ok(agendamento);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            var response = await _agendamentoBusiness.Delete(id);

            if (response)
                return Ok(response);
            else
                return NotFound("Não foi possível deletar");
        }
    }
}
