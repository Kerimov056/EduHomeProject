using EduHome.Core.Entities;
using EduHomeDataAccess.Database;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

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

    public async Task<IEnumerable<Blog>> Delete(Blog blog)
    {
        var deleteProduct = _context.Blogs.Remove(blog);
        await _context.SaveChangesAsync();
        return (IEnumerable<Blog>)deleteProduct;
    }

    public Task Edit(int id, Blog blog)
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

    public async Task<IEnumerable<Blog>> Update(int id, Blog newBlog)
    {
        _context.Entry(newBlog).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return(IEnumerable<Blog>)newBlog;
    }

    //Task<IEnumerable<Blog>> IBlogsService.Update(int id, Blog newBlog)
    //{
    //    throw new NotImplementedException();
    //}
}
