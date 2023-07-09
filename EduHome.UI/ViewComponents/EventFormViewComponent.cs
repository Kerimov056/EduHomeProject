using EduHome.Core.Entities;
using EduHome.UI.ViewModel;
using EduHomeDataAccess.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.UI.ViewComponents;

public class EventFormViewComponent: ViewComponent
{
    private readonly AppDbContext _context;
	public EventFormViewComponent(AppDbContext context)
	{
		_context= context;
	}

	public async Task<IViewComponentResult> InvokeAsync()
	{
        ViewerViewModel viewerViewModel = new ViewerViewModel();
        return View(viewerViewModel);
    }

    [HttpPost]
    public async Task<IViewComponentResult> SubmitForm(ViewerViewModel viewerViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View("InvokeAsync", viewerViewModel);
        }

        Viewer viewer = new Viewer
        {
            Name = viewerViewModel.Name,
            Email = viewerViewModel.Email,
            Subject = viewerViewModel.Subject,
            Message = viewerViewModel.Message
        };
       

        await _context.Viewers.AddAsync(viewer);
        await _context.SaveChangesAsync();

        return View("Success"); // Başarılı sonucu temsil eden bir view döndürün
    }

}
