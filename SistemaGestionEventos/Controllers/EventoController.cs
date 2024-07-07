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
    public class EventoController : Controller
    {
        private readonly EventosDatabaseContext _context;

        public EventoController(EventosDatabaseContext context)
        {
            _context = context;
        }

        // GET: Evento
        public async Task<IActionResult> Index()
        {
            var eventosDatabaseContext = _context.Eventos.Include(e => e.Cliente).Include(e => e.Lugar);
            return View(await eventosDatabaseContext.ToListAsync());
        }

        // GET: Evento/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos
                .Include(e => e.Cliente)
                .Include(e => e.Lugar)
                .FirstOrDefaultAsync(m => m.IdEvento == id);
            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        // GET: Evento/Create
        public IActionResult Create()
        {
            var clientes = _context.Clientes.Select(c => new {
                IdCliente = c.IdCliente,
                NombreCompleto = c.Nombre + " " + c.Apellido
            }).ToList();

            ViewData["Clientes"] = new SelectList(clientes, "IdCliente", "NombreCompleto");
            ViewData["Lugares"] = new SelectList(_context.Lugares, "IdLugar", "Nombre");
            return View();
        }

        // POST: Evento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEvento,Descripcion,IdLugar,Equipamiento,Presupuesto,FechaInicio,FechaFin,IdCliente,Estado")] Evento evento)
        {
            if (evento != null)
            {
                _context.Add(evento);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "EventoPersonal", new { idEvento = evento.IdEvento });
            }

            var clientes = _context.Clientes.Select(c => new {
                IdCliente = c.IdCliente,
                NombreCompleto = c.Nombre + " " + c.Apellido
            }).ToList();

            ViewData["Clientes"] = new SelectList(clientes, "IdCliente", "NombreCompleto", evento.IdCliente);
            ViewData["Lugares"] = new SelectList(_context.Lugares, "IdLugar", "IdLugar", evento.IdLugar);
            return View(evento);
        }


        // GET: Evento/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos.FindAsync(id);
            if (evento == null)
            {
                return NotFound();
            }

            var clientes = _context.Clientes.Select(c => new {
                IdCliente = c.IdCliente,
                NombreCompleto = c.Nombre + " " + c.Apellido
            }).ToList();

            ViewData["Clientes"] = new SelectList(clientes, "IdCliente", "NombreCompleto", evento.IdCliente);
            ViewData["Lugares"] = new SelectList(_context.Lugares, "IdLugar", "Nombre", evento.IdLugar);
            return View(evento);
        }

        // POST: Evento/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEvento,Descripcion,IdLugar,Equipamiento,Presupuesto,FechaInicio,FechaFin,IdCliente,Estado")] Evento evento)
        {
            if (id != evento.IdEvento)
            {
                return NotFound();
            }

            /*if (ModelState.IsValid)*/
            if(evento.IdEvento > -1)
            {
                try
                {
                    _context.Update(evento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventoExists(evento.IdEvento))
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

            var clientes = _context.Clientes.Select(c => new {
                IdCliente = c.IdCliente,
                NombreCompleto = c.Nombre + " " + c.Apellido
            }).ToList();

            ViewData["Clientes"] = new SelectList(clientes, "IdCliente", "NombreCompleto", evento.IdCliente);
            ViewData["Lugares"] = new SelectList(_context.Lugares, "IdLugar", "Nombre", evento.IdLugar);
            return View(evento);
        }


        // GET: Evento/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos
                .Include(e => e.Cliente)
                .Include(e => e.Lugar)
                .FirstOrDefaultAsync(m => m.IdEvento == id);
            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        // POST: Evento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var evento = await _context.Eventos.FindAsync(id);
            if (evento != null)
            {
                _context.Eventos.Remove(evento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventoExists(int id)
        {
            return _context.Eventos.Any(e => e.IdEvento == id);
        }
    }
}
