using EduHome.Core.Interface;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduHome.Core.Entities;

public class Like : IEntity
{
    public int Id { get ; set; }
    public DateTime DateTime { get; set; }
    public int like_sum { get; set; }
    public int CourseCommentId { get; set; }
    public CourseComment CourseComment { get; set; }
    [ForeignKey("User")]
    public string UserId { get; set; }
    public User User { get; set; }
}
