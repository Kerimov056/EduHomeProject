using AutoMapper;
using EduHome.Core.Entities;
using EduHome.UI.Areas.Admin.Data.Base;
using EduHome.UI.Areas.Admin.Data.Exception;
using EduHome.UI.Areas.Admin.Data.Services.Interfaces;
using EduHome.UI.Areas.Admin.ViewModel;
using EduHomeDataAccess.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EduHome.UI.Areas.Admin.Data.Services.Concrets;

public class NoticesServices : INoticeService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    public NoticesServices(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task CreateAsync(NoticeViewModel NoticeViewModel)
    {
        if (NoticeViewModel is null) throw new NotFoundException("Notice is Null");
        Notice notice = new()
        {
            Description = NoticeViewModel.Description,
            Date_Time = DateTime.Now
        };
        await _context.Notices.AddAsync(notice);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        if (id == 0) throw new NullReferenceException("Notice Is Null");
        var notice = await _context.Notices.FindAsync(id);
        if (notice is null) throw new NotFoundException("Notice Is Null");
        _context.Entry(notice).State = EntityState.Deleted;
        await _context.SaveChangesAsync();
    }

    public async Task EditAsync(int id, NoticeViewModel NoticeViewModel)
    {
        if (id == 0) throw new NotFoundException("Notice is Null");
        var notice = await _context.Notices.FindAsync(id);
        notice.Description = NoticeViewModel.Description;
        notice.Date_Time = DateTime.Now;
        _context.Notices.Update(notice);
        await _context.SaveChangesAsync();
    }

    public async Task<NoticeViewModel> FindByIdAsync(int id)
    {
        if (id == 0) throw new NotFoundException("Notice is Null");
        var notice = await _context.Notices.FindAsync(id);
        if (notice is null) throw new NotFoundException("Notice is Null");
        NoticeViewModel noticeViewModel = new()
        {
            Datatime = notice.Date_Time,
            Description = notice.Description
        };
        return noticeViewModel;
    }

    public async Task<IEnumerable<Notice>> GetNotice() => await _context.Notices.ToListAsync();
}
