using EduHome.Core.Entities;
using EduHome.UI.Areas.Admin.Data.Services.Interfaces;
using EduHome.UI.Areas.Admin.ViewModel;
using EduHome.UI.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.UI.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "Admin")]

public class TeacheraController : Controller
{
    private readonly ITeacherService _teacherService;
    public TeacheraController(ITeacherService teacherService)
    {
        _teacherService = teacherService;
    }

    public async Task<IActionResult> Index()
    {
        int sum = 0;
        var teacher = await _teacherService.GetTeacher();
        foreach (var item in teacher) sum++;
        TempData["TeacherSum"] = sum;

        HomeViewModel model = new HomeViewModel
        {
            teachers = await _teacherService.GetTeacher(),
        };
        return View(model);
    }

    public async Task<IActionResult> Details(int id)
    {
        if (!ModelState.IsValid) return NotFound();
        Teacher? teacher = await _teacherService.FindByTeacherAsync(id);
        ViewBag.TeacherId = teacher.Id;
        HomeViewModel model = new HomeViewModel
        {
            teachers = await _teacherService.GetTeacher(),
        };
        return View(model);
    }

    public async Task<IActionResult> Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]

    public async Task<IActionResult> Create(TeacherViewModel teacherViewModel)
    {
        if (!ModelState.IsValid)  return View(teacherViewModel);
        try
        {
            await _teacherService.CreateAsync(teacherViewModel);
            return RedirectToAction(nameof(Index));
        }
        catch (ArgumentException ex)
        {
            ModelState.AddModelError("Image", ex.Message);
            return View(teacherViewModel);
        }
       
    }

    public async Task<IActionResult> Edit(int id)
    {
        var teacher = await _teacherService.FindByTeacherAsync(id);
        TeacherViewModel teacherView = new TeacherViewModel
        {
            //Image = teacher.ImagePath
            Name = teacher.Name,
            Posistion = teacher.Posistion,
            Degree = teacher.teacherDetails.Degree,
            Experince = teacher.teacherDetails.Experince,
            Hobbies = teacher.teacherDetails.Hobbies,
            Facultry = teacher.teacherDetails.Facultry,
            Email = teacher.teacherDetails.Email,
            Phone = teacher.teacherDetails.Phone,
            Skype = teacher.teacherDetails.Skaype,
            Language = teacher.teacherDetails.LanguageDegree,
            Development = teacher.teacherDetails.DevelopmentDegree,
            Design = teacher.teacherDetails.DesignDegree,
            Innovation = teacher.teacherDetails.InnovationDegree,
            Communication = teacher.teacherDetails.CommunicationDegree,
            AboutMe = teacher.teacherDetails.Description
        };
        return View(teacherView);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]

    public async Task<IActionResult> Edit(int id,TeacherViewModel teacherViewModel)
    {
        if (!ModelState.IsValid)  return View(teacherViewModel);
        await _teacherService.EditAsync(id,teacherViewModel);
        return RedirectToAction(nameof(Index));
    }


    public async Task<IActionResult> Delete(int id )
    {
        Teacher? teacher = await _teacherService.FindByTeacherAsync(id);
        ViewBag.TeId = teacher.Id;
        HomeViewModel model = new HomeViewModel
        {
            teachers = await _teacherService.GetTeacher()
        };
        return View(model);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePost(int id)
    {
        await _teacherService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
