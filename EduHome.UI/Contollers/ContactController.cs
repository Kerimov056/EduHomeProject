using EduHome.Core.Entities;
using EduHome.UI.ViewModel;
using EduHomeDataAccess.Database;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.UI.Contollers;

public class ContactController : Controller
{
    private readonly AppDbContext _context;
    public ContactController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]

    public async Task<IActionResult> Index(ViewerViewModel viewModel)
    {
        if (!ModelState.IsValid) return View(viewModel);
        if (viewModel is null) return NotFound();
        Viewer viewer = new()
        {
            Name= viewModel.Name,
            Email= viewModel.Email,
            Subject= viewModel.Subject,
            Message= viewModel.Message
        };
        
        await _context.Viewers.AddAsync(viewer);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
