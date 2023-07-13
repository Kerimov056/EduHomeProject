using EduHome.Core.Entities;
using EduHome.UI.Areas.Admin.Data.Exception;
using EduHome.UI.Areas.Admin.Data.Services.Interfaces;
using EduHomeDataAccess.Database;
using EduHomeDataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EduHome.UI.Areas.Admin.Data.Services.Concrets;

public class UserServices : IUserServices
{
    private readonly AppDbContext _appDbContext;
    public UserServices(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task DeleteAsync(string id)
    {
        if (id is null) throw new NullReferenceException();
        var ByUser = await _appDbContext.Users.FindAsync(id);
        if (ByUser is null) throw new NotFoundException("User Is Null");

        _appDbContext.Users.Remove(ByUser);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task<User> FindByIdAsync(string id)
    {
        if (id is null) throw new NullReferenceException();
        var user = await _appDbContext.Users.FindAsync(id);
        if (user is null) throw new NullReferenceException();
        return user;
    }

    public async Task<IEnumerable<User>> GetUserAsync() => await _appDbContext.Users.ToListAsync();
}
