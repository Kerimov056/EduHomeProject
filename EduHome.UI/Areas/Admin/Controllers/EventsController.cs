using EduHome.Core.Entities;
using EduHome.UI.Areas.Admin.Data.Services.Interfaces;
using EduHome.UI.Areas.Admin.Extension;
using EduHome.UI.Areas.Admin.ViewModel;
using EduHome.UI.ViewModel;
using EduHomeDataAccess.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.UI.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class EventsController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;
    private readonly IEventServices _eventServices; 
    public EventsController(AppDbContext context, IWebHostEnvironment env, IEventServices eventServices)
    {
        _context = context;
        _env = env;
        _eventServices = eventServices;
    }

    public async Task<IActionResult> Index()
    {
        int sum = 0;
        var events = await _context.Eventss.ToListAsync();
        foreach (var c in events)
        {
            sum++;
        }

        TempData["EventSum"] = sum;

        HomeViewModel model = new()
        {
            events = await _eventServices.GetEvent()
        };
        return View(model);
    }

    public async Task<IActionResult> Details(int id)
    {
        if (id == 0 || id == null)
        {
            return NotFound();
        }
        var events = _context.Eventss.Find(id);
        if (events == null)
        {
            return NotFound();
        }
        ViewBag.EventId = events.Id;


        var eventsDetails = await _context.EventsDetailss
        .Include(ed => ed.Events)
        .ToListAsync();

        var eventsIds = eventsDetails.Select(ed => ed.Events.Id).ToList();
        var events1 = await _context.Eventss
            .Where(e => eventsIds.Contains(e.Id))
            .Include(e => e.Events_Speakers)
                .ThenInclude(es => es.Speakers)
            .ToListAsync();
        //Bomba kimi

        return View(new HomeViewModel
        {
            eventsDetails = eventsDetails,
            events = events1,
        });
    }

    public async Task<IActionResult> Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]

    public async Task<IActionResult> Create(EventsViewModel eventsViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(eventsViewModel);
        }
        if (!eventsViewModel.Image.FormatFile("image"))
        {
            ModelState.AddModelError("Image", "Select correct image format!");
            return View(eventsViewModel);
        }
        if (!eventsViewModel.Image.FormatLength(100))
        {
            ModelState.AddModelError("Image", "Size must be less than 100 kb");
            return View(eventsViewModel);
        }
        string filePath = await eventsViewModel.Image.CopyFileAsync(_env.WebRootPath, "assets", "img", "event");


        Events events = new Events
        {
            Name = eventsViewModel.Name,
            DateTime = eventsViewModel.DateTime,
            Location = eventsViewModel.Location,
            Details = new EventsDetails
            {
                ImagePath = filePath,
                Description = eventsViewModel.Description,
            }
        };

        await _eventServices.CreateAsync(events);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(int id)
    {
        if (id == 0 || id == null)
        {
            return NotFound();
        }

        Events? events = await _context.Eventss.Include(e => e.Details).FirstOrDefaultAsync(n => n.Id == id);
        if (events == null)
        {
            return NotFound();
        }

        EventsViewModel ViewModel = new EventsViewModel
        {
            Name = events.Name,
            DateTime = events.DateTime,
            Location = events.Location,
            //Image = events.Details.ImagePath
            Description = events.Details.Description
        };

        return View(ViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, EventsViewModel eventsViewModel)
    {
        if (id == 0 || id == null)
        {
            return NotFound();
        }
        if (!ModelState.IsValid)
        {
            return View(eventsViewModel);
        }
        if (!eventsViewModel.Image.FormatFile("image"))
        {
            ModelState.AddModelError("Image", "Select correct image format!");
            return View(eventsViewModel);
        }
        if (!eventsViewModel.Image.FormatLength(100))
        {
            ModelState.AddModelError("Image", "Size must be less than 100 kb");
            return View(eventsViewModel);
        }

        Events? events = await _context.Eventss.Include(c => c.Details).FirstOrDefaultAsync(c => c.Id == id);
        if (events == null)
        {
            return NotFound();
        }

        string filePath = await eventsViewModel.Image.CopyFileAsync(_env.WebRootPath, "assets", "img", "event");

        _context.Entry(events).CurrentValues.SetValues(eventsViewModel);
        events.Details.ImagePath = filePath;

        await _eventServices.EditAsync(id,events);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        if (id == 0 || id == null)
        {
            return NotFound();
        }
        var events = await _context.Eventss.FindAsync(id);
        if (events is null)
        {
            return NotFound();
        }

        ViewBag.EventId = events.Id;

        var eventsDetails = await _context.EventsDetailss
        .Include(ed => ed.Events)
        .ToListAsync();

        var eventsIds = eventsDetails.Select(ed => ed.Events.Id).ToList();
        var events1 = await _context.Eventss
            .Where(e => eventsIds.Contains(e.Id))
            .Include(e => e.Events_Speakers)
                .ThenInclude(es => es.Speakers)
            .ToListAsync();
        //Bomba kimi

        return View(new HomeViewModel
        {
            eventsDetails = eventsDetails,
            events = events1,
        });
    }
    [HttpPost,ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePost(int id)
    {
        var events = await _context.Eventss.FindAsync(id);
        if (events is null)
        {
            return NotFound();
        }

        await _eventServices.DeleteAsync(id);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
