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
        if (id == 0)
        {
            return NotFound();
        }
        var cart = _context.Coursess.Find(id);
        if (cart == null)
        {
            return NotFound();
        }
        ViewBag.Id = cart.Id;

        var fullComents = await _context.CourseComments.ToListAsync();
        foreach (var item in fullComents)
        {
            if (item.CoursesId == id)
            {
                ViewBag.ByComment = item.Comment;
            }
        }

        HomeViewModel homeViewModel = new()
        {
            blogs = await _context.Blogs.ToListAsync(),
            courses = await _context.Coursess
                                .Include(c => c.CoursesDetails)
                                .Include(c => c.CourseComments)
                                .ToListAsync(),
            categories = await _context.Categoriess.ToListAsync(),
        };
        return View(homeViewModel);
    }

    public async Task<IActionResult> AddComment(string message, int courseId)
    {
        if (string.IsNullOrWhiteSpace(message))
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
                Comment = message,
                CoursesId = courseId,
                UserId = user.Id
            };

            _context.CourseComments.Add(comment);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("Index", new { id = courseId });
    }


    private string GetUserId()
    {
        var user = _contextAccessor.HttpContext.User;
        string userId = _userManager.GetUserId(user);
        return userId;
    }

}




