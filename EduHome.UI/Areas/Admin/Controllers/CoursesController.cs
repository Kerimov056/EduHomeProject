using EduHome.Core.Entities;
using EduHome.UI.Areas.Admin.Data.Services.Interfaces;
using EduHome.UI.Areas.Admin.ViewModel;
using EduHome.UI.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace EduHome.UI.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "Admin")]
public class CoursesController : Controller
{
    private readonly ICoursesServices _coruseService;
    private readonly ICategoryServices _categoryServices;
    public CoursesController(ICoursesServices coruseService, ICategoryServices categoryServices)
    {
        _coruseService = coruseService;
        _categoryServices = categoryServices;
    }

    public async Task<IActionResult> Index()
    {
        HomeViewModel homeViewModel = new()
        {
            courses = await _coruseService.GetCourses()
        };
        return View(homeViewModel);
    }
    public async Task<IActionResult> Details(int id)
    {
        var cours = await _coruseService.FindByIdAsync(id);
        ViewBag.coursId = cours.Id;
        HomeViewModel homeViewModel = new()
        {
            courses = await _coruseService.GetCourses()
        };
        return View(homeViewModel);
    }


    public async Task<IActionResult> Create()
    {
        ViewBag.catagory = await _categoryServices.GetCategory();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CourseFullDetailsViewModel courseFullDetailsViewModel, int CatagoryId)
    {
        if (!ModelState.IsValid) return View(courseFullDetailsViewModel);
        await _coruseService.CreateAsync(courseFullDetailsViewModel, CatagoryId);
        return RedirectToAction("Index");
    }



    public async Task<IActionResult> Edit(int id, CourseFullDetailsViewModel viewModel)
    {
        ViewBag.catagory = await _categoryServices.GetCategory();
        Courses? course = await _coruseService.FindByIdAsync(id);
        if (course is null) return RedirectToAction(nameof(Index));
        var model = await _coruseService.GetEdit(id,viewModel);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CourseFullDetailsViewModel viewModel, int CategorId)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.category = await _categoryServices.GetCategory();
            return View(viewModel);
        }
        await _coruseService.UpdateAsync(id, viewModel, CategorId);
        return RedirectToAction("Index");
    }




    public async Task<IActionResult> Delete(int id)
    {
        var cours = await _coruseService.FindByIdAsync(id);
        ViewBag.coursId = cours.Id;
        HomeViewModel homeViewModel = new()
        {
            courses = await _coruseService.GetCourses()
        };
        return View(homeViewModel);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePost(int id)
    {
        await _coruseService.DeleteAsync(id);
        return RedirectToAction("Index");
    }
}



