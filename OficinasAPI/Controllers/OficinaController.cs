using Microsoft.AspNetCore.Mvc;
using OficinasAPI.DTO;
using OficinasAPI.Business.Interface;

namespace OficinasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OficinaController : ControllerBase
    {
        private readonly IOficinaBusiness _oficinaBusiness;

        public OficinaController(IOficinaBusiness oficinaBusiness)
        {
            _oficinaBusiness = oficinaBusiness;
        }

        // GET: api/<OficinaController>
        [HttpGet]
        public async Task<ActionResult<List<OficinaDTO>>> Get()
        {
            var oficinas =  await _oficinaBusiness.GetAll();
            if(oficinas.Any())
                return Ok(oficinas);
            return NotFound();
        }

        // GET api/<OficinaController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OficinaDTO>> GetById(int id)
        {
            var oficina =  await _oficinaBusiness.GetById(id);
            if(oficina == null)
                return NotFound();
            return Ok(oficina);
        }

        // POST api/<OficinaController>
        [HttpPost]
        public async Task<ActionResult<OficinaDTO>> Post([FromBody] OficinaDTO oficinaDTO)
        {
            if (oficinaDTO == null)
                return BadRequest();

            var oficina =  await _oficinaBusiness.Create(oficinaDTO);
            return Ok(oficina);
        }

        // PUT api/<OficinaController>/5
        [HttpPut]
        public async Task<ActionResult<OficinaDTO>> Put([FromBody] OficinaDTO oficinaDTO)
        {
            if (oficinaDTO == null)
                return BadRequest();

            var oficina =  await _oficinaBusiness.Update(oficinaDTO);
            return Ok(oficina);
        }

        // DELETE api/<OficinaController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var response = await _oficinaBusiness.Delete(id);

            if (response)
                return Ok(response);
            else
                return NotFound();
        }
    }
}
