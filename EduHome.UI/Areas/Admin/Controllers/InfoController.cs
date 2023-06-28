using EduHome.Core.Entities;
using EduHome.UI.Areas.Admin.Data.Services;
using EduHome.UI.Areas.Admin.ViewModel;
using EduHomeDataAccess.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.UI.Areas.Admin.Controllers;
[Area("Admin")]
public class InfoController : Controller
{
    private readonly AppDbContext _context;
    private readonly IInfoService _service;
    public InfoController(AppDbContext context, IInfoService service)
    {
        _context = context;
        _service = service;
    }
    public async Task<IActionResult> Index()
    {
        var info = await _service.GetInfo();
        return View(info);
    }

    public async Task<IActionResult> Create(int id)
    {
        return View(); 
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(InfoViewModel infoViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(infoViewModel);
        }
        Info info = new();
        info.Name = infoViewModel.Title;
        info.Description = infoViewModel.Description;

        _context.Infos.Add(info);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }


    public async Task<IActionResult> Details(int id)
    {
        if (id == 0 || id == null)
        {
            return NotFound();
        }
        var product = _context.Infos.Find(id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }

    public async Task<IActionResult> Edit(int id)
    {
        if (id == 0 || id == null)
        {
            return NotFound();
        }
        var product = _context.Infos.Find(id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Info info)
    {
        if (!ModelState.IsValid)
        {
            return View(info);
        }
        if (id == null)
        {
            return BadRequest();
        }

        Info? info1 = await _context.Infos.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);

        if (info1 == null)
        {
            return NotFound();
        }

        _service.Update(id,info);
        return RedirectToAction(nameof(Index));
    }


    public async Task<IActionResult> Delete(int id)
    {
        if (id == 0 || id == null)
        {
            return NotFound();
        }
        var product = _context.Infos.Find(id);
        if (product is null)
        {
            return NotFound();
        }
        return View(product);
    }

    [HttpPost,ActionName("Delete")]
    [ValidateAntiForgeryToken]

    public async Task<IActionResult> DeletePost(int id)
    {
        var product = _context.Infos.Find(id);
        if (id == null)
        {
            return NotFound();
        }

        _service.Delete(product);
        return RedirectToAction(nameof(Index));
    }
}
