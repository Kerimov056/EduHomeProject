using AutoMapper;
using EduHome.Core.Entities;
using EduHome.UI.Areas.Admin.Data.Base;
using EduHome.UI.Areas.Admin.Data.Exception;
using EduHome.UI.Areas.Admin.Data.Services.Interfaces;
using EduHome.UI.Areas.Admin.ViewModel;
using EduHomeDataAccess.Database;
using Microsoft.EntityFrameworkCore;

namespace EduHome.UI.Areas.Admin.Data.Services.Concrets;

public class InfosServices : IInfoService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    public InfosServices(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task CreateAsync(InfoViewModel infoViewModel)
    {
        if (infoViewModel is null) throw new NullReferenceException("Info Is Null");
        Info ınfo = new()
        {
            Name = infoViewModel.Title,
            Description = infoViewModel.Description
        };
        await _context.AddAsync(ınfo);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        if (id == 0) throw new NotFoundException("Info Is Null");
        var info = await _context.Infos.FindAsync(id);
        if (info is null) throw new NullReferenceException("Info Is Null");
         _context.Entry(info).State = EntityState.Deleted;
        await _context.SaveChangesAsync();
    }

    public async Task EditAsync(int id, InfoViewModel infoViewModel)
    {
        if (id == 0) throw new NullReferenceException("Info is Null");
        var info = await _context.Infos.FindAsync(id);
        if (info is null) throw new NotFoundException("Info Is Null");
        _context.Infos.Update(info);
        await _context.SaveChangesAsync();
    }

    public async Task<Info> FindByIdAsync(int id)
    {
        var info =  await _context.Infos.FindAsync(id);
        return info;
    }

    public async Task<IEnumerable<Info>> GetInfoAsync() => await _context.Infos.ToListAsync();
}

