using EduHome.Core.Entities;
using EduHome.UI.Areas.Admin.Data.Services.Interfaces;
using EduHomeDataAccess.Database;
using Microsoft.EntityFrameworkCore;

namespace EduHome.UI.Areas.Admin.Data.Services.Concrets;

public class EventsDetailsServices : IEventsDetailsServices
{
    private readonly AppDbContext _context;
    public EventsDetailsServices(AppDbContext context)
    {
        _context= context;
    }
    public async Task<IEnumerable<EventsDetails>> GetEventsDetails() 
        => await _context.EventsDetailss.Include(ed => ed.Events).ToListAsync();
}
