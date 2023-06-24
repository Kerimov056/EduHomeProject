using AutoMapper;
using EduHome.Core.Entities;
using EduHome.UI.Areas.Admin.AutoMapper;
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

    public DashboardController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
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
        if (!blogViewModel.ImagePath.ContentType.Contains("image"))
        {
            ModelState.AddModelError("Image", "duz yaz gijdila");
            return View(blogViewModel);
        }
        if (blogViewModel.ImagePath.Length/1024>100)
        {
            ModelState.AddModelError("Image", "duz yaz gijdila");
            return View(blogViewModel);
        }

        //D:\work2\Asp.net MVC\EduHome\EduHome.UI\wwwroot\assets\img\slider\
        string file_name = Guid.NewGuid().ToString()+blogViewModel.ImagePath.FileName;
        string filePath = Path.Combine("assets", "img", "slider", file_name);
        string path = Path.Combine(_env.WebRootPath, filePath);
        using (FileStream fileStream = new FileStream(path,FileMode.CreateNew))
        {
            await blogViewModel.ImagePath.CopyToAsync(fileStream);
        }
        
        Blog blogg = _mapper.Map<Blog>(blogViewModel);
        blogg.ImagePath= filePath;
        await _context.Blogs.AddAsync(blogg);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
