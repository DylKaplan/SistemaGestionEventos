using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaGestionEventos.Context;
using SistemaGestionEventos.Models;

namespace SistemaGestionEventos.Controllers
{
    public class LugarController : Controller
    {
        private readonly EventosDatabaseContext _context;

        public LugarController(EventosDatabaseContext context)
        {
            _context = context;
        }

        // GET: Lugar
        public async Task<IActionResult> Index()
        {
            return View(await _context.Lugares.ToListAsync());
        }

        // GET: Lugar/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lugar = await _context.Lugares
                .FirstOrDefaultAsync(m => m.IdLugar == id);
            if (lugar == null)
            {
                return NotFound();
            }

            return View(lugar);
        }

        // GET: Lugar/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lugar/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdLugar,Nombre,Direccion,tieneEscaleras")] Lugar lugar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lugar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lugar);
        }

        // GET: Lugar/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lugar = await _context.Lugares.FindAsync(id);
            if (lugar == null)
            {
                return NotFound();
            }
            return View(lugar);
        }

        // POST: Lugar/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdLugar,Nombre,Direccion,tieneEscaleras")] Lugar lugar)
        {
            if (id != lugar.IdLugar)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lugar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LugarExists(lugar.IdLugar))
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
            return View(lugar);
        }

        // GET: Lugar/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lugar = await _context.Lugares
                .FirstOrDefaultAsync(m => m.IdLugar == id);
            if (lugar == null)
            {
                return NotFound();
            }

            return View(lugar);
        }

        // POST: Lugar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lugar = await _context.Lugares.FindAsync(id);
            if (lugar != null)
            {
                _context.Lugares.Remove(lugar);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LugarExists(int id)
        {
            return _context.Lugares.Any(e => e.IdLugar == id);
        }
    }
}
