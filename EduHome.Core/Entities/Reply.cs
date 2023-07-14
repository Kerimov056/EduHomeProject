using EduHome.Core.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduHome.Core.Entities;

public class Reply:IEntity
{
    [Key]
    public int Id { get ; set ; }
    [Required]
    public string Text { get; set ; }
    public DateTime dateTime { get; set ; } = DateTime.Now;
    public int UserId { get; set; }

    [ForeignKey("UserId")]
    public virtual User User { get; set; }
    public int CommentId { get; set; }

    [ForeignKey("CommentId")]
    public virtual CourseComment Comment { get; set; }
}
