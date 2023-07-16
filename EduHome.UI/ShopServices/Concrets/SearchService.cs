using EduHome.Core.Entities;
using EduHome.UI.ShopServices.Interfaces;
using EduHomeDataAccess.Database;
using Microsoft.EntityFrameworkCore;

namespace EduHome.UI.ShopServices.Concrets;

public class SearchService : ISearchServices
{
    private readonly AppDbContext _context;
    public SearchService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Categories>> Categories()
    {
        return await _context.Categoriess.ToListAsync();
        //var categories = await _context.Categoriess.ToListAsync();
        //if (catagoryId>0)
        //{
        //    categories = categories.Where(a => a.Id == catagoryId).ToList();
        //}
        //return categories;
    }
    public async Task<IEnumerable<Courses>> GetCourses(string sTrem = "", int catagoryId = 0)
    {
        //sTrem = sTrem.ToLower();
        IEnumerable<Courses> courses = await (from course in _context.Coursess
                                              join category in _context.Categoriess on course.CategoriesId equals category.Id
                                              join coursesDetails in _context.CoursesDetailss on course.Id equals coursesDetails.CoursesId
                                              where string.IsNullOrWhiteSpace(sTrem) || (course != null && course.Name.ToLower().StartsWith(sTrem))
                                              select new Courses
                                              {
                                                  Id = course.Id,
                                                  Name = course.Name,
                                                  ImagePath = course.ImagePath,
                                                  Descripton = course.Descripton,
                                                  CategoriesId = course.CategoriesId,
                                                  CoursesDetails = coursesDetails

                                              }).ToListAsync();

        if (catagoryId > 0)
        {
            courses = courses.Where(a => a.CategoriesId == catagoryId).ToList();
        }
        return courses;
    }

    //public async Task<IEnumerable<User>> GetUser(string user = "")
    //{
    //    user = user.ToLower();
    //    IEnumerable<User> User = await (from USER in _context.Users
    //                                    join comment in _context.CourseComments
    //                                    on USER.CourseComments equals comment.UserId
    //                                    where string.IsNullOrWhiteSpace(user) || (USER != null && USER.UserName.ToLower().StartsWith(user))
    //                                    select new User
    //                                    {

    //                                        CourseComments = USER.CourseComments,
    //                                        us

    //                                    }).ToListAsync();

    //    return User;
    //}
}