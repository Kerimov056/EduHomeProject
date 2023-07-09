//using EduHome.Core.Entities;
//using EduHome.Core.Interface;
//using EduHomeDataAccess.Database;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.ChangeTracking;

//namespace EduHome.UI.Areas.Admin.Data.Base;

//public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntity, new()
//{
//    private readonly AppDbContext _context;
//    public EntityBaseRepository(AppDbContext context)
//    {
//        _context = context;
//    }

//    public async Task<IEnumerable<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();
//    public async Task<T> GetByIdAsync(int id) => await _context.Set<T>().FindAsync(id);
//    public async Task AddAsync(T entity) => await _context.Set<T>().AddAsync(entity);
//    public async Task UpdateAsync(int id, T entity)
//    {
//        EntityEntry entry = _context.Entry<T>(entity);
//        entry.State = EntityState.Modified;
//    }
//    public async Task DeleteAsync(int id)
//    {
//        var entity = await _context.Set<T>().FindAsync(id);
//        EntityEntry entry = _context.Entry<T>(entity);
//        entry.State = EntityState.Deleted;
//    }
//}
