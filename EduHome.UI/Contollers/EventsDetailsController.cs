using EduHome.Core.Entities;
using EduHome.UI.ViewModel;
using EduHomeDataAccess.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.UI.Contollers;

public class EventsDetailsController : Controller
{
    private readonly AppDbContext _context;
    public EventsDetailsController(AppDbContext context)
    {
        _context= context;
    }
    public async Task<IActionResult> Index(int id)
    {
        if (id==0)
        {
            return NotFound();
        }
        var Event = _context.Eventss.Find(id);
        if (Event == null)
        {
            return BadRequest();
        }
        ViewBag.EventProductId = Event.Id;

        var eventsDetails = await _context.EventsDetailss
       .Include(ed => ed.Events)
       .ToListAsync();

        var eventsIds = eventsDetails.Select(ed => ed.Events.Id).ToList();
        var events = await _context.Eventss
            .Where(e => eventsIds.Contains(e.Id))
            .Include(e => e.Events_Speakers)
                .ThenInclude(es => es.Speakers)
            .ToListAsync();
        //Bomba kimi
      
        return View(new HomeViewModel
        {
            
            eventsDetails = eventsDetails,
            events = events,
            blogs = await _context.Blogs.ToListAsync(),
            categories = await _context.Categoriess.ToListAsync(),
            viewers = await _context.Viewers.ToListAsync()
        });
    }   
}
