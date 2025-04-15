using Microsoft.AspNetCore.Mvc;
using VeterinariaPAW.Services;
using VeterinariaPAW.Models;

namespace VeterinariaPAW.Controllers
{
    public class DashboardController : Controller
    {
        private readonly DashboardService _service;

        public DashboardController(VeterinariaContext context)
        {
            _service = new DashboardService(context);
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ObtenerMetricas()
        {
            var datos = _service.ObtenerDatos();
            return Json(datos);
        }
    }
}
