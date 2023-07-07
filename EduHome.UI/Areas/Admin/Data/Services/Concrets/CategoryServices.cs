using EduHome.Core.Entities;
using EduHome.UI.Areas.Admin.Data.Services.Interfaces;
using EduHomeDataAccess.Database;
using Microsoft.EntityFrameworkCore;

namespace EduHome.UI.Areas.Admin.Data.Services.Concrets;

public class CategoryServices : ICategoryServices
{
    private readonly AppDbContext _context;
    public CategoryServices(AppDbContext context)
    {
        _context= context;
    }
    public async Task<IEnumerable<Categories>> GetCategory() => await _context.Categoriess.ToListAsync();
}
