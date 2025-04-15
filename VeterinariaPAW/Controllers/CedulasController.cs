using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VeterinariaPAW.Models;
using VeterinariaPAW.Services;

namespace VeterinariaPAW.Controllers
{
    public class CedulasController : Controller
    {
        private readonly GometaApiService _gometaService;

        public CedulasController(GometaApiService gometaService)
        {
            _gometaService = gometaService;
        }

        [HttpGet]
        public IActionResult Buscar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Buscar(string cedula)
        {
            var resultado = await _gometaService.BuscarCedula(cedula);
            return View("Resultado", resultado);
        }
    }
}
