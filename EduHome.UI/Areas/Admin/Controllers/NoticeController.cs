﻿using AutoMapper;
using EduHome.Core.Entities;
using EduHome.UI.Areas.Admin.Data.Services;
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
    private readonly AppDbContext _context;
    private readonly INoticeService _service;
    public NoticeController(AppDbContext context, INoticeService service)
    {
        _context = context;
        _service = service;
    }
    public async Task<IActionResult> Index()
    {
        var notice = await _service.GetNotice();
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
        if (!ModelState.IsValid)
        {
            return View(noticeViewModel);
        }
        Notice notice = new();
        notice.Description = noticeViewModel.Description;
        notice.Date_Time = DateTime.Now;

        await _service.CreateAsync(notice);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int id)
    {
        if (id == 0 || id == null)
        {
            return NotFound();
        }
        var product = _context.Notices.Find(id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }


    public async Task<IActionResult> Edit(int id)
    {
        if (id ==0 || id==null)
        {
            return NotFound();
        }
        var product = _context.Notices.Find(id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]

    public async Task<IActionResult> Edit(Notice notice,int id)
    {
        if (!ModelState.IsValid)
        {
            return View(notice);
        }
        if (id == null)
        {
            return BadRequest();
        }

        Notice? notice1 = await _context.Notices.AsNoTracking().FirstOrDefaultAsync(n=>n.Id==id);

        if (notice1 is null)
        {
            return NotFound();
        }
        notice.Date_Time= DateTime.Now;
        await _service.EditAsync(id,notice);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }


    public async Task<IActionResult> Delete(int id)
    {
        if (id == 0 || id == null)
        {
            return NotFound();
        }
        var product = _context.Notices.Find(id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }

    [HttpPost,ActionName("Delete")]
    [ValidateAntiForgeryToken]

    public async Task<IActionResult> DeletePost(int id)
    {
        var notice = _context.Notices.Find(id);
        if (notice is null)
        {
            return NotFound();
        }

        await _service.DeleteAsync(id);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

}
