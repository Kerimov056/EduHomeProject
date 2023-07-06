using EduHome.Core.Entities;

namespace EduHome.UI.Areas.Admin.Data.Services.Interfaces;

public interface IEventServices
{
    Task<IEnumerable<Events>> GetEvent();
    Task<Events> CreateAsync(Events blog);
    Task<Events> EditAsync(int id, Events blog);
    Task<Events> GetByIdAsync(int id);
    Task DeleteAsync(int id);
}
