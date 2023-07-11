using EduHome.Core.Entities;
using EduHome.UI.Areas.Admin.ViewModel;

namespace EduHome.UI.Areas.Admin.Data.Services.Interfaces;

public interface IContactService
{
    Task<IEnumerable<Viewer>> GetViewer();
    Task<Viewer> FindByIdAsync(int id);
    Task DeleteAsync(int id);
    //Task CreateAsync(BlogViewModel blogViewModel);
    //Task EditAsync(int Id, BlogViewModel blogViewModel);
}
