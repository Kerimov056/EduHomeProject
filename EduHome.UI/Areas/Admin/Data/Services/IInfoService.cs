using EduHome.Core.Entities;

namespace EduHome.UI.Areas.Admin.Data.Services;

public interface IInfoService
{
    Task<IEnumerable<Info>> GetInfo();
    Task<IEnumerable<Info>> Delete(Info info);
    Task<IEnumerable<Info>> Update(int id,Info info);
    Task<IEnumerable<Info>> InfoCreate(Info info);
    void Createe(Info info);
}
