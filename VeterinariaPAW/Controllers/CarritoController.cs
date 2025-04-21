using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VeterinariaPAW.Models;

namespace VeterinariaPAW.Controllers
{
    public class CarritoController : Controller
    {
        private readonly VeterinariaContext _context;

        public CarritoController(VeterinariaContext context)
        {
            _context = context;
        }

        private List<ItemCarrito> ObtenerCarrito()
        {
            var carritoJson = HttpContext.Session.GetString("Carrito");
            if (carritoJson != null)
            {
                return JsonConvert.DeserializeObject<List<ItemCarrito>>(carritoJson);
            }
            return new List<ItemCarrito>();
        }

        private void GuardarCarrito(List<ItemCarrito> carrito)
        {
            var carritoJson = JsonConvert.SerializeObject(carrito);
            HttpContext.Session.SetString("Carrito", carritoJson);
        }

        public IActionResult Index()
        {
            var carrito = ObtenerCarrito();
            return View(carrito);
        }

        public IActionResult Agregar(int id)
        {
            var producto = _context.Producto.FirstOrDefault(p => p.Id == id);
            if (producto == null)
                return NotFound();

            var carrito = ObtenerCarrito();
            var item = carrito.FirstOrDefault(c => c.Producto.Id == id);

            if (item != null)
            {
                item.cantidad++;
            }
            else
            {
                carrito.Add(new ItemCarrito { Producto = producto, cantidad = 1 });
            }

            GuardarCarrito(carrito);
            return RedirectToAction("IndexClientes", "Productos", new { area = "" });
        }

        public IActionResult Eliminar(int id)
        {
            var carrito = ObtenerCarrito();
            var item = carrito.FirstOrDefault(c => c.Producto.Id == id);

            if (item != null)
            {
                carrito.Remove(item);
            }

            GuardarCarrito(carrito);
            return RedirectToAction("Index");
        }

        public IActionResult Vaciar()
        {
            HttpContext.Session.Remove("Carrito");
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Comprar(int productoId)
        {
            // Obtenemos el carrito de la sesión
            var carrito = ObtenerCarrito();
        


            // Buscamos el item correspondiente
            var item = carrito.FirstOrDefault(i => i.Producto.Id == productoId);
            if (item == null)
            {
                TempData["Mensaje"] = "Producto no encontrado en el carrito.";
                return RedirectToAction("Index");
            }

            // Obtenemos el producto real desde la base de datos
            var producto = await _context.Producto.FindAsync(productoId);
            if (producto == null || producto.Stock <= 0)
            {
                TempData["Mensaje"] = "No hay stock disponible.";
                return RedirectToAction("Index");
            }

            // Reducimos el stock en 1
            producto.Stock -= 1;
            _context.Update(producto);
            await _context.SaveChangesAsync();

            // Reducimos la cantidad en el carrito
            item.cantidad--;
            if (item.cantidad <= 0)
            {
                carrito.Remove(item);
            }

            GuardarCarrito(carrito);

            TempData["CompraExitosa"] = "¡Compra realizada!";
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> ComprarTodo()
        {
            var carrito = ObtenerCarrito();
            foreach (var item in carrito)
            {
                var producto = await _context.Producto.FindAsync(item.Producto.Id);
                if (producto != null && producto.Stock > 0)
                {
                    producto.Stock -= item.cantidad;
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
            }

            carrito.Clear();
            GuardarCarrito(carrito);
            TempData["CompraExitosa"] = "¡Compra realizada!";
            return RedirectToAction("Index");
        }

        public IActionResult ActualizarCantidad(int productoId, int cantidad)
        {
            var carrito = ObtenerCarrito();
            var item = carrito.FirstOrDefault(c => c.Producto.Id == productoId);

            if (item != null && cantidad > 0)
            {
                item.cantidad = cantidad;
                GuardarCarrito(carrito);
            }

            // Retorna el precio total actualizado
            var total = carrito.Sum(i => decimal.Parse(i.Producto.Precio) * i.cantidad);
            return Json(new { total = total.ToString("F2") });
        }


    }
}
