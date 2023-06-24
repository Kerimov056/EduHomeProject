using Microsoft.AspNetCore.Mvc;

namespace EduHome.UI.Contollers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
