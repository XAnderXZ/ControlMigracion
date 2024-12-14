using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ControlMigracion.Data;
using ControlMigracion.Models;
using Microsoft.AspNetCore.Authorization;

namespace ControlMigracion.Controllers
{
    [Authorize]
    public class EntradasSalidasController : Controller
    {
        private readonly ControlMigracionContext _context;

        public EntradasSalidasController(ControlMigracionContext context)
        {
            _context = context;
        }

        // GET: EntradasSalidas
        public async Task<IActionResult> Index()
        {
            var entradasSalidas = await _context.EntradasSalidas
                .Include(e => e.Viaje)
                .ThenInclude(v => v.Viajero)
                .Include(e => e.Pais)
                .OrderByDescending(e => e.Fecha)
                .ToListAsync();
            return View(entradasSalidas);
        }

        // GET: EntradasSalidas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entradaSalida = await _context.EntradasSalidas
                .Include(e => e.Viaje)
                .ThenInclude(v => v.Viajero)
                .Include(e => e.Pais)
                .FirstOrDefaultAsync(m => m.EntradaSalidaID == id);
            if (entradaSalida == null)
            {
                return NotFound();
            }

            return View(entradaSalida);
        }

        // GET: EntradasSalidas/Create
        public IActionResult Create()
        {
            ViewData["ViajeID"] = new SelectList(_context.Viajes, "ViajeID", "ViajeID");
            ViewData["PaisID"] = new SelectList(_context.Paises, "PaisID", "Nombre");
            return View();
        }

        // POST: EntradasSalidas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ViajeID,PaisID,Fecha,Tipo")] EntradaSalida entradaSalida)
        {
            if (ModelState.IsValid)
            {
                _context.Add(entradaSalida);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ViajeID"] = new SelectList(_context.Viajes, "ViajeID", "ViajeID", entradaSalida.ViajeID);
            ViewData["PaisID"] = new SelectList(_context.Paises, "PaisID", "Nombre", entradaSalida.PaisID);
            return View(entradaSalida);
        }

        // GET: EntradasSalidas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entradaSalida = await _context.EntradasSalidas.FindAsync(id);
            if (entradaSalida == null)
            {
                return NotFound();
            }
            ViewData["ViajeID"] = new SelectList(_context.Viajes, "ViajeID", "ViajeID", entradaSalida.ViajeID);
            ViewData["PaisID"] = new SelectList(_context.Paises, "PaisID", "Nombre", entradaSalida.PaisID);
            return View(entradaSalida);
        }

        // POST: EntradasSalidas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EntradaSalidaID,ViajeID,PaisID,Fecha,Tipo")] EntradaSalida entradaSalida)
        {
            if (id != entradaSalida.EntradaSalidaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(entradaSalida);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EntradaSalidaExists(entradaSalida.EntradaSalidaID))
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
            ViewData["ViajeID"] = new SelectList(_context.Viajes, "ViajeID", "ViajeID", entradaSalida.ViajeID);
            ViewData["PaisID"] = new SelectList(_context.Paises, "PaisID", "Nombre", entradaSalida.PaisID);
            return View(entradaSalida);
        }

        // GET: EntradasSalidas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entradaSalida = await _context.EntradasSalidas
                .Include(e => e.Viaje)
                .Include(e => e.Pais)
                .FirstOrDefaultAsync(m => m.EntradaSalidaID == id);
            if (entradaSalida == null)
            {
                return NotFound();
            }

            return View(entradaSalida);
        }

        // POST: EntradasSalidas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var entradaSalida = await _context.EntradasSalidas.FindAsync(id);
            _context.EntradasSalidas.Remove(entradaSalida);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EntradaSalidaExists(int id)
        {
            return _context.EntradasSalidas.Any(e => e.EntradaSalidaID == id);
        }
    }
}

