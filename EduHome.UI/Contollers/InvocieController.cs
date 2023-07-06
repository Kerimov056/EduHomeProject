using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.UI.Contollers;

[Authorize(Roles ="Member,Admin")]
public class InvocieController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
