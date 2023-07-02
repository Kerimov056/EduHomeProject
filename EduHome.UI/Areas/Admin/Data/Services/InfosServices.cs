using EduHome.Core.Entities;
using EduHome.UI.Areas.Admin.Data.Base;
using EduHomeDataAccess.Database;
using Microsoft.EntityFrameworkCore;

namespace EduHome.UI.Areas.Admin.Data.Services;

public class InfosServices : IInfoService
{
    private readonly IEntityBaseRepository<Info> _InfoRepsitory;
	public InfosServices(IEntityBaseRepository<Info> InfoRepsitory)
	{
        _InfoRepsitory = InfoRepsitory;
    }

    public async Task<Info> GetByIdAsync(int id) => await _InfoRepsitory.GetByIdAsync(id);
    public async Task<IEnumerable<Info>> GetInfo() => await _InfoRepsitory.GetAllAsync();
    public  Task DeleteAsync(int id) => _InfoRepsitory.DeleteAsync(id);
    public async Task<Info> CreateAsync(Info info)
    {
         await _InfoRepsitory.AddAsync(info);
         return info;
    }
    public async Task<Info> EditAsync(int id, Info info)
    {
        await _InfoRepsitory.UpdateAsync(id, info);
        return info;
    }
}

