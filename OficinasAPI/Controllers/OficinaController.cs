using Microsoft.AspNetCore.Mvc;
using OficinasAPI.DTO;
using OficinasAPI.Repository.Interface;

namespace OficinasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OficinaController : ControllerBase
    {
        private readonly IOficinaRepository _oficinaRepository;

        public OficinaController(IOficinaRepository oficinaRepository)
        {
            _oficinaRepository = oficinaRepository;
        }

        // GET: api/<OficinaController>
        [HttpGet]
        public async Task<ActionResult<List<OficinaDTO>>> Get()
        {
            var oficinas =  await _oficinaRepository.GetAll();
            if(oficinas.Any())
                return Ok(oficinas);
            return NotFound();
        }

        // GET api/<OficinaController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OficinaDTO>> GetById(int id)
        {
            var oficina =  await _oficinaRepository.GetById(id);
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

            var oficina =  await _oficinaRepository.Create(oficinaDTO);
            return Ok(oficina);
        }

        // PUT api/<OficinaController>/5
        [HttpPut]
        public async Task<ActionResult<OficinaDTO>> Put([FromBody] OficinaDTO oficinaDTO)
        {
            if (oficinaDTO == null)
                return BadRequest();

            var oficina =  await _oficinaRepository.Update(oficinaDTO);
            return Ok(oficina);
        }

        // DELETE api/<OficinaController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var response = await _oficinaRepository.Delete(id);

            if (response)
                return Ok(response);
            else
                return NotFound();
        }
    }
}
