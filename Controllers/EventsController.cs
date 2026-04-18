using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiProyectoMVC.Models.Events;
using MiProyectoMVC.Data;

namespace MiProyectoMVC.Controllers.Events;    
public class EventsController : Controller
{
    private readonly MySqlDBContext _context;

    public EventsController(MySqlDBContext context)
    {
        _context = context;
    }
    
    public async Task<IActionResult> Index()
    {
        var eventos = await _context.Events.ToListAsync();
        return View(eventos);
    }
    
    public async Task<IActionResult> Gallery(string search)
    {
        var eventos = from e in _context.Events
                      where e.Status == "Activo"
                      select e;

        if (!string.IsNullOrEmpty(search))
        {
            eventos = eventos.Where(e => e.Nombre.Contains(search));
        }

        return View(await eventos.ToListAsync());
    }
    
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Event ev)
    {
        if (ModelState.IsValid)
        {
            ev.FechaCreacion = DateTime.Now;
            ev.Status = string.IsNullOrEmpty(ev.Status) ? "Activo" : ev.Status;

            _context.Add(ev);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        return View(ev);
    }
    
    public async Task<IActionResult> Edit(int id)
    {
        var ev = await _context.Events.FindAsync(id);

        if (ev == null)
            return NotFound();

        return View(ev);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Event ev)
    {
        if (ModelState.IsValid)
        {
            _context.Update(ev);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        return View(ev);
    }
    
    public async Task<IActionResult> Delete(int id)
    {
        var ev = await _context.Events.FindAsync(id);

        if (ev != null)
        {
            _context.Events.Remove(ev);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }
}