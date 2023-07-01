using EduHome.Core.Interface;
using EduHomeDataAccess.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EduHome.UI.Areas.Admin.Data.Base.CoursesRepository;

public class CoursesBaseRepository<T>: EntityBaseRepository<T>, ICoursesBaseRepository<T> where T : class,IEntity, new()
{   
    private readonly AppDbContext _context;
    public CoursesBaseRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
    public async Task<IEnumerable<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();

    public async Task AddAsync(T entity) =>  await _context.Set<T>().AddAsync(entity);
    public async Task<T> GetByIdAsync(int id) => await _context.Set<T>().FindAsync(id);

    public async Task DeleteAsync(int id)
    {
        var course = await _context.Set<T>().FindAsync(id);
        EntityEntry entry = _context.Entry<T>(course);
        entry.State = EntityState.Modified;
    }

    public async Task UpdateAsync(int id, T entity)
    {
        EntityEntry entry = _context.Entry<T>(entity);
        entry.State = EntityState.Deleted;
    }
}
