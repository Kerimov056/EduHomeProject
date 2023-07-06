using AutoMapper;
using EduHome.Core.Entities;
using EduHome.UI.Areas.Admin.AutoMapper;
using EduHome.UI.Areas.Admin.Data.Services.Interfaces;
using EduHome.UI.Areas.Admin.Extension;
using EduHome.UI.Areas.Admin.ViewModel;
using EduHome.UI.ViewModel;
using EduHomeDataAccess.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using System.Reflection.Metadata;
using System.Text;

namespace EduHome.UI.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "Admin")]
public class DashboardController : Controller
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _env;
    private readonly IBlogsService _blogService;

    public DashboardController(AppDbContext context, IMapper mapper, IWebHostEnvironment env, IBlogsService blogService)
    {
        _context = context;
        _mapper = mapper;
        _env = env;
        _blogService = blogService;
    }

    public async Task<IActionResult> Index()
    {
        int sum = 0;
        var blog = await _context.Blogs.ToListAsync();
        foreach (var c in blog)
        {
            sum++;
        }
        TempData["BlogeSum"] = sum;


        var BSer = await _blogService.GetBlogs();
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
        if (!ModelState.IsValid) return View(blogViewModel);

        try
        {
            await _blogService.CreateAsync(blogViewModel);
            return RedirectToAction(nameof(Index));
        }
        catch (ArgumentException ex)
        {
            ModelState.AddModelError("ImagePath", ex.Message);
            return View(blogViewModel);
        }
    }


    public async Task<IActionResult> Edit(int id)
    {
        if (id == 0)  return NotFound();
        var blog = _context.Blogs.Find(id);
        if (blog is null) return NotFound();
        BlogViewModel model = new()
        {
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
        if (!ModelState.IsValid) return View(blogViewModel);
        await _blogService.EditAsync(id, blogViewModel);
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
        await _blogService.DeleteAsync(id);
        return RedirectToAction("Index");
    }
    ////--------------------------------------------------------------------------
}

