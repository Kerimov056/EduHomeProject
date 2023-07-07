using EduHome.Core.Entities;
using EduHome.UI.Areas.Admin.ViewModel;

namespace EduHome.UI.Areas.Admin.Data.Services.Interfaces;

public interface INoticeService
{
    Task<IEnumerable<Notice>> GetNotice();
    Task CreateAsync(NoticeViewModel NoticeViewModel);
    Task EditAsync(int id, NoticeViewModel NoticeViewModel);
    Task<NoticeViewModel> FindByIdAsync(int id);
    Task DeleteAsync(int id);
}
