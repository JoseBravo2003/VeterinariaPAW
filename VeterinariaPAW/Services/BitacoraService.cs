using System;
using System.Threading.Tasks;
using VeterinariaPAW.Models;

namespace VeterinariaPAW.Services
{
    public class BitacoraService
    {
        private readonly VeterinariaContext _context;

        public BitacoraService(VeterinariaContext context)
        {
            _context = context;
        }

        public async Task Registrar(string usuario, string accion, string controlador, string vista)
        {
            var entrada = new BitacoraActividad
            {
                Usuario = usuario ?? "Anónimo",
                Accion = accion,
                Controlador = controlador,
                Vista = vista,
                FechaHora = DateTime.Now
            };

            _context.BitacoraActividad.Add(entrada);
            await _context.SaveChangesAsync();
        }
    }
}
