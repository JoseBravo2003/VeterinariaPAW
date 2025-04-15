using VeterinariaPAW.Models;

namespace VeterinariaPAW.Services
{
    public class DashboardService
    {
        private readonly VeterinariaContext _context;

        public DashboardService(VeterinariaContext context)
        {
            _context = context;
        }

        public List<DashboardMetricDTO> ObtenerDatos()
        {
            var total = _context.Producto.Count();
            var stockBajo = _context.Producto.Count(p => p.Stock < 10);
            var categoriasUnicas = _context.Producto.Select(p => p.IdCategoria).Distinct().Count();
            var clinicos = _context.Producto.Count(p => p.Clinico == true);
            var vencidos = _context.Producto.Count(p => p.FechaCaducidad < DateTime.Today);

            return new List<DashboardMetricDTO>
            {
                new() { Titulo = "Productos Totales", Valor = total },
                new() { Titulo = "Stock Bajo (<10)", Valor = stockBajo },
                new() { Titulo = "Categorías Distintas", Valor = categoriasUnicas },
                new() { Titulo = "Productos Clínicos", Valor = clinicos },
                new() { Titulo = "Productos Vencidos", Valor = vencidos }
            };
        }
    }
}
