using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaGestionEventos.Context;
using SistemaGestionEventos.Models;

namespace SistemaGestionEventos.Controllers
{
    public class EventoPersonalController : Controller
    {
        private readonly EventosDatabaseContext _context;

        public EventoPersonalController(EventosDatabaseContext context)
        {
            _context = context;
        }

        // GET: EventoPersonal
        public async Task<IActionResult> Index()
        {
            var eventosDatabaseContext = _context.EventoPersonal.Include(e => e.Evento).Include(e => e.Personal);
            return View(await eventosDatabaseContext.ToListAsync());
        }

        // GET: EventoPersonal/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventoPersonal = await _context.EventoPersonal
                .Include(e => e.Evento)
                .Include(e => e.Personal)
                .FirstOrDefaultAsync(m => m.IdEventoPersonal == id);
            if (eventoPersonal == null)
            {
                return NotFound();
            }

            return View(eventoPersonal);
        }

        // GET: EventoPersonal/Create
        public async Task<IActionResult> Create(int idEvento)
        {
            var evento = await _context.Eventos.Include(e => e.Lugar).FirstOrDefaultAsync(e => e.IdEvento == idEvento);
            if (evento == null)
            {
                return NotFound();
            }

            bool lugarTieneEscaleras = evento.Lugar != null && evento.Lugar.tieneEscaleras;
            int personalCount = _context.EventoPersonal.Count(ep => ep.IdEvento == idEvento);
            int minPersonalRequired = lugarTieneEscaleras ? 3 : 1;

            ViewBag.IdEvento = idEvento;
            ViewBag.IdPersonal = new SelectList(_context.Personal, "IdPersonal", "Nombre");
            ViewBag.LugarTieneEscaleras = lugarTieneEscaleras;
            ViewBag.DisableBackButton = personalCount < minPersonalRequired;

            if (lugarTieneEscaleras)
            {
                ViewBag.ErrorMessage = $"Debe asignar al menos {minPersonalRequired} registros de personal para un lugar con escaleras.";
            }

            return View();
        }


        // POST: EventoPersonal/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEventoPersonal,IdEvento,IdPersonal")] EventoPersonal eventoPersonal)
        {
            if (eventoPersonal.IdEvento > -1)
            {
                _context.Add(eventoPersonal);
                await _context.SaveChangesAsync();

                var personal = await _context.Personal.FindAsync(eventoPersonal.IdPersonal);
                var nombreCompletoPersonal = personal.Nombre + " " + personal.Apellido;

                var evento = await _context.Eventos
                    .Include(e => e.Lugar)
                    .FirstOrDefaultAsync(e => e.IdEvento == eventoPersonal.IdEvento);

                if (evento != null)
                {
                    bool lugarTieneEscaleras = evento.Lugar != null && evento.Lugar.tieneEscaleras;
                    int personalCount = _context.EventoPersonal.Count(ep => ep.IdEvento == eventoPersonal.IdEvento);
                    int minPersonalRequired = lugarTieneEscaleras ? 3 : 1;

                    if (lugarTieneEscaleras && personalCount < minPersonalRequired)
                    {
                        ViewBag.ErrorMessage = $"Debe asignar al menos {minPersonalRequired} registros de personal para un lugar con escaleras.";
                        ViewBag.DisableBackButton = true;
                    }
                    else
                    {
                        ViewBag.DisableBackButton = false;
                    }
                }
                else
                {
                    ViewBag.DisableBackButton = true;
                }

                TempData["SuccessMessage"] = $"Se agregó exitosamente la persona {nombreCompletoPersonal}.";
                return RedirectToAction(nameof(Create), new { idEvento = eventoPersonal.IdEvento });
            }

            ViewBag.IdEvento = eventoPersonal.IdEvento;
            ViewBag.IdPersonal = new SelectList(_context.Personal, "IdPersonal", "Nombre", eventoPersonal.IdPersonal);
            ViewBag.DisableBackButton = true;
            return View(eventoPersonal);
        }


        // GET: EventoPersonal/CreateForEvent
        public IActionResult CreateForEvent()
        {
            ViewBag.IdEvento = new SelectList(_context.Eventos, "IdEvento", "Descripcion");
            ViewBag.IdPersonal = new SelectList(_context.Personal, "IdPersonal", "Nombre");
            return View();
        }

        // POST: EventoPersonal/CreateForEvent
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateForEvent([Bind("IdEventoPersonal,IdEvento,IdPersonal")] EventoPersonal eventoPersonal)
        {
            _context.Add(eventoPersonal);
            await _context.SaveChangesAsync();

            var personal = await _context.Personal.FindAsync(eventoPersonal.IdPersonal);
            var nombreCompletoPersonal = personal.Nombre + " " + personal.Apellido;

            TempData["SuccessMessage"] = $"Se agregó exitosamente la persona {nombreCompletoPersonal}.";

            return RedirectToAction(nameof(CreateForEvent));
        }

        // GET: EventoPersonal/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventoPersonal = await _context.EventoPersonal
                .Include(e => e.Evento)
                .Include(e => e.Personal)
                .FirstOrDefaultAsync(m => m.IdEventoPersonal == id);
            if (eventoPersonal == null)
            {
                return NotFound();
            }

            return View(eventoPersonal);
        }

        // POST: EventoPersonal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventoPersonal = await _context.EventoPersonal.FindAsync(id);
            if (eventoPersonal != null)
            {
                _context.EventoPersonal.Remove(eventoPersonal);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventoPersonalExists(int id)
        {
            return _context.EventoPersonal.Any(e => e.IdEventoPersonal == id);
        }
    }
}
