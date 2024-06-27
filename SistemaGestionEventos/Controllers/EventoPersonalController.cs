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
        /*public async Task<IActionResult> Details(int? id)
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
        }*/

        // GET: EventoPersonal/Create
       /* public IActionResult Create()
        {
            ViewData["IdEvento"] = new SelectList(_context.Eventos, "IdEvento", "IdEvento");
            ViewData["IdPersonal"] = new SelectList(_context.Personal, "IdPersonal", "IdPersonal");
            return View();
        }*/
        public IActionResult Create(int idEvento)
        {
            ViewBag.IdEvento = idEvento;
            ViewBag.IdPersonal = new SelectList(_context.Personal, "IdPersonal", "Nombre");
            return View();
        }



        // POST: EventoPersonal/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEventoPersonal,IdEvento,IdPersonal")] EventoPersonal eventoPersonal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventoPersonal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEvento"] = new SelectList(_context.Eventos, "IdEvento", "IdEvento", eventoPersonal.IdEvento);
            ViewData["IdPersonal"] = new SelectList(_context.Personal, "IdPersonal", "IdPersonal", eventoPersonal.IdPersonal);
            return View(eventoPersonal);
        }*/

        /*ESTO ANDA OK
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEventoPersonal,IdEvento,IdPersonal")] EventoPersonal eventoPersonal)
        {
          
            if (eventoPersonal.IdEvento > -1 )
            {
                _context.Add(eventoPersonal);
                await _context.SaveChangesAsync();

                int personalCount = _context.EventoPersonal.Count(ep => ep.IdEvento == eventoPersonal.IdEvento);
                if (personalCount < 3)
                {
                    ViewBag.ErrorMessage = "Debe asignar al menos 3 registros de personal.";
                    ViewBag.IdEvento = eventoPersonal.IdEvento;
                    ViewBag.IdPersonal = new SelectList(_context.Personal, "IdPersonal", "Nombre");
                    return View(eventoPersonal);
                }

                return RedirectToAction("Index", "Evento");
            }
            ViewBag.IdEvento = eventoPersonal.IdEvento;
            ViewBag.IdPersonal = new SelectList(_context.Personal, "IdPersonal", "Nombre", eventoPersonal.IdPersonal);
            return View(eventoPersonal);
        }*/

        /*esto tambien anda ok
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEventoPersonal,IdEvento,IdPersonal")] EventoPersonal eventoPersonal)
        {

            if (eventoPersonal.IdEvento > -1)
            {
                _context.Add(eventoPersonal);
                await _context.SaveChangesAsync();

                // Obtener el evento y el lugar asociado
                var evento = await _context.Eventos
                    .Include(e => e.Lugar)
                    .FirstOrDefaultAsync(e => e.IdEvento == eventoPersonal.IdEvento);

                if (evento != null)
                {
                    // Verificar si el lugar asociado tiene escaleras
                    bool lugarTieneEscaleras = evento.Lugar != null && evento.Lugar.tieneEscaleras;

                    // Determinar el mínimo número de personal requerido
                    int minPersonalRequired = lugarTieneEscaleras ? 3 : 0;

                    // Contar los registros de personal asociados al evento
                    int personalCount = _context.EventoPersonal.Count(ep => ep.IdEvento == eventoPersonal.IdEvento);

                    // Si no se cumple el requisito mínimo de personal, mostrar mensaje de error y volver a la vista Create
                    if (personalCount < minPersonalRequired)
                    {
                        ViewBag.ErrorMessage = $"Debe asignar al menos {minPersonalRequired} registros de personal.";
                        ViewBag.IdEvento = eventoPersonal.IdEvento;
                        ViewBag.IdPersonal = new SelectList(_context.Personal, "IdPersonal", "Nombre");
                        return View(eventoPersonal);
                    }
                }

                // Redirigir al índice de Evento si se cumple con los requisitos
                return RedirectToAction("Index", "Evento");
            }

            // Si el modelo no es válido, volver a la vista Create con los datos necesarios
            ViewBag.IdEvento = eventoPersonal.IdEvento;
            ViewBag.IdPersonal = new SelectList(_context.Personal, "IdPersonal", "Nombre", eventoPersonal.IdPersonal);
            return View(eventoPersonal);
        }*/

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEventoPersonal,IdEvento,IdPersonal")] EventoPersonal eventoPersonal)
        {
            if (eventoPersonal.IdEvento > -1)
            {
                _context.Add(eventoPersonal);
                await _context.SaveChangesAsync();

                var evento = await _context.Eventos
                    .Include(e => e.Lugar)
                    .FirstOrDefaultAsync(e => e.IdEvento == eventoPersonal.IdEvento);

                if (evento != null)
                {
                    // Verifico si el lugar asociado tiene escaleras
                    bool lugarTieneEscaleras = evento.Lugar != null && evento.Lugar.tieneEscaleras;

                    // Cuento los registros de personal asociados al evento
                    int personalCount = _context.EventoPersonal.Count(ep => ep.IdEvento == eventoPersonal.IdEvento);

                    // Determinar el mínimo número de personal requerido
                    int minPersonalRequired = lugarTieneEscaleras ? 3 : 0;

                    // Si el lugar tiene escaleras y el número de personal es menor que el mínimo requerido, muestro mensaje de error
                    if (lugarTieneEscaleras && personalCount < minPersonalRequired)
                    {
                        ViewBag.ErrorMessage = $"Debe asignar al menos {minPersonalRequired} registros de personal para un lugar con escaleras.";
                        ViewBag.DisableBackButton = true; // Botón deshabilitado mientras no se cumpla el requisito
                    }
                    else
                    {
                        // Permito la redirección y habilito el botón
                        ViewBag.DisableBackButton = false;
                    }
                }
                else
                {
                    ViewBag.DisableBackButton = true;
                }

                // Preparo los datos para la vista Create nuevamente
                ViewBag.IdEvento = eventoPersonal.IdEvento;
                ViewBag.IdPersonal = new SelectList(_context.Personal, "IdPersonal", "Nombre");
                return View(eventoPersonal);
            }

            // Si el modelo no es válido, volver a la vista Create con los datos necesarios
            ViewBag.IdEvento = eventoPersonal.IdEvento;
            ViewBag.IdPersonal = new SelectList(_context.Personal, "IdPersonal", "Nombre", eventoPersonal.IdPersonal);
            ViewBag.DisableBackButton = true; // Asegurar que el botón esté deshabilitado si hay un error en el modelo
            return View(eventoPersonal);
        }












        // GET: EventoPersonal/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventoPersonal = await _context.EventoPersonal.FindAsync(id);
            if (eventoPersonal == null)
            {
                return NotFound();
            }
            ViewData["IdEvento"] = new SelectList(_context.Eventos, "IdEvento", "IdEvento", eventoPersonal.IdEvento);
            ViewData["IdPersonal"] = new SelectList(_context.Personal, "IdPersonal", "IdPersonal", eventoPersonal.IdPersonal);
            return View(eventoPersonal);
        }

        // POST: EventoPersonal/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEventoPersonal,IdEvento,IdPersonal")] EventoPersonal eventoPersonal)
        {
            if (id != eventoPersonal.IdEventoPersonal)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventoPersonal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventoPersonalExists(eventoPersonal.IdEventoPersonal))
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
            ViewData["IdEvento"] = new SelectList(_context.Eventos, "IdEvento", "IdEvento", eventoPersonal.IdEvento);
            ViewData["IdPersonal"] = new SelectList(_context.Personal, "IdPersonal", "IdPersonal", eventoPersonal.IdPersonal);
            return View(eventoPersonal);
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
