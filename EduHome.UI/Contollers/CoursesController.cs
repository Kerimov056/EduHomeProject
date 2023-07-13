using EduHome.Core.Entities;
using EduHome.UI.Areas.Admin.Data.Exception;
using EduHome.UI.ShopServices.Interfaces;
using EduHome.UI.ViewModel;
using EduHomeDataAccess.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.UI.Contollers;

public class CoursesController : Controller
{
    private readonly AppDbContext _context;
    private readonly ILogger<CoursesController> _logger;
    private readonly ISearchServices _searchServices;
    public CoursesController(
        AppDbContext context,
        ILogger<CoursesController> logger,
        ISearchServices searchServices)
    {
        _context = context;
        _logger = logger;
        _searchServices = searchServices;
    }

    public async Task<IActionResult> Index(string sTrem = "", int catagoryId = 0)
    {
        IEnumerable<Courses> cours = await _searchServices.GetCourses(sTrem, catagoryId);
        IEnumerable<Categories> categories = await _searchServices.Categories();

        HomeViewModel model = new HomeViewModel
        {
            courses = cours,
            categories = categories,
            blogs = await _context.Blogs.ToListAsync(),
            sTrem = sTrem,
            catagoryId= catagoryId
        };

        return View(model);
    }


    public async Task<IActionResult> AddWishList(int id)
    {
        if (id == 0) return NotFound();

        Courses courses = await _context.Coursess
                                .Include(x => x.Categories)
                                .Include(x => x.CoursesDetails)
                                .FirstAsync(x => x.Id == id);


        if (courses is null) throw new NotFoundException("Courses Is Null");

        string? value = HttpContext.Request.Cookies["WishList"];
        List<CoursesCartVM> cartsCookies = new List<CoursesCartVM>();

        if (value is null)
        {
            HttpContext.Response.Cookies
                       .Append("WishList",System.Text.Json.JsonSerializer.Serialize(cartsCookies));
        }
        else
        {
            cartsCookies = System.Text.Json.JsonSerializer.Deserialize<List<CoursesCartVM>>(value);
        }

        CoursesCartVM? cartVm = cartsCookies.Find(x => x.Id == id);
        if (cartVm is null)
        {
            cartsCookies.Add(new CoursesCartVM()
            {
                Id = id,
                Count = 1,
                Name = courses.Name,
                ImagePath = courses.ImagePath,
                Price = (int) courses.CoursesDetails.CourseFee
            });
        }
        else
        {
            cartVm.Count += 1;
        }

        HttpContext.Response.Cookies.Append("WishList",System.Text.Json.JsonSerializer.Serialize(cartsCookies),new CookieOptions()
        {
            MaxAge = TimeSpan.FromDays(25)
        });
        return RedirectToAction(nameof(Index));
    }
}
