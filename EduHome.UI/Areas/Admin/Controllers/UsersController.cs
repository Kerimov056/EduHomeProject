using EduHome.UI.Areas.Admin.Data.Services.Interfaces;
using EduHome.UI.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.UI.Areas.Admin.Controllers;

[Area("Admin")]
public class UsersController : Controller
{
    private readonly IUserServices _userServices;
    public UsersController(IUserServices userServices)
    {
        _userServices = userServices;
    }

    public async Task<IActionResult> Index()
    {
        HomeViewModel users = new()
        {
            users = await _userServices.GetUserAsync()
        };
        return View(users);
    }

    public async Task<IActionResult> Details(string id)
    {
        var Byuser = await _userServices.FindByIdAsync(id);
        ViewBag.ByUser = Byuser.Id;
        HomeViewModel user = new()
        {
            users = await _userServices.GetUserAsync()
        };
        return View(user);
    }


    public async Task<IActionResult> Delete(string id)
    {
        var user = await _userServices.FindByIdAsync(id);
        ViewBag.ByUserDel = user.Id;
        HomeViewModel model = new()
        {
            users = await _userServices.GetUserAsync()
        };
        return View(model);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]

    public async Task<IActionResult> DeletePost(string id)
    {
        await _userServices.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
