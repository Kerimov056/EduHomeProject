using AutoMapper;
using EduHome.Core.Entities;
using EduHome.UI.Areas.Admin.Data.Services.Interfaces;
using EduHome.UI.Areas.Admin.ViewModel;
using EduHomeDataAccess.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace EduHome.UI.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "Admin")]
public class NoticeController : Controller
{
    private readonly INoticeService _noticeService;
    public NoticeController(INoticeService noticeServiceervice)
    {
        _noticeService = noticeServiceervice;
    }
    public async Task<IActionResult> Index()
    {
        var notice = await _noticeService.GetNotice();
        return View(notice);
    }

    public async Task<IActionResult> Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(NoticeViewModel noticeViewModel)
    {
        if (!ModelState.IsValid)  return View(noticeViewModel);
        await _noticeService.CreateAsync(noticeViewModel);
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Details(int id)
    {
        NoticeViewModel notice = await _noticeService.FindByIdAsync(id);
        return View(notice);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var product = await _noticeService.FindByIdAsync(id);
        if (product == null) return NotFound();
        return View(product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]

    public async Task<IActionResult> Edit(NoticeViewModel noticeViewModel,int id)
    {
        if (!ModelState.IsValid) return View(noticeViewModel);
        await _noticeService.EditAsync(id, noticeViewModel);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var product = await _noticeService.FindByIdAsync(id);
        if (product is null)  return NotFound();
        return View(product);
    }

    [HttpPost,ActionName("Delete")]
    [ValidateAntiForgeryToken]

    public async Task<IActionResult> DeletePost(int id)
    {
        await _noticeService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
