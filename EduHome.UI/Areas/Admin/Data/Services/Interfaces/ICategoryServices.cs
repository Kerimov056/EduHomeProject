using EduHome.Core.Entities;

namespace EduHome.UI.Areas.Admin.Data.Services.Interfaces;

public interface ICategoryServices
{
    Task<IEnumerable<Categories>> GetCategory();
}
