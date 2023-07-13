using EduHome.Core.Entities;

namespace EduHome.UI.Areas.Admin.Data.Services.Interfaces;

public interface IUserServices
{
    Task<IEnumerable<User>> GetUserAsync();
    Task DeleteAsync(string id);
    Task<User> FindByIdAsync(string id);
}
