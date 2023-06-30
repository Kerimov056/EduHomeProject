﻿using AutoMapper;
using EduHome.Core.Entities;
using EduHome.UI.Areas.Admin.Extension;
using EduHome.UI.Areas.Admin.ViewModel;
using EduHome.UI.ViewModel;
using EduHomeDataAccess.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EduHome.UI.Areas.Admin.Controllers;

[Area("Admin")]
public class SpeakerController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;
    private readonly IMapper _mapper;
    public SpeakerController(AppDbContext context, IWebHostEnvironment env, IMapper mapper)
    {
        _context = context;
        _env = env;
        _mapper = mapper;
    }
    public async Task<IActionResult> Index()
    {
        HomeViewModel model = new()
        {
            speakers = await _context.Speakerss.ToArrayAsync()
        };
        return View(model);
    }

    public async Task<IActionResult> Create()
    {
        ViewBag.Eventsss = await _context.Eventss.ToListAsync();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(SpeakerViewModel speakerViewModel, int[] SelectedEventIds)
    {
        if (!ModelState.IsValid)
        {
            return View(speakerViewModel);
        }

        if (!speakerViewModel.Image.FormatFile("image"))
        {
            ModelState.AddModelError("Image", "Select correct image format!");
            return View(speakerViewModel);
        }

        if (!speakerViewModel.Image.FormatLength(100))
        {
            ModelState.AddModelError("Image", "Size must be less than 100 kb");
            return View(speakerViewModel);
        }

        string filePath = await speakerViewModel.Image.CopyFileAsync(_env.WebRootPath, "assets", "img", "event");

        var events = await _context.Eventss.FindAsync(SelectedEventIds.FirstOrDefault());

        if (events == null)
        {
            ModelState.AddModelError("EventId", "Invalid Event selected!");
            ViewBag.Eventsss = await _context.Eventss.ToListAsync();
            return View(speakerViewModel);
        }

        Speakers speakers = _mapper.Map<Speakers>(speakerViewModel);
        speakers.ImagePath = filePath;

        _context.Speakerss.Add(speakers);
        await _context.SaveChangesAsync();

        foreach (var eventId in SelectedEventIds)
        {
            var events_speakers = new Events_Speakers
            {
                EventsId = eventId,
                SpeakersId = speakers.Id
            };

            _context.EventsDetails.Add(events_speakers);
        }

        await _context.SaveChangesAsync();
        var eventList = _context.Eventss.ToList();
        ViewBag.Eventsss = new SelectList(eventList, "Id", "Name");

        return RedirectToAction(nameof(Index));
    }



    public async Task<IActionResult> Details(int id)
    {
        if (id == 0 || id == null)
        {
            return NotFound();
        }
        var spkear = await _context.Speakerss.FindAsync(id);
        if (spkear is null)
        {
            return NotFound();
        }

        ViewBag.DetailSpkearId = spkear.Id;

        ViewBag.FullEvent = await _context.Eventss.ToListAsync();


        HomeViewModel model = new()
        {
            events = await _context.Eventss.ToListAsync(),
            events_Speakers  = await _context.EventsDetails.ToArrayAsync(),
            speakers = await _context.Speakerss.ToListAsync(),
        };
        return View(model);
    }



    public async Task<IActionResult> Edit(int id)
    {
        if (id == 0 || id == null)
        {
            return NotFound();
        }
        var spkear = await _context.Speakerss.FindAsync(id);
        if (spkear is null)
        {
            return NotFound();
        }
        var VM = _mapper.Map<SpeakerViewModel>(spkear);

        VM.SelectedEventIds = await _context.EventsDetails.Where(e => e.SpeakersId == id)
            .Select(e => e.EventsId).ToListAsync();

        ViewBag.Eventsss = await _context.Eventss.ToListAsync();
        return View(VM);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, SpeakerViewModel speakerViewModel, int EventId)
    {
        if (id == 0 || id == null)
        {
            return NotFound();
        }
        if (!ModelState.IsValid)
        {
            return View(speakerViewModel);
        }
        if (!speakerViewModel.Image.FormatFile("image"))
        {
            ModelState.AddModelError("Image", "Select correct image format!");
            return View(speakerViewModel);
        }
        if (!speakerViewModel.Image.FormatLength(100))
        {
            ModelState.AddModelError("Image", "Size must be less than 100 kb");
            return View(speakerViewModel);
        }
        string filePath = await speakerViewModel.Image.CopyFileAsync(_env.WebRootPath, "assets", "img", "event");

        Speakers spkear = await _context.Speakerss.FindAsync(id);
        if (spkear is null)
        {
            return NotFound();
        }

        // Speaker bilgilerini güncelle
        spkear.Name = speakerViewModel.Name;
        spkear.ImagePath = filePath;
        spkear.Postions = speakerViewModel.Postions;
        spkear.JobName = speakerViewModel.JobName;

        // Speaker'ın Event'lerini güncelle
        var existingEvents = await _context.EventsDetails.Where(e => e.SpeakersId == id).ToListAsync();
        _context.EventsDetails.RemoveRange(existingEvents);

        if (speakerViewModel.SelectedEventIds != null)
        {
            foreach (var eventId in speakerViewModel.SelectedEventIds)
            {
                var eventSpeaker = new Events_Speakers
                {
                    EventsId = eventId,
                    SpeakersId = id
                };
                _context.EventsDetails.Add(eventSpeaker);
            }
        }

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }


    public async Task<IActionResult> Delete(int id)
    {
        if (id == 0 || id == null)
        {
            return NotFound();
        }
        Speakers? Spkear = await _context.Speakerss.FindAsync(id);
        if (Spkear is null)
        {
            return NotFound();
        }
        ViewBag.SpkearId = Spkear.Id;
        HomeViewModel model = new()
        {
            speakers = await _context.Speakerss.ToArrayAsync(),
        };
        return View(model);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePost(int id)
    {
        var speaker = await _context.Speakerss.FindAsync(id);
        if (speaker is null)
        {
            return NotFound();
        }

        _context.Entry(speaker).State = EntityState.Deleted;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
