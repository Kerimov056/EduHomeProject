using EduHome.UI.Areas.Admin.Data.Services.Interfaces;
using EduHome.UI.Areas.Admin.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace EduHome.UI.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "Admin")]
public class DashboardController : Controller
{
    private readonly IBlogsService _blogService;

    public DashboardController(IBlogsService blogService)
    {
        _blogService = blogService;
    }

    public async Task<IActionResult> Index()
    {
        int sum = 0;
        var BSer = await _blogService.GetBlogs();
        foreach (var c in BSer) sum++;
        TempData["BlogeSum"] = sum;
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
        var blog = await _blogService.FindByIdAsync(id);
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


    public async Task<IActionResult> Delete(int id)
    {
        var blog = await _blogService.FindByIdAsync(id);
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

