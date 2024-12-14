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
    public class ViajesController : Controller
    {
        private readonly ControlMigracionContext _context;

        public ViajesController(ControlMigracionContext context)
        {
            _context = context;
        }

        // GET: Viajes
        public async Task<IActionResult> Index()
        {
            var viajes = await _context.Viajes
                .Include(v => v.Viajero)
                .ToListAsync();
            return View(viajes);
        }

        // GET: Viajes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viaje = await _context.Viajes
                .Include(v => v.Viajero)
                .FirstOrDefaultAsync(m => m.ViajeID == id);
            if (viaje == null)
            {
                return NotFound();
            }

            return View(viaje);
        }

        // GET: Viajes/Create
        public IActionResult Create()
        {
            var viajeros = _context.Viajeros.ToList();
            if (viajeros == null || !viajeros.Any())
            {
                TempData["ErrorMessage"] = "No hay viajeros registrados. Por favor, registre un viajero primero.";
                return RedirectToAction("Create", "Viajeros");
            }
            ViewData["ViajeroID"] = new SelectList(viajeros, "ViajeroID", "NombreCompleto");
            return View();
        }

        // POST: Viajes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ViajeroID,FechaInicio,FechaFin,Destino")] Viaje viaje)
        {
            if (ModelState.IsValid)
            {
                _context.Add(viaje);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var viajeros = await _context.Viajeros.ToListAsync();
            if (viajeros == null || !viajeros.Any())
            {
                TempData["ErrorMessage"] = "No hay viajeros registrados. Por favor, registre un viajero primero.";
                return RedirectToAction("Create", "Viajeros");
            }
            ViewData["ViajeroID"] = new SelectList(viajeros, "ViajeroID", "NombreCompleto", viaje.ViajeroID);
            return View(viaje);
        }

        // GET: Viajes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viaje = await _context.Viajes.FindAsync(id);
            if (viaje == null)
            {
                return NotFound();
            }
            var viajeros = await _context.Viajeros.ToListAsync();
            if (viajeros == null || !viajeros.Any())
            {
                TempData["ErrorMessage"] = "No hay viajeros registrados. No se puede editar el viaje.";
                return RedirectToAction(nameof(Index));
            }
            ViewData["ViajeroID"] = new SelectList(viajeros, "ViajeroID", "NombreCompleto", viaje.ViajeroID);
            return View(viaje);
        }

        // POST: Viajes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ViajeID,ViajeroID,FechaInicio,FechaFin,Destino")] Viaje viaje)
        {
            if (id != viaje.ViajeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(viaje);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ViajeExists(viaje.ViajeID))
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
            var viajeros = await _context.Viajeros.ToListAsync();
            if (viajeros == null || !viajeros.Any())
            {
                TempData["ErrorMessage"] = "No hay viajeros registrados. No se puede editar el viaje.";
                return RedirectToAction(nameof(Index));
            }
            ViewData["ViajeroID"] = new SelectList(viajeros, "ViajeroID", "NombreCompleto", viaje.ViajeroID);
            return View(viaje);
        }

        // GET: Viajes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viaje = await _context.Viajes
                .Include(v => v.Viajero)
                .FirstOrDefaultAsync(m => m.ViajeID == id);
            if (viaje == null)
            {
                return NotFound();
            }

            return View(viaje);
        }

        // POST: Viajes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var viaje = await _context.Viajes.FindAsync(id);
            _context.Viajes.Remove(viaje);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ViajeExists(int id)
        {
            return _context.Viajes.Any(e => e.ViajeID == id);
        }
    }
}

