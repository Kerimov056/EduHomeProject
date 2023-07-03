using AutoMapper;
using EduHome.Core.Entities;
using EduHome.UI.Areas.Admin.Data.Base.CoursesRepository;
using EduHome.UI.Areas.Admin.Data.Services;
using EduHome.UI.Areas.Admin.Extension;
using EduHome.UI.Areas.Admin.ViewModel;
using EduHome.UI.ViewModel;
using EduHomeDataAccess.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace EduHome.UI.Areas.Admin.Controllers;
[Area("Admin")]
public class CoursesController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;
    private readonly IMapper _mapper;
    private readonly ICoursesServices _coruseService;
    public CoursesController(AppDbContext context, IWebHostEnvironment env, IMapper mapper, ICoursesServices coruseService)
    {
        _context = context;
        _env = env;
        _mapper = mapper;
        _coruseService = coruseService;
    }

    public async Task<IActionResult> Index()
    {
        //int sum = 0;
        //var course = await _context.Coursess.ToListAsync();
        //foreach (var c in course)
        //{
        //    sum++;
        //}

        //TempData["CourseSum"] = sum;

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
        if (category is null)
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


        await _coruseService.CreateAsync(courses);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }



    public async Task<IActionResult> Edit(int id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        ViewBag.catagory = await _context.Categoriess.ToListAsync();
        Courses? course = await _context.Coursess.Include(c => c.CoursesDetails).FirstOrDefaultAsync(n => n.Id == id);
        if (course == null)
        {
            return NotFound();
        }


        CourseFullDetailsViewModel viewModel = new CourseFullDetailsViewModel
        {
            //ImagePath = course.ImagePath,
            Description = course.Descripton,
            Cours = course.Name,
            CategorId = course.CategoriesId,
            AboutCours = course.CoursesDetails.AboutCours,
            AboutCoursDescription = course.CoursesDetails.AboutCoursDescription,
            ToApply = course.CoursesDetails.ToApply,
            ToApplyDescription = course.CoursesDetails.ToApplyDescription,
            Certification = course.CoursesDetails.Certification,
            CertificationDescription = course.CoursesDetails.CertificationDescription,
            Starts = course.CoursesDetails.Starts,
            Month = course.CoursesDetails.Month,
            Hours = course.CoursesDetails.Hours,
            Level = course.CoursesDetails.Level,
            Language = course.CoursesDetails.Language,
            Students = course.CoursesDetails.Students,
            Assesments = course.CoursesDetails.Assesments,
            CourseFee = course.CoursesDetails.CourseFee
        };


        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CourseFullDetailsViewModel viewModel, int CategorId)
    {
        if (id == 0)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }
        if (!viewModel.ImagePath.FormatFile("image"))
        {
            ModelState.AddModelError("ImagePath", "Select correct image format!");
            return View(viewModel);
        }
        if (!viewModel.ImagePath.FormatLength(100))
        {
            ModelState.AddModelError("ImagePath", "Size must be less than 100 kb");
            return View(viewModel);
        }

        Courses? course = await _context.Coursess.Include(c => c.CoursesDetails).FirstOrDefaultAsync(c => c.Id == id);
        if (course == null)
        {
            return NotFound();
        }

        
        
        string filePath = await viewModel.ImagePath.CopyFileAsync(_env.WebRootPath, "assets", "img", "course");
        


        var category = await _context.Categoriess.FindAsync(CategorId);
        if (category == null)
        {
            ModelState.AddModelError("CatagoryId", "Invalid category selected!");
            ViewBag.catagory = await _context.Categoriess.ToListAsync();
            return View(viewModel);
        }



        course.ImagePath = filePath;
        course.Descripton = viewModel.Description;
        course.Name = viewModel.Cours;
        course.CategoriesId = CategorId;
        course.CoursesDetails.AboutCours = viewModel.AboutCours;
        course.CoursesDetails.AboutCoursDescription = viewModel.AboutCoursDescription;
        course.CoursesDetails.ToApply = viewModel.ToApply;
        course.CoursesDetails.ToApplyDescription = viewModel.ToApplyDescription;
        course.CoursesDetails.Certification = viewModel.Certification;
        course.CoursesDetails.CertificationDescription = viewModel.CertificationDescription;
        course.CoursesDetails.Starts = viewModel.Starts;
        course.CoursesDetails.Month = viewModel.Month;
        course.CoursesDetails.Hours = viewModel.Hours;
        course.CoursesDetails.Level = viewModel.Level;
        course.CoursesDetails.Language = viewModel.Language;
        course.CoursesDetails.Students = viewModel.Students;
        course.CoursesDetails.Assesments = viewModel.Assesments;
        course.CoursesDetails.CourseFee = viewModel.CourseFee;


        await _coruseService.UpdateAsync(id,course);
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

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePost(int id)
    {
        var cours = await _context.Coursess.FindAsync(id);
        if (cours is null)
        {
            return NotFound();
        }
        var coursDetails = cours.CoursesDetails;

        await _coruseService.DeleteAsync(id);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}



