using EduHome.Core.Entities;

namespace EduHome.UI.Areas.Admin.Data.Services;

public interface IBlogsService
{
    Task<IEnumerable<Blog>> GetBlogs();
    Task<IEnumerable<Blog>> Delete(Blog blog);
    Task<IEnumerable<Blog>> Update(int id,Blog newBlog);
    Blog GetBlog(int id);
    void Add(Blog blog);
}
