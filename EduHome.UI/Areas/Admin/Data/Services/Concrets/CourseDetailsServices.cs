using EduHome.Core.Entities;
using EduHome.UI.Areas.Admin.Data.Services.Interfaces;
using EduHomeDataAccess.Database;

namespace EduHome.UI.Areas.Admin.Data.Services.Concrets;

public class CourseDetailsServices : ICourseDetailsServices
{
	private readonly AppDbContext _context;
	public CourseDetailsServices(AppDbContext context)
	{
		_context= context;
	}
	public async Task<CoursesDetails> FindByIdAsync(int id) => await _context.CoursesDetailss.FindAsync(id);
}
