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
    private readonly IEventsDetailsServices _eventsDetailsServices;
    public EventsController(
        AppDbContext context,
        IWebHostEnvironment env,
        IEventServices eventServices,
        IEventsDetailsServices eventsDetailsServices)
    {
        _context = context;
        _env = env;
        _eventServices = eventServices;
        _eventsDetailsServices = eventsDetailsServices;
    }

    public async Task<IActionResult> Index()
    {
        int sum = 0;
        var events = await _context.Eventss.ToListAsync();
        foreach (var c in events) sum++;
        TempData["EventSum"] = sum;

        HomeViewModel model = new() { events = await _eventServices.GetEvent() };
        return View(model);
    }

    public async Task<IActionResult> Details(int id)
    {
        var events = await _eventServices.GetByIdAsync(id);
        ViewBag.EventId = id;
        var model =  await _eventServices.DetailsAsync();
        return View(model);
    }

    public async Task<IActionResult> Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]

    public async Task<IActionResult> Create(EventsViewModel eventsViewModel)
    {
        if (!ModelState.IsValid) return View(eventsViewModel);
        try
        {
            await _eventServices.CreateAsync(eventsViewModel);
            return RedirectToAction("Index");
        }
        catch (ArgumentException ex)
        {
            ModelState.AddModelError("Image", ex.Message);
            return View(eventsViewModel);
        }
    }

    public async Task<IActionResult> Edit(int id)
    {
        var events = await _eventServices.GetByIdAsync(id);
        return View(events);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, EventsViewModel eventsViewModel)
    {
        await _eventServices.EditAsync(id, eventsViewModel);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var events = await _eventServices.GetByIdAsync(id);
        ViewBag.EventId = id;
        var model = await _eventServices.DetailsAsync();
        return View(model);
    }
    [HttpPost,ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePost(int id)
    {
        await _eventServices.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
