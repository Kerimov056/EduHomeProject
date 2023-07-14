using Microsoft.AspNetCore.Mvc;

namespace EduHome.UI.Contollers;

public class MyProfileController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
