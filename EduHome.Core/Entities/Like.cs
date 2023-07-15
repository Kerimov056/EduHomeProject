using EduHome.Core.Interface;

namespace EduHome.Core.Entities;

public class Like : IEntity
{
    public int Id { get ; set; }
    public int CourseCommentId { get; set; }
    public CourseComment CourseComment { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}
