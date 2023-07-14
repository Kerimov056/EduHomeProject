using EduHome.Core.Entities;
using EduHome.UI.ViewModel;
using EduHomeDataAccess.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.UI.Controllers;

public class CourseDetailsController : Controller
{
    private readonly AppDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly IHttpContextAccessor _contextAccessor;
    public CourseDetailsController(
        AppDbContext context,
        IHttpContextAccessor contextAccessor,
        UserManager<User> userManager)
    {
        _context = context;
        _contextAccessor = contextAccessor;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index(int id)
    {
        if (id == 0)  return NotFound();
        var cart = await _context.Coursess.FindAsync(id);
        if (cart is null)  return NotFound();

        ViewBag.Id = cart.Id;

        var comments = _context.CourseComments.Where(c => c.CoursesId == id).ToList();
        TempData["CommentsSum"] = comments.Count();

        HomeViewModel homeViewModel = new()
        {
            blogs = await _context.Blogs.ToListAsync(),
            courses = await _context.Coursess
                                .Include(c => c.CoursesDetails)
                                .Include(c => c.CourseComments)
                                .ThenInclude(u => u.User)
                                .ToListAsync(),
            categories = await _context.Categoriess.ToListAsync(),
        };
        return View(homeViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> AddComment(int id, HomeViewModel homeViewModel)
    {
        if (string.IsNullOrWhiteSpace(homeViewModel.Comments))
        {
            return Ok();
        } 

        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToAction("LogIn", "SiginUp");
        }


        if (User.Identity.IsAuthenticated)
        {
            User user = await _userManager.FindByNameAsync(User.Identity.Name);

            CourseComment comment = new CourseComment
            {
                CreatedDate = DateTime.Now,
                Comment = homeViewModel.Comments,
                CoursesId = id,
                UserId = user.Id
            };

            _context.CourseComments.Add(comment);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("Index", new { id = id });
    }

    public async Task<IActionResult> CommentsPage(int id)
    {
        if (id == 0) return NotFound();
        var cart = await _context.Coursess.FindAsync(id);
        if (cart == null)
        {
            return NotFound();
        }
        ViewBag.Id = cart.Id;

        var comments = _context.CourseComments.Where(c => c.CoursesId == id).ToList();
        ViewBag.CommentsSum = comments.Count();

        HomeViewModel homeViewModel = new()
        {
            blogs = await _context.Blogs.ToListAsync(),
            courses = await _context.Coursess
                                .Include(c => c.CoursesDetails)
                                .Include(c => c.CourseComments)
                                .ThenInclude(u => u.User)
                                .ToListAsync(),
            categories = await _context.Categoriess.ToListAsync(),
        };
        return View(homeViewModel);
    }

    //[HttpPost]
    //public async Task<IActionResult> CommentsPage(string user, int coursId)
    //{
    //    var comments = await _context.CourseComments.Where(c => c.CoursesId == coursId).ToListAsync();

    //    var FilterSearch = comments.Where(u => u.User.UserName == user);
    //    if (FilterSearch is null) return View(user);

    //    return View(FilterSearch);
    //}


    [HttpPost]
    private string GetUserId()
    {
        var user = _contextAccessor.HttpContext.User;
        string userId = _userManager.GetUserId(user);
        return userId;
    }

}




