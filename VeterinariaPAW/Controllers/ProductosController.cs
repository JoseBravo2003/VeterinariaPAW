using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VeterinariaPAW.Models;
using VeterinariaPAW.Services;

namespace VeterinariaPAW.Controllers
{
    public class ProductosController : Controller
    {
        private readonly VeterinariaContext _context;
        private readonly BitacoraService _bitacora;

        public ProductosController(VeterinariaContext context, BitacoraService bitacora)
        {
            _context = context;
            _bitacora = bitacora;
        }

        // GET: Producto
        public async Task<IActionResult> Index()
        {
            var veterinariaContext = _context.Producto.Include(p => p.Categoria).Include(p => p.Proveedor);
            return View(await veterinariaContext.ToListAsync());


            await _bitacora.Registrar(User.Identity?.Name, "Accedió a listado de productos", "Productos", "Index");
            var productos = _context.Producto.Include(p => p.Categoria).Include(p => p.Proveedor);
            return View(await productos.ToListAsync());
        }
        // GET: Producto
        public async Task<IActionResult> IndexClientes()
        {
            var veterinariaContext = _context.Producto.Include(p => p.Categoria).Include(p => p.Proveedor);
            return View(await veterinariaContext.ToListAsync());
        }

        // GET: Producto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Producto
                .Include(p => p.Categoria)
                .Include(p => p.Proveedor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }
        // GET: Producto/Details/5
        public async Task<IActionResult> DetailsClientes(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Producto
                .Include(p => p.Categoria)
                .Include(p => p.Proveedor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Producto/Create
        public IActionResult Create()
        {
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "Id", "Id");
            ViewData["IdProveedor"] = new SelectList(_context.Proveedor, "Id", "Id");
            return View();
        }

        // POST: Producto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion,FechaCaducidad,SKU,IdCategoria,Precio,Stock,IdProveedor,Tipo,Estado,Clinico,FotoUrl")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "Id", "Id", producto.IdCategoria);
            ViewData["IdProveedor"] = new SelectList(_context.Proveedor, "Id", "Id", producto.IdProveedor);
            return View(producto);
        }

        // GET: Producto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Producto.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "Id", "Id", producto.IdCategoria);
            ViewData["IdProveedor"] = new SelectList(_context.Proveedor, "Id", "Id", producto.IdProveedor);
            return View(producto);
        }

        // POST: Producto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Descripcion,FechaCaducidad,SKU,IdCategoria,Precio,Stock,IdProveedor,Tipo,Estado,Clinico,FotoUrl")] Producto producto)
        {
            if (id != producto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "Id", "Id", producto.IdCategoria);
            ViewData["IdProveedor"] = new SelectList(_context.Proveedor, "Id", "Id", producto.IdProveedor);
            return View(producto);
        }

        // GET: Producto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Producto
                .Include(p => p.Categoria)
                .Include(p => p.Proveedor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Producto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto = await _context.Producto.FindAsync(id);
            if (producto != null)
            {
                _context.Producto.Remove(producto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
            return _context.Producto.Any(e => e.Id == id);
        }
    }
}
