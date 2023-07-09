using AutoMapper;
using EduHome.Core.Entities;
using EduHome.UI.Areas.Admin.Data.Base;
using EduHome.UI.Areas.Admin.Data.Exception;
using EduHome.UI.Areas.Admin.Data.Services.Interfaces;
using EduHome.UI.Areas.Admin.Extension;
using EduHome.UI.Areas.Admin.ViewModel;
using EduHome.UI.ViewModel;
using EduHomeDataAccess.Database;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace EduHome.UI.Areas.Admin.Data.Services.Concrets;

public class EventServices : IEventServices
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _env;

    public EventServices(AppDbContext context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<IEnumerable<Events>> GetEvent() => await _context.Eventss.ToListAsync();

    public async Task CreateAsync(EventsViewModel eventsViewModel)
    {
        if (eventsViewModel is null) throw  new NullReferenceException();

        if (!eventsViewModel.Image.FormatFile("image"))
        {
            throw new ArgumentException("Select correct image format!");
        }
        if (!eventsViewModel.Image.FormatLength(1000))
        {
            throw new ArgumentException("Size must be less than 1000 kb");
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

        await _context.Eventss.AddAsync(events);
        await _context.SaveChangesAsync();
    }

    public async Task<HomeViewModel> DetailsAsync()
    {
        var eventsDetails = await _context.EventsDetailss
       .Include(ed => ed.Events)
       .ToListAsync();

        var eventsIds = eventsDetails.Select(ed => ed.Events.Id).ToList();
        var events1 = await _context.Eventss
            .Where(e => eventsIds.Contains(e.Id))
            .Include(e => e.Events_Speakers)
                .ThenInclude(es => es.Speakers)
            .ToListAsync();

        HomeViewModel homeViewModel = new()
        {
            eventsDetails = eventsDetails,
            events = events1,
        };
        return homeViewModel;
    }
    public async Task DeleteAsync(int id)
    {
        if (id == 0) throw new NotFoundException("Event is Null");

        var events = await _context.Eventss.Include(es=>es.Events_Speakers).ThenInclude(s=>s.Speakers).FirstOrDefaultAsync(e=>e.Id==id);
        if (events is null) throw new NullReferenceException();

        foreach (var item in _context.EventsDetails)
        {
            if (item.EventsId==events.Id)
            {
                _context.EventsDetails.RemoveRange(item.Speakers.Events_Speakers);
                _context.Speakerss.Remove(item.Speakers);
                break;
            }
        }
        await _context.SaveChangesAsync();

        _context.Eventss.Remove(events);
        await _context.SaveChangesAsync();
    }

    public async Task EditAsync(int id, EventsViewModel eventsViewModel)
    {
        Events? events = await _context.Eventss.Include(e => e.Details).FirstOrDefaultAsync(n => n.Id == id);
        if (events is null) throw new NullReferenceException("Events is nUll");

        if (eventsViewModel.Image is null)              
        {
            if (!eventsViewModel.Image.FormatFile("Image"))
            {
                throw new ArgumentException("Select correct image format!");
            }

            if (!eventsViewModel.Image.FormatLength(1000))
            {
                throw new ArgumentException("Size must be less than 1000 kb");
            }

            string filePath = await eventsViewModel.Image.CopyFileAsync(_env.WebRootPath, "assets", "img", "slider");
            events.Details.ImagePath = filePath;
        }

        EventsViewModel ViewModel = new EventsViewModel
        {
            Name = events.Name,
            DateTime = events.DateTime,
            Location = events.Location,
            Description = events.Details.Description
        };

        _context.Eventss.Update(events);
        await _context.SaveChangesAsync();
    }

    public async Task<EventsViewModel> GetByIdAsync(int id)
    {
        if (id == 0) throw new NotFoundException("Event is Null");
        var GetEvent = await _context.Eventss.Include(e => e.Details).FirstOrDefaultAsync(n => n.Id == id);
        if (GetEvent is null) throw new NullReferenceException();
        EventsViewModel model = _mapper.Map<EventsViewModel>(GetEvent);
        model.Description=GetEvent.Details.Description;
        return model;
    }

}
