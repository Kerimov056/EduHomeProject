using EduHome.Core.Entities;
using EduHome.UI.Areas.Admin.ViewModel;

namespace EduHome.UI.Areas.Admin.Data.Services.Interfaces;

public interface IInfoService
{
    Task<IEnumerable<Info>> GetInfoAsync();
    Task<Info> FindByIdAsync(int id);
    Task EditAsync(int id,InfoViewModel ınfoViewModel);
    Task CreateAsync(InfoViewModel ınfoViewModel);
    Task DeleteAsync(int id);
}
