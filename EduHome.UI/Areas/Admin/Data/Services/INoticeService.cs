using EduHome.Core.Entities;

namespace EduHome.UI.Areas.Admin.Data.Services;

public interface INoticeService
{
    Task<IEnumerable<Notice>> GetNotices();
    Task<IEnumerable<Notice>> Delete(Notice notice);
    Task<IEnumerable<Notice>> Update(int id,Notice notice);
}
