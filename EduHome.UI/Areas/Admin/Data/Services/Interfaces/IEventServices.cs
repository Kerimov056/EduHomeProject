using EduHome.Core.Entities;
using EduHome.UI.Areas.Admin.ViewModel;
using EduHome.UI.ViewModel;

namespace EduHome.UI.Areas.Admin.Data.Services.Interfaces;

public interface IEventServices
{
    Task<IEnumerable<Events>> GetEvent();
    Task CreateAsync(EventsViewModel eventsViewModel);
    Task EditAsync(int id, EventsViewModel eventsViewModel);
    Task<EventsViewModel> GetByIdAsync(int id);
    Task<HomeViewModel> DetailsAsync();
    Task DeleteAsync(int id);
}
