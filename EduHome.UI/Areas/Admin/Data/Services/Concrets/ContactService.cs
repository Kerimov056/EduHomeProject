using EduHome.Core.Entities;
using EduHome.UI.Areas.Admin.Data.Exception;
using EduHome.UI.Areas.Admin.Data.Services.Interfaces;
using EduHomeDataAccess.Database;
using EduHomeDataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EduHome.UI.Areas.Admin.Data.Services.Concrets;

public class ContactService : IContactService
{
    private readonly IEntityBaseRepository<Viewer> _entityBaseRepository;
    private readonly AppDbContext _context;
    public ContactService(IEntityBaseRepository<Viewer> entityBaseRepository, AppDbContext context)
    {
        _entityBaseRepository = entityBaseRepository;
        _context = context;
    }

    public async Task DeleteAsync(int id)
    {
        if (id==0) throw new NullReferenceException();
        Viewer viewer = await _context.Viewers.FindAsync(id);
        if (viewer is null) throw new NotFoundException("Viewer is Null");
        _context.Viewers.Remove(viewer);
        await _context.SaveChangesAsync(); 
    }

    public async Task<Viewer> FindByIdAsync(int id)
    {
        if (id == 0) throw new NullReferenceException();
        Viewer viewer = await _context.Viewers.FindAsync(id);
        if (viewer is null) throw new NotFoundException("Viewer Is Null");
        return viewer;
    }

    public async Task<IEnumerable<Viewer>> GetViewer() => await _entityBaseRepository.GetAllAsync();
}
