using EduHomeDataAccess.Database;
using Microsoft.AspNetCore.Mvc;

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
		return View();
	}
}
