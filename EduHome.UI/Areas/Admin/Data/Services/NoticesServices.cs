using EduHome.Core.Entities;
using EduHomeDataAccess.Database;
using Microsoft.EntityFrameworkCore;

namespace EduHome.UI.Areas.Admin.Data.Services;

public class NoticesServices : INoticeService
{
    private readonly AppDbContext _context;
    public NoticesServices(AppDbContext context)
    {
        _context= context;
    }
    public async Task<IEnumerable<Notice>> Delete(Notice notice)
    {
        var noticeProduct = _context.Notices.Remove(notice);
        await _context.SaveChangesAsync();
        return (IEnumerable<Notice>)noticeProduct;
    }

    public async Task<IEnumerable<Notice>> GetNotices()
    {
        var product = await _context.Notices.ToListAsync();
        return product;
    }

    public async Task<IEnumerable<Notice>> Update(int id, Notice notice)
    {
        _context.Entry(notice).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return (IEnumerable<Notice>)notice;
    }
}
