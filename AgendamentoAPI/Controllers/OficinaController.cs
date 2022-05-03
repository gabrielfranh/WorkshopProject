using Microsoft.AspNetCore.Mvc;
using AgendamentoAPI.DTO;
using AgendamentoAPI.Business.Interface;
using Utils;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace AgendamentoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OficinaController : ControllerBase
    {
        private readonly IOficinaBusiness _oficinaBusiness;
        private readonly IConfiguration _configuration;

        public OficinaController(IOficinaBusiness oficinaBusiness, IConfiguration configuration)
        {
            _oficinaBusiness = oficinaBusiness;
            _configuration = configuration;
        }

        // GET: api/<OficinaController>
        [HttpGet]
        public async Task<ActionResult<List<OficinaDTO>>> Get()
        {
            var oficinas =  await _oficinaBusiness.GetAll();
            if(oficinas.Any())
                return Ok(oficinas);
            return NoContent();
        }

        // GET api/<OficinaController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OficinaDTO>> GetById(int id)
        {
            var oficina =  await _oficinaBusiness.GetById(id);
            if(oficina == null)
                return NoContent();
            return Ok(oficina);
        }

        // POST api/<OficinaController>
        [HttpPost]
        public async Task<ActionResult<OficinaDTO>> Create([FromBody] OficinaDTO oficinaDTO)
        {
            if (oficinaDTO == null)
                return BadRequest("Não há oficina para o cadastro");
            else if (string.IsNullOrEmpty(oficinaDTO.Nome))
                return BadRequest("Nome inválido");
            else if (string.IsNullOrEmpty(oficinaDTO.Cnpj) && !Funcoes.ValidaCnpj(oficinaDTO.Cnpj))
                return BadRequest("Cnpj inválido");
            else if (!Int32.TryParse(oficinaDTO.CargaTrabalhoDiaria.ToString(), out int carga))
                return BadRequest("Carga de trabalho inválida");
            else if (string.IsNullOrEmpty(oficinaDTO.Senha))
                return BadRequest("Senha inválida");

            var oficina =  await _oficinaBusiness.Create(oficinaDTO);
            return Ok(oficina);
        }
        
        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginDTO loginDTO)
        {
            if (loginDTO == null)
                return BadRequest("Login nulo");
            else if (string.IsNullOrEmpty(loginDTO.Cnpj))
                return BadRequest("Cnpj vazio");
            else if (string.IsNullOrEmpty(loginDTO.Senha))
                return BadRequest("Senha vazia");

            var isLogin = await _oficinaBusiness.Login(loginDTO);
            if (isLogin)
                return Ok(GerarToken(loginDTO));
            else
                return BadRequest("Erro ao logar");
        }

        private string GerarToken(LoginDTO loginDTO)
        {
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var expiry = DateTime.Now.AddMinutes(120);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(issuer: issuer, audience: audience,
            expires: DateTime.Now.AddMinutes(120), signingCredentials: credentials);
            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
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
