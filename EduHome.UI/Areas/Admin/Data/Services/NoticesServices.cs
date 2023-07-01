using EduHome.Core.Entities;
using EduHome.UI.Areas.Admin.Data.Base;
using EduHomeDataAccess.Database;
using Microsoft.EntityFrameworkCore;

namespace EduHome.UI.Areas.Admin.Data.Services;

public class NoticesServices : INoticeService
{
   private readonly IEntityBaseRepository<Notice> _Noticerepository;
	public NoticesServices(IEntityBaseRepository<Notice> Noticerepository)
	{
		_Noticerepository= Noticerepository;
	}

    public async Task<Notice> CreateAsync(Notice notice)
    {
         await _Noticerepository.AddAsync(notice);
         return notice;
    }

    public Task DeleteAsync(int id)
    {
       return  _Noticerepository.DeleteAsync(id);
    }

    public async Task<Notice> EditAsync(int id, Notice notice)
    {
        await _Noticerepository.UpdateAsync(id, notice);
        return notice;
    }

    public async Task<IEnumerable<Notice>> GetNotice()
    {
        return await _Noticerepository.GetAllAsync();
    }

    public async Task<Notice> GetByIdAsync(int id)
    {
        return await _Noticerepository.GetByIdAsync(id);
    }
}
