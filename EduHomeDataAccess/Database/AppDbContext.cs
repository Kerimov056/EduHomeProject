using Microsoft.EntityFrameworkCore;

namespace EduHomeDataAccess.Database;

public class AppDbContext:DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options):base(options){}

}
