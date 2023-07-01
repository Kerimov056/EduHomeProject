using EduHome.Core.Entities;
using EduHome.UI.Areas.Admin.Data.Base;

namespace EduHome.UI.Areas.Admin.Data.Services;

public class EventServices : IEventServices
{
    private readonly IEntityBaseRepository<Events> _eventRepository;
    public EventServices(IEntityBaseRepository<Events> eventRepository)
    {
        _eventRepository= eventRepository;
    }
    public  Task DeleteAsync(int id) => _eventRepository.DeleteAsync(id);
    public async Task<IEnumerable<Events>> GetEvent() => await _eventRepository.GetAllAsync();

    public async Task<Events> GetByIdAsync(int id) => await _eventRepository.GetByIdAsync(id);
    public async Task<Events> CreateAsync(Events blog)
    {
        await _eventRepository.AddAsync(blog);
        return blog;
    }

    public async Task<Events> EditAsync(int id, Events blog)
    {
        await _eventRepository.UpdateAsync(id, blog);
        return blog;
    }
}
