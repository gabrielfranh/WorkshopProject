using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using Utils;

namespace FrontEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _clientFactory;

        private Uri _urlOficina = new Uri($"https://localhost:7087/api/Oficina");

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult CriarOficina()
        {
            return View();
        }

        public async void GravarOficina(string txtNome, string txtCnpj, string txtCarga, string txtSenha)
        {
            if (string.IsNullOrEmpty(txtCnpj) || string.IsNullOrEmpty(txtNome) || string.IsNullOrEmpty(txtCarga) || string.IsNullOrEmpty(txtSenha) || !Int32.TryParse(txtCarga, out int carga))
                throw new Exception("Valores nulos");

            if (!Funcoes.ValidaCnpj(txtCnpj))
                throw new Exception("cnpj invalido");

            var hash = Funcoes.GerarHash(txtSenha);

            var oficina = new OficinaDTO()
            {
                Nome = txtNome,
                Cnpj = txtCnpj,
                CargaTrabalhoDiaria = Convert.ToInt32(txtCarga),
                Senha = hash
            };

            var objectJson = JsonConvert.SerializeObject(oficina);

            var httpClient = _clientFactory.CreateClient();
            httpClient.BaseAddress = _urlOficina;
            var response = await httpClient.PostAsJsonAsync(httpClient.BaseAddress, objectJson);
        }

        public IActionResult AgendarServico()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}