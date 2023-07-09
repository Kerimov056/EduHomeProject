using EduHome.Core.Entities;
using EduHomeDataAccess.Database;
using EduHomeDataAccess.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using EduHome.Core.Interface;

namespace EduHomeDataAccess.Repository;

public class CoursRepository<T> : ICoursRepository<T> where T : class, IEntity, new()
{
    private readonly AppDbContext _context;

    public CoursRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();
    public async Task<T> GetByIdAsync(int id) => await _context.Set<T>().FindAsync(id);
    public async Task AddAsync(T entity) => await _context.Set<T>().AddAsync(entity);
    public async Task UpdateAsync(int id, T entity)
    {
        EntityEntry entry = _context.Entry<T>(entity);
        entry.State = EntityState.Modified;
    }
    public async Task DeleteAsync(int id)
    {
        var entity = await _context.Set<T>().FindAsync(id);
        EntityEntry entry = _context.Entry<T>(entity);
        entry.State = EntityState.Deleted;
    }
}
