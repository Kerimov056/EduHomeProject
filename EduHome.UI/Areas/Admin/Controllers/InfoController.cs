using EduHome.Core.Entities;
using EduHome.UI.Areas.Admin.Data.Services.Interfaces;
using EduHome.UI.Areas.Admin.ViewModel;
using EduHomeDataAccess.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.UI.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "Admin")]
public class InfoController : Controller
{
    private readonly AppDbContext _context;
    private readonly IInfoService _infoService;
    public InfoController(AppDbContext context, IInfoService infoService)
    {
        _context = context;
        _infoService = infoService;
    }
    public async Task<IActionResult> Index()
    {
        var info = await _infoService.GetInfoAsync();
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
        if (!ModelState.IsValid)  return View(infoViewModel);
        await _infoService.CreateAsync(infoViewModel);
        return RedirectToAction(nameof(Index));
    }


    public async Task<IActionResult> Details(int id)
    {
        if (id == 0) return NotFound();
        var product = await _infoService.FindByIdAsync(id);
        if (product == null)   return NotFound();
        return View(product);
    }

    public async Task<IActionResult> Edit(int id)
    {
        if (id == 0) return NotFound();
        var product = _context.Infos.Find(id);
        if (product == null)  return NotFound();
        InfoViewModel ınfoViewModel = new()
        {
            Title = product.Name,
            Description = product.Description
        };
        return View(ınfoViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, InfoViewModel infoViewModel)
    {
        if (!ModelState.IsValid)  return View(infoViewModel);
        await _infoService.EditAsync(id,infoViewModel);
        return RedirectToAction(nameof(Index));
    }


    public async Task<IActionResult> Delete(int id)
    {
        if (id == 0) return NotFound();
        var product = _context.Infos.Find(id);
        if (product is null) return NotFound();
        return View(product);
    }

    [HttpPost,ActionName("Delete")]
    [ValidateAntiForgeryToken]

    public async Task<IActionResult> DeletePost(int id)
    {
        await _infoService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
