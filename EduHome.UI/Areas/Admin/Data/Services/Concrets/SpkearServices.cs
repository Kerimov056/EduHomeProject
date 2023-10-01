using AutoMapper;
using EduHome.Core.Entities;
using EduHomeDataAccess.Interfaces;
using EduHome.UI.Areas.Admin.Data.Exception;
using EduHome.UI.Areas.Admin.Data.Services.Interfaces;
using EduHome.UI.Areas.Admin.Extension;
using EduHome.UI.Areas.Admin.ViewModel;
using EduHome.UI.ViewModel;
using EduHomeDataAccess.Database;
using Microsoft.EntityFrameworkCore;

namespace EduHome.UI.Areas.Admin.Data.Services.Concrets;

public class SpkearServices : ISpkearServices
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;
    private readonly IMapper _mapper;
    private readonly IEntityBaseRepository<Speakers> _entityBaseRepository;

    public SpkearServices(
        AppDbContext context,
        IWebHostEnvironment env,
        IMapper mapper,
        IEntityBaseRepository<Speakers> entityBaseRepository)
    {
        _context = context;
        _env = env;
        _mapper = mapper;
        _entityBaseRepository = entityBaseRepository;
    }

    public async Task CreateAsync(SpeakerViewModel speakerViewModel, int[] SelectedEventIds)
    {
        if (speakerViewModel is null) throw new NotFoundException("Spkear is null");

        if (!speakerViewModel.Image.FormatLength(1000))
        {
            throw new ArgumentNullException("Size must be less than 1000 kb");
        }
        string filePath = await speakerViewModel.Image.CopyFileAsync(_env.WebRootPath, "assets", "img", "event");

        var events = await _context.Eventss.FindAsync(SelectedEventIds.FirstOrDefault());
        if (events is null) throw new NotFoundException("Event is null");

        Speakers speakers = _mapper.Map<Speakers>(speakerViewModel);
        speakers.ImagePath = filePath;

        await _entityBaseRepository.AddAsync(speakers);
        await _context.SaveChangesAsync();

        foreach (var eventId in SelectedEventIds)
        {
            var events_speakers = new Events_Speakers
            {
                EventsId = eventId,
                SpeakersId = speakers.Id
            };

            await _context.EventsDetails.AddAsync(events_speakers);
        }
        await _context.SaveChangesAsync();


    }

    public async Task DeleteAsync(int id)
    {
        if (id == 0) throw new NullReferenceException();
        var DeleteedEvent = await _context.Speakerss.FindAsync(id);
        if (DeleteedEvent is null) throw new NotFoundException("Event is Null");
        
        await _entityBaseRepository.DeleteAsync(id);
        await _context.SaveChangesAsync();
    }

    public async Task<HomeViewModel> Details()
    {
        HomeViewModel model = new()
        {
            events = await _context.Eventss.ToListAsync(),
            events_Speakers = await _context.EventsDetails.ToListAsync(),
            speakers = await _entityBaseRepository.GetAllAsync(),
        };
        return model;
    }

    public async Task EditAsync(int id, SpeakerViewModel SpeakerViewModel, int EventId)
    {
        if (id == 0) throw new NullReferenceException("Spkear is Null");
        Speakers? spkear = await _context.Speakerss.FindAsync(id);
        if (spkear is null) throw new NotFoundException("Spkear is Null");
        if (SpeakerViewModel.Image is not null)
        {
            if (!SpeakerViewModel.Image.FormatFile("image"))
            {
                throw new ArgumentNullException("Select Correct Image Format");
            }
            if (!SpeakerViewModel.Image.FormatLength(1000))
            {
                throw new ArgumentNullException("Size must be than 1000 kb");
            }
            string filePath = await SpeakerViewModel.Image.CopyFileAsync(_env.WebRootPath, "assets", "img", "event");
            spkear.ImagePath = filePath;
        }
        spkear.Name = SpeakerViewModel.Name;
        spkear.Postions = SpeakerViewModel.Postions;
        spkear.JobName = SpeakerViewModel.JobName;

        var exisEvents = await _context.EventsDetails.Where(e => e.SpeakersId == id).ToListAsync();
        _context.EventsDetails.RemoveRange(exisEvents);

        if (SpeakerViewModel.SelectedEventIds is not null)
        {
            foreach (var eventId in SpeakerViewModel.SelectedEventIds)
            {
                var eventSpeaker = new Events_Speakers
                {
                    EventsId = eventId,
                    SpeakersId = id
                };
                _context.EventsDetails.Add(eventSpeaker);
            }
        }
        await _entityBaseRepository.UpdateAsync(id,spkear);
        await _context.SaveChangesAsync();
    }

    public async Task<Speakers> GetByIdAsync(int id)
    {
        if (id == 0) throw new NullReferenceException();
        var spkear = await _entityBaseRepository.GetByIdAsync(id);
        if (spkear is null) throw new NotFoundException("Spkear is Null");
        return spkear;
    }

    public async Task<IEnumerable<Speakers>> GetSpeakers() => await _entityBaseRepository.GetAllAsync();
}
