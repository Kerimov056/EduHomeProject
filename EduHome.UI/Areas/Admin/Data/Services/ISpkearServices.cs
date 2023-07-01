using EduHome.Core.Entities;

namespace EduHome.UI.Areas.Admin.Data.Services;

public interface ISpkearServices
{
    Task<IEnumerable<Speakers>> GetSpeakers();
    Task<Speakers> CreateAsync(Speakers speakers);
    Task<Speakers> Edit(int id, Speakers speakers);
    Task<Speakers> GetByIdAsync(int id);
    Task DeleteAsync(int id);
}
