using Microsoft.AspNetCore.Mvc;
using VeterinariaPAW.Services;
using VeterinariaPAW.Models;
using Microsoft.EntityFrameworkCore;

namespace VeterinariaPAW.Controllers
{
    public class DashboardController : Controller
    {
        private readonly DashboardService _service;
        private readonly VeterinariaContext _context;

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
        public IActionResult UltimaActividad()
        {
            var actividades = _context.BitacoraActividad
                .OrderByDescending(a => a.FechaHora)
                .Take(10)
                .ToList();

            return View(actividades);
        }

    }
}
