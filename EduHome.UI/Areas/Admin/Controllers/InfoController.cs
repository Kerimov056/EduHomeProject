using Microsoft.AspNetCore.Mvc;

namespace EduHome.UI.Areas.Admin.Controllers;

public class InfoController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
