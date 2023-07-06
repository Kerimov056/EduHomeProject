using EduHome.Core.Entities;

namespace EduHome.UI.Areas.Admin.Data.Services.Interfaces;

public interface IInfoService
{
    Task<IEnumerable<Info>> GetInfo();
    Task<Info> CreateAsync(Info info);
    Task<Info> EditAsync(int id, Info info);
    Task<Info> GetByIdAsync(int id);
    Task DeleteAsync(int id);
}
