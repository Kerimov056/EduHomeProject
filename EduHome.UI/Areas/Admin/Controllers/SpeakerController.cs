using AutoMapper;
using EduHome.Core.Entities;
using EduHome.UI.Areas.Admin.Data.Services.Concrets;
using EduHome.UI.Areas.Admin.Data.Services.Interfaces;
using EduHome.UI.Areas.Admin.Extension;
using EduHome.UI.Areas.Admin.ViewModel;
using EduHome.UI.ViewModel;
using EduHomeDataAccess.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EduHome.UI.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class SpeakerController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;
    private readonly IMapper _mapper;
    private readonly ISpkearServices _spkearServices;
    private readonly IEventServices _eventServices;
    public SpeakerController(
        AppDbContext context,
        IWebHostEnvironment env,
        IMapper mapper, 
        ISpkearServices spkearServices,
        IEventServices eventServices)
    {
        _context = context;
        _env = env;
        _mapper = mapper;
        _spkearServices = spkearServices;
        _eventServices = eventServices;
    }
    public async Task<IActionResult> Index()
    {
        int sum = 0;
        var speaker = await _spkearServices.GetSpeakers();
        foreach (var c in speaker) sum++;
        TempData["SpeakerSum"] = sum;

        HomeViewModel model = new()
        {
            speakers = await _spkearServices.GetSpeakers()
        };
        return View(model);
    }

    public async Task<IActionResult> Create()
    {
        ViewBag.Eventsss = await _eventServices.GetEvent();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(SpeakerViewModel speakerViewModel, int[] SelectedEventIds)
    {
        if (!ModelState.IsValid) return View(speakerViewModel);
        try
        {
            var eventList = await _eventServices.GetEvent();
            await _spkearServices.CreateAsync(speakerViewModel, SelectedEventIds);
            ViewBag.Eventsss = new SelectList(eventList, "Id", "Name");
            return RedirectToAction(nameof(Index));
        }
        catch (ArgumentException ex)
        {
            ModelState.AddModelError("Image", ex.Message);
            return View(speakerViewModel);
        }
    }

    public async Task<IActionResult> Details(int id)
    {
        var spkear = await _spkearServices.GetByIdAsync(id);
        ViewBag.DetailSpkearId = spkear.Id;
        ViewBag.FullEvent = await _eventServices.GetEvent();
        var model = await _spkearServices.Details();
        return View(model);
    }

    

    public async Task<IActionResult> Edit(int id)
    {
        var spkear = await _spkearServices.GetByIdAsync(id);
        var VM = _mapper.Map<SpeakerViewModel>(spkear);
        VM.SelectedEventIds = await _context.EventsDetails.Where(e => e.SpeakersId == id)
            .Select(e => e.EventsId).ToListAsync();
        ViewBag.Eventsss = await _eventServices.GetEvent();
        return View(VM);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, SpeakerViewModel speakerViewModel, int EventId)
    {
        if (!ModelState.IsValid) return View(speakerViewModel);
        await _spkearServices.EditAsync(id, speakerViewModel, EventId);
        return RedirectToAction(nameof(Index));
    }


    public async Task<IActionResult> Delete(int id)
    {
        Speakers? Spkear = await _spkearServices.GetByIdAsync(id);
        ViewBag.SpkearId = Spkear.Id;
        HomeViewModel model = new()
        {
            speakers = await _spkearServices.GetSpeakers(),
        };
        return View(model);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePost(int id)
    {
        await _spkearServices.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
