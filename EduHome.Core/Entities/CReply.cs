using EduHome.Core.Interface;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduHome.Core.Entities;

public class CReply : IEntity
{
    public int Id { get; set; }
    public string Reply { get; set; }
    public DateTime DateTime { get; set; } = DateTime.Now;
    [ForeignKey("User")]
    public string UserId { get; set; }
    public User User { get; set; } = null!;
    [ForeignKey("CourseComment")]
    public int CourseCommentId { get; set; }
    public CourseComment CourseComment { get; set; }
}
