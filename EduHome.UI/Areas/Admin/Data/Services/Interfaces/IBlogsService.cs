using EduHome.Core.Entities;
using EduHome.UI.Areas.Admin.ViewModel;

namespace EduHome.UI.Areas.Admin.Data.Services.Interfaces;

public interface IBlogsService
{
    Task<IEnumerable<Blog>> GetBlogs();
    Task CreateAsync(BlogViewModel blogViewModel);
    Task DeleteAsync(int id);
    Task<BlogViewModel> FindByIdAsync(int id);
    Task EditAsync(int Id,BlogViewModel blogViewModel);
}
