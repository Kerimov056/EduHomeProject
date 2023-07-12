using EduHome.Core.Entities;

namespace EduHome.UI.ShopServices.Interfaces;

public interface ISearchServices
{
    Task<IEnumerable<Courses>> GetCourses(string sTrem = "", int catagoryId = 0);
    Task<IEnumerable<Categories>> Categories();
}
