using EduHome.Core.Entities;

namespace EduHome.UI.Areas.Admin.Data.Services.Interfaces;

public interface IEventsDetailsServices
{
    Task<IEnumerable<EventsDetails>> GetEventsDetails();
}
