using AutoMapper;
using EduHome.Core.Entities;
using EduHome.UI.Areas.Admin.Data.Base;
using EduHome.UI.Areas.Admin.Data.Exception;
using EduHome.UI.Areas.Admin.Data.Services.Interfaces;
using EduHome.UI.Areas.Admin.Extension;
using EduHome.UI.Areas.Admin.ViewModel;
using EduHomeDataAccess.Database;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace EduHome.UI.Areas.Admin.Data.Services.Concrets;

public class BlogsServices : IBlogsService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _env;

    public BlogsServices(AppDbContext contex, IMapper mapper, IWebHostEnvironment env)
    {
        _context = contex;
        _mapper = mapper;
        _env = env;
    }
    public async Task CreateAsync(BlogViewModel blogViewModel)
    {
        if (blogViewModel is null)
        {
            throw new NullReferenceException("Blog is null");
        }

        if (!blogViewModel.ImagePath.FormatFile("image"))
        {
            throw new ArgumentException("Select correct image format!");
        }

        if (!blogViewModel.ImagePath.FormatLength(1000))
        {
            throw new ArgumentException("Size must be less than 1000 kb");
        }

        string filePath = await blogViewModel.ImagePath.CopyFileAsync(_env.WebRootPath, "assets", "img", "slider");
        Blog blog = _mapper.Map<Blog>(blogViewModel);
        blog.ImagePath = filePath;
        blog.Data_Time = DateTime.Now;
        blog.Description = blogViewModel.Decs;


        await _context.AddAsync(blog);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        if (id == 0)
        {
            throw new NullReferenceException("Id is null, 0");
        }

        var blog = await _context.Blogs.FindAsync(id);
        if (blog is null)
        {
            throw new NotFoundException("Blog is not Found");
        }

        _context.Remove(blog);
        await _context.SaveChangesAsync();
    }

    public async Task EditAsync(int Id, BlogViewModel blogViewModel)
    {
        if (blogViewModel is null)
        {
            throw new NullReferenceException("Blog is null");
        }

        var blog = await _context.Blogs.FindAsync(Id);
        if (blog is null)  throw new NullReferenceException("Blog is nUll");

        if (blogViewModel.ImagePath is not null)
        {
            if (!blogViewModel.ImagePath.FormatFile("image"))
            {
                throw new ArgumentException("Select correct image format!");
            }

            if (!blogViewModel.ImagePath.FormatLength(1000))
            {
                throw new ArgumentException("Size must be less than 1000 kb");
            }
            
            string filePath = await blogViewModel.ImagePath.CopyFileAsync(_env.WebRootPath, "assets", "img", "slider");
            blog.ImagePath = filePath;
        }

        blog.Data_Time = DateTime.Now;
        blog.Description = blogViewModel.Decs;
        blog.MessageNum = blogViewModel.MessageNum;
        blog.PersonName = blogViewModel.PersonName;
        blog.Name = blogViewModel.Name;

        _context.Entry(blog).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public Task<BlogViewModel> FindByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Blog>> GetBlogs() => await _context.Blogs.ToListAsync();
}
