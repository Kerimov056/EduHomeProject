using EduHome.Core.Entities;

namespace EduHome.UI.Areas.Admin.Data.Services;

public interface IBlogsService
{
    Task<IEnumerable<Blog>> GetBlogs();
    Task<Blog> CreateAsync(Blog blog);
    Task<Blog> EditAsync(int id, Blog blog);
    Task<Blog> GetByIdAsync(int id);
    Task DeleteAsync(int id);
}
