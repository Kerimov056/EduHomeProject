using AutoMapper;
using EduHome.Core.Entities;
using EduHome.UI.Areas.Admin.Data.Services.Interfaces;
using EduHome.UI.Areas.Admin.Extension;
using EduHome.UI.Areas.Admin.ViewModel;
using EduHome.UI.ViewModel;
using EduHomeDataAccess.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.UI.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "Admin")]
public class SliderController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;
    private readonly IMapper _mapper;
    private readonly ISliderServices _sliderServices;
    public SliderController(AppDbContext context, IWebHostEnvironment env, IMapper mapper, ISliderServices sliderServices)
    {
        _context = context;
        _env = env;
        _mapper = mapper;
        _sliderServices = sliderServices;
    }

    public async Task<IActionResult> Index()
    {
        HomeViewModel model = new()
        {
            sliders = await _sliderServices.GetSliders()
        };
        return View(model);
    }

    public async Task<IActionResult> Details(int id)
    {
        var slider = await _sliderServices.FindByIdAsync(id);
        if (slider is null) return NotFound();
        ViewBag.SliderId = slider.Id;
        HomeViewModel model = new()
        {
            sliders = await _sliderServices.GetSliders()
        };
        return View(model);
    }

    public async Task<IActionResult> Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]

    public async Task<IActionResult> Create(SliderViewModel sliderViewModel)
    {
        if (!ModelState.IsValid)  return View(sliderViewModel);
        await _sliderServices.CreateAsync(sliderViewModel);
        return RedirectToAction(nameof(Index));
    }



    public async Task<IActionResult> Edit(int id)
    {
        var slider = await _sliderServices.FindByIdAsync(id);
        if (slider is null) return NotFound();
        SliderViewModel model = new()
        {
            //image = slider.ImagePath
            Name = slider.Name,
            NameTwo = slider.NameTwo,
            Information = slider.Information
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, SliderViewModel sliderViewModel)
    {
        if (!ModelState.IsValid)  return View(sliderViewModel);
        await _sliderServices.EditAsync(id, sliderViewModel); 
        return RedirectToAction(nameof(Index));
    }


    public async Task<IActionResult> Delete(int id)
    {
        Slider? slider = await _sliderServices.FindByIdAsync(id);
        if (slider is null)  return NotFound();
        ViewBag.SliderID = slider.Id;
        HomeViewModel model = new()
        {
            sliders = await _sliderServices.GetSliders()
        };
        return View(model);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePost(int id)
    {
        await _sliderServices.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
