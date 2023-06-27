﻿using AutoMapper;
using EduHome.Core.Entities;
using EduHome.UI.Areas.Admin.Extension;
using EduHome.UI.Areas.Admin.ViewModel;
using EduHome.UI.ViewModel;
using EduHomeDataAccess.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.UI.Areas.Admin.Controllers;
[Area("Admin")]
public class CoursesController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;
    private readonly IMapper _mapper;
    public CoursesController(AppDbContext context, IWebHostEnvironment env, IMapper mapper)
    {
        _context = context;
        _env = env;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        HomeViewModel homeViewModel = new()
        {
            blogs = await _context.Blogs.ToListAsync(),
            courses = await _context.Coursess.Include(c => c.Categories).ToListAsync()
        };
        return View(homeViewModel);
    }
    public async Task<IActionResult> Details(int id)
    {
        if (id == 0 || id == null)
        {
            return NotFound();
        }
        var cours = _context.Coursess.Find(id);
        if (cours is null)
        {
            return NotFound();
        }
        ViewBag.coursId = cours.Id;
        HomeViewModel homeViewModel = new()
        {
            blogs = await _context.Blogs.ToListAsync(),
            courses = await _context.Coursess.Include(c => c.CoursesDetails).ToListAsync()
        };
        return View(homeViewModel);
    }

    //----------------------------------------------------------------------------------


    public async Task<IActionResult> Create()
    {
        ViewBag.catagory = await _context.Categoriess.ToListAsync();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CourseFullDetailsViewModel courseFullDetailsViewModel, int CatagoryId)
    {
        if (!ModelState.IsValid)
        {
            return View(courseFullDetailsViewModel);
        }
        if (!courseFullDetailsViewModel.ImagePath.FormatFile("image"))
        {
            ModelState.AddModelError("ImagePath", "Select correct image format!");
            return View(courseFullDetailsViewModel);
        }
        if (!courseFullDetailsViewModel.ImagePath.FormatLength(100))
        {
            ModelState.AddModelError("ImagePath", "Size must be less than 100 kb");
            return View(courseFullDetailsViewModel);
        }


        var category = await _context.Categoriess.FindAsync(CatagoryId);
        if (category == null)
        {
            ModelState.AddModelError("CatagoryId", "Invalid category selected!");
            ViewBag.catagory = await _context.Categoriess.ToListAsync();
            return View(courseFullDetailsViewModel);
        }

        string filePath = await courseFullDetailsViewModel.ImagePath.CopyFileAsync(_env.WebRootPath, "assets", "img", "course");
        Courses courses = new Courses
        {
            ImagePath = filePath,
            Name = courseFullDetailsViewModel.Cours,
            Descripton = courseFullDetailsViewModel.Description,
            CategoriesId = CatagoryId,
            CoursesDetails = new CoursesDetails
            {
                AboutCours = courseFullDetailsViewModel.AboutCours,
                AboutCoursDescription = courseFullDetailsViewModel.AboutCoursDescription,
                ToApply = courseFullDetailsViewModel.ToApply,
                ToApplyDescription = courseFullDetailsViewModel.ToApplyDescription,
                Certification = courseFullDetailsViewModel.Certification,
                CertificationDescription = courseFullDetailsViewModel.CertificationDescription,
                Starts = courseFullDetailsViewModel.Starts,
                Month = courseFullDetailsViewModel.Month,
                Hours = courseFullDetailsViewModel.Hours,
                Level = courseFullDetailsViewModel.Level,
                Language = courseFullDetailsViewModel.Language,
                Students = courseFullDetailsViewModel.Students,
                Assesments = courseFullDetailsViewModel.Assesments,
                CourseFee = courseFullDetailsViewModel.CourseFee
            }
        };


        _context.Coursess.Add(courses);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }



    public async Task<IActionResult> Edit(int id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        ViewBag.catagory = await _context.Categoriess.ToListAsync();
        Courses course = await _context.Coursess.Include(c => c.CoursesDetails).FirstOrDefaultAsync(n => n.Id == id);
        if (course == null)
        {
            return NotFound();
        }

        var courseViewModel = _mapper.Map<CourseFullDetailsViewModel>(course);

        return View(courseViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CourseFullDetailsViewModel courseViewModel, int CatagoryId)
    {
        if (!ModelState.IsValid)
        {
            return View(courseViewModel);
        }

        if (id == null)
        {
            return BadRequest();
        }

        var category = await _context.Categoriess.FindAsync(CatagoryId);
        if (category == null)
        {
            ModelState.AddModelError("CatagoryId", "Invalid category selected!");
            ViewBag.catagory = await _context.Categoriess.ToListAsync();
            return View(courseViewModel);
        }

        Courses course = await _context.Coursess.Include(c => c.CoursesDetails).FirstOrDefaultAsync(n => n.Id == id);
        if (course == null)
        {
            return NotFound();
        }

        _mapper.Map(courseViewModel, course);
        course.CategoriesId = CatagoryId;

        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }


    public async Task<IActionResult> Delete(int id)
    {
        if (id == 0 || id == null)
        {
            return NotFound();
        }

        Courses course = await _context.Coursess.Include(c => c.CoursesDetails).FirstOrDefaultAsync(n => n.Id == id);
        if (course == null)
        {
            return NotFound();
        }

        if (id == 0 || id == null)
        {
            return NotFound();
        }
        var cours = _context.Coursess.Find(id);
        if (cours is null)
        {
            return NotFound();
        }
        ViewBag.coursId = cours.Id;
        HomeViewModel homeViewModel = new()
        {
            blogs = await _context.Blogs.ToListAsync(),
            courses = await _context.Coursess.Include(c => c.CoursesDetails).ToListAsync()
        };
        return View(homeViewModel);
    }

    [HttpPost,ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePost(int id)
    {
        var cours = await _context.Coursess.FindAsync(id);
        if (cours is null)
        {
            return NotFound();
        }
        var coursDetails = cours.CoursesDetails;
        _context.Coursess.Remove(cours);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }
}