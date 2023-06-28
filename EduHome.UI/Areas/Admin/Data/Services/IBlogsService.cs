using EduHome.Core.Entities;

namespace EduHome.UI.Areas.Admin.Data.Services;

public interface IBlogsService
{
    Task<IEnumerable<Blog>> GetBlogs();
    Blog GetBlog(int id);
    void Add(Blog blog);
    Blog Update(int id,Blog newBlog);
    void Delete(int id);
}
