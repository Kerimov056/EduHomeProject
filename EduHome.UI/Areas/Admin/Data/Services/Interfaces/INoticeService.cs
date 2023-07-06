using EduHome.Core.Entities;

namespace EduHome.UI.Areas.Admin.Data.Services.Interfaces;

public interface INoticeService
{
    Task<IEnumerable<Notice>> GetNotice();
    Task<Notice> CreateAsync(Notice notice);
    Task<Notice> EditAsync(int id, Notice notice);
    Task<Notice> GetByIdAsync(int id);
    Task DeleteAsync(int id);
}
