using EduHome.Core.Entities;
using EduHome.UI.Areas.Admin.Data.Base;
using EduHomeDataAccess.Database;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace EduHome.UI.Areas.Admin.Data.Services;

public class BlogsServices : IBlogsService
{
    private readonly IEntityBaseRepository<Blog> _blogRepository;
    public BlogsServices(IEntityBaseRepository<Blog> blogRepository)
    {
      _blogRepository= blogRepository;
    }

    public async Task<IEnumerable<Blog>> GetBlogs() => await _blogRepository.GetAllAsync();
    public async Task<Blog> GetByIdAsync(int id) => await _blogRepository.GetByIdAsync(id);
    public Task DeleteAsync(int id) =>  _blogRepository.DeleteAsync(id);
    public async Task<Blog> CreateAsync(Blog blog)
    {
        await _blogRepository.AddAsync(blog);
        return blog;
    }
    public async Task<Blog> EditAsync(int id, Blog blog)
    {
        await _blogRepository.UpdateAsync(id,blog);
        return blog;
    }
}
