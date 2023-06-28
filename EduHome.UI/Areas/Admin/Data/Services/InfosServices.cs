using EduHome.Core.Entities;
using EduHomeDataAccess.Database;
using Microsoft.EntityFrameworkCore;

namespace EduHome.UI.Areas.Admin.Data.Services;

public class InfosServices : IInfoService
{
    private readonly AppDbContext _context;
    public InfosServices(AppDbContext context)
    {
        _context = context;
    }

    public void Createe(Info info)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Info>> Delete(Info info)
    {
        var infoProduct = _context.Infos.Remove(info);
        await _context.SaveChangesAsync();
        return (IEnumerable<Info>)info;
    }

    public async Task<IEnumerable<Info>> GetInfo()
    {
        var InfoProduct = await _context.Infos.ToListAsync();
        return InfoProduct;
    }

    public async Task<IEnumerable<Info>> InfoCreate(Info info)
    {
        var InfoProduct = _context.Infos.Add(info);
        await _context.SaveChangesAsync();
        return (IEnumerable<Info>)InfoProduct;
    }

    public async Task<IEnumerable<Info>> Update(int id, Info info)
    {
        _context.Entry(info).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return (IEnumerable<Info>)info;
    }
}


//public async Task<IEnumerable<Info>> Createes(Info info)
//{
//    var InfoProduct = _context.Infos.Add(info);
//    await _context.SaveChangesAsync();
//    return (IEnumerable<Info>)InfoProduct;
//}
