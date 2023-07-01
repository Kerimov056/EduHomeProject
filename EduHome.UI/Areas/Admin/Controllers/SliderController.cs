using AutoMapper;
using EduHome.Core.Entities;
using EduHome.UI.Areas.Admin.Extension;
using EduHome.UI.Areas.Admin.ViewModel;
using EduHome.UI.ViewModel;
using EduHomeDataAccess.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.UI.Areas.Admin.Controllers;
[Area("Admin")]
public class SliderController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;
    private readonly IMapper _mapper;
    public SliderController(AppDbContext context, IWebHostEnvironment env, IMapper mapper)
    {
        _context = context;
        _env = env;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        HomeViewModel model = new()
        {
            sliders = await _context.Sliders.ToListAsync()
        };
        return View(model);
    }

    public async Task<IActionResult> Details(int id)
    {
        if (id == 0 || id == null)
        {
            return NotFound();
        }
        var slider = await _context.Sliders.FindAsync(id);
        if (slider is null)
        {
            return NotFound();
        }
        ViewBag.SliderId = slider.Id;
        HomeViewModel model = new()
        {
            sliders = await _context.Sliders.ToListAsync()
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
        if (!ModelState.IsValid)
        {
            return View(sliderViewModel);
        }
        if (!sliderViewModel.image.FormatFile("image"))
        {
            ModelState.AddModelError("image", "Select correct image format!");
            return View(sliderViewModel);
        }
        if (!sliderViewModel.image.FormatLength(100))
        {
            ModelState.AddModelError("image", "Size must be less than 100 kb");
            return View(sliderViewModel);
        }

        string filePath = await sliderViewModel.image.CopyFileAsync(_env.WebRootPath, "assets", "img", "slider");

        Slider slider = _mapper.Map<Slider>(sliderViewModel);
        slider.ImagePath = filePath;


        await _context.Sliders.AddAsync(slider);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }



    public async Task<IActionResult> Edit(int id)
    {
        if (id == 0 || id == null)
        {
            return NotFound();
        }
        var slider = await _context.Sliders.FindAsync(id);
        if (slider is null)
        {
            return NotFound();
        }

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
        if (!ModelState.IsValid)
        {
            return View(sliderViewModel);
        }
        if (!sliderViewModel.image.FormatFile("image"))
        {
            ModelState.AddModelError("image", "Select correct image format!");
            return View(sliderViewModel);
        }
        if (!sliderViewModel.image.FormatLength(100))
        {
            ModelState.AddModelError("image", "Size must be less than 100 kb");
            return View(sliderViewModel);
        }

        string filePath = await sliderViewModel.image.CopyFileAsync(_env.WebRootPath, "assets", "img", "slider");

        var slider = await _context.Sliders.FindAsync(id);
        slider.ImagePath = filePath;
        slider.Name = sliderViewModel.Name;
        slider.NameTwo = sliderViewModel.NameTwo;
        slider.Information = sliderViewModel.Information;

        _context.Entry(slider).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }


    public async Task<IActionResult> Delete(int id)
    {
        if (id == 0)
        {
            return NotFound();
        }
        Slider? slider = await _context.Sliders.FindAsync(id);
        if (slider is null)
        {
            return NotFound();
        }
        ViewBag.SliderID = slider.Id;
        HomeViewModel model = new()
        {
            sliders = await _context.Sliders.ToListAsync()
        };
        return View(model);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePost(int id)
    {
        var slider = await _context.Sliders.FindAsync(id);
        if (slider is null)
        {
            return NotFound();
        }

        _context.Sliders.Remove(slider);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
