using AutoMapper;
using EduHome.Core.Entities;
using EduHome.UI.Areas.Admin.AutoMapper;
using EduHome.UI.Areas.Admin.Data.Services;
using EduHome.UI.Areas.Admin.Extension;
using EduHome.UI.Areas.Admin.ViewModel;
using EduHome.UI.ViewModel;
using EduHomeDataAccess.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace EduHome.UI.Areas.Admin.Controllers;
[Area("Admin")]
public class DashboardController : Controller
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _env;
    private readonly IBlogsService _service;

    public DashboardController(AppDbContext context, IMapper mapper, IWebHostEnvironment env, IBlogsService service)
    {
        _context = context;
        _mapper = mapper;
        _env = env;
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        var BSer = await _service.GetBlogs();
        return View(BSer);
    }


    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]

    public async Task<IActionResult> Create(BlogViewModel blogViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(blogViewModel);
        }
        if (!blogViewModel.ImagePath.FormatFile("image"))
        {
            ModelState.AddModelError("ImagePath", "Select correct image format!");
            return View(blogViewModel);
        }
        if (!blogViewModel.ImagePath.FormatLength(100))
        {
            ModelState.AddModelError("ImagePath", "Size must be less than 100 kb");
            return View(blogViewModel);
        }
        //D:\work2\Asp.net MVC\EduHome\EduHome.UI\wwwroot\assets\img\slider\
        string filePath = await blogViewModel.ImagePath.CopyFileAsync(_env.WebRootPath, "assets", "img", "slider");
        Blog blogg = _mapper.Map<Blog>(blogViewModel);
        blogg.ImagePath = filePath;
        blogg.Data_Time = DateTime.Now;
        blogg.Description = blogViewModel.Decs;


        await _service.CreateAsync(blogg);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }


    public async Task<IActionResult> Edit(int id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        var blog = _context.Blogs.Find(id);
        if (blog is null)
        {
            return NotFound();
        }
        BlogViewModel model = new()
        {
            //ImagePath = blog.filePath,
            Data_Time = DateTime.Now,
            Decs = blog.Description,
            MessageNum = blog.MessageNum,
            PersonName = blog.PersonName,
            Name = blog.Name
        };
        return View(model);
}

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(int id, BlogViewModel blogViewModel)
{
    if (id == 0)
    {
        return BadRequest();
    }
    if (!ModelState.IsValid)
    {
        return View(blogViewModel);
    }
    if (!blogViewModel.ImagePath.FormatFile("image"))
    {
        ModelState.AddModelError("ImagePath", "Select correct image format!");
        return View(blogViewModel);
    }
    if (!blogViewModel.ImagePath.FormatLength(100))
    {
        ModelState.AddModelError("ImagePath", "Size must be less than 100 kb");
        return View(blogViewModel);
    }

    Blog? blog = await _context.Blogs.FindAsync(id);

    if (blog == null)
    {
        return NotFound();
    }

    string filePath = await blogViewModel.ImagePath.CopyFileAsync(_env.WebRootPath, "assets", "img", "slider");

    blog.ImagePath = filePath;
    blog.Data_Time = DateTime.Now;
    blog.Description = blogViewModel.Decs;
    blog.MessageNum = blogViewModel.MessageNum;
    blog.PersonName = blogViewModel.PersonName;
    blog.Name = blogViewModel.Name;

    await _service.EditAsync(id, blog);
    await _context.SaveChangesAsync();
    return RedirectToAction("Index");
}



public IActionResult Delete(int id)
{
    var blog = _context.Blogs.Find(id);
    if (blog is null)
    {
        return NotFound();
    }
    return View(blog);
}

[HttpPost, ActionName("Delete")]
[ValidateAntiForgeryToken]

public async Task<IActionResult> DeletePost(int id)
{
    var blog = _context.Blogs.Find(id);
    if (blog is null)
    {
        return NotFound();
    }
    //_context.Blogs.Remove(blog);
    await _service.DeleteAsync(id);
    _context.SaveChangesAsync();
    return RedirectToAction("Index");
}
    ////--------------------------------------------------------------------------
}

