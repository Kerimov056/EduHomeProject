using AutoMapper;
using EduHome.Core.Entities;
using EduHome.UI.Areas.Admin.AutoMapper;
using EduHome.UI.Areas.Admin.Extension;
using EduHome.UI.Areas.Admin.ViewModel;
using EduHomeDataAccess.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.UI.Areas.Admin.Controllers;
[Area("Admin")]
public class DashboardController : Controller
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _env;

    public DashboardController(AppDbContext context, IMapper mapper, IWebHostEnvironment env)
    {
        _context = context;
        _mapper = mapper;
        _env = env;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Blogs.ToListAsync());
    }

    public async Task<IActionResult> Create()
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
            ModelState.AddModelError("Image", "Select correct image format!");
            return View(blogViewModel);
        }
        if (!blogViewModel.ImagePath.FormatLenght(100))
        {
            ModelState.AddModelError("Image", "Size must be less than 100 kb");
            return View(blogViewModel);
        }

        //D:\work2\Asp.net MVC\EduHome\EduHome.UI\wwwroot\assets\img\slider\
        string filePath = await blogViewModel.ImagePath.CopyFileAsync(_env.WebRootPath, "assets", "img", "slider");

        Blog blogg = _mapper.Map<Blog>(blogViewModel);
        blogg.ImagePath = filePath;
        await _context.Blogs.AddAsync(blogg);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }


    public async Task<IActionResult> Edit(int id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        var blog = _context.Blogs.Find(id);
        if (blog == null)
        {
            return NotFound();
        }
        return View();
    }

    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> Edit(int id,Blog blog)
    //{
    //    if (id == null || id==0)
    //    {
    //        return NotFound();
    //    }
    //    if (ModelState.IsValid)
    //    {
    //        return NotFound();
    //    }

    //    var blogMap = _mapper.Map<BlogViewModel>(blog);
    //    blog.ImagePath = blogMap.ImagePath.ToString();
    //    blog.Name= blogMap.Name;
    //    blog.PersonName=blogMap.PersonName;
    //    blog.Data_Time=blogMap.Data_Time;
    //    blog.MessageNum=blogMap.MessageNum;

    //    _context.Blogs.Update(blog);
    //    _context.SaveChanges();
    //    return RedirectToAction("Index");
    //}


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Blog blog)
    {
        if (id == null || id == 0)
        {
            return BadRequest();
        }
        if (ModelState.IsValid)
        {
            return NotFound();
        }

        Blog? blog1 = await _context.Blogs.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);

        if (blog1 == null)
        {
            return NotFound();
        }
        _context.Entry(blog).State = EntityState.Modified;
        _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        var blog = _context.Blogs.Find(id);
        if (blog is null)
        {
            return NotFound();
        }
        return View();
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]

    public IActionResult DeletePost(int id)
    {
        var blog = _context.Blogs.Find(id);
        if (blog is null)
        {
            return NotFound();
        }
        _context.Blogs.Remove(blog);
        _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}
