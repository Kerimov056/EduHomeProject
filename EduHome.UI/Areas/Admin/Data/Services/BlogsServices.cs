using EduHome.Core.Entities;
using EduHomeDataAccess.Database;
using Microsoft.EntityFrameworkCore;

namespace EduHome.UI.Areas.Admin.Data.Services;

public class BlogsServices : IBlogsService
{
    private readonly AppDbContext _context;

    public BlogsServices(AppDbContext context)
    {
        _context = context;
    }
    public void Add(Blog blog)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Blog GetBlog(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Blog>> GetBlogs()
    {
        var result =await _context.Blogs.ToListAsync();
        return result;
    }

    public Blog Update(int id, Blog newBlog)
    {
        throw new NotImplementedException();
    }
}
