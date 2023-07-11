using EduHome.UI.Areas.Admin.Data.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.UI.Areas.Admin.Controllers;

public class UsersController : Controller
{
    private readonly IUserServices _userServices;
    public UsersController(IUserServices userServices)
    {
        _userServices = userServices;
    }

    public async Task<IActionResult> Index()
    {
        var users = await _userServices.GetUserAsync();
        return View(users);
    }
}
