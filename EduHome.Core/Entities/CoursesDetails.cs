using EduHome.Core.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduHome.Core.Entities;

public class CoursesDetails : IEntity
{
    public int Id { get ; set ; }
    [Required]
    public DateTime Starts { get; set; }
    [Required]
    public int Month { get; set; }
    [Required]
    public int Hours { get; set; }
    [Required,MaxLength(25)]
    public string Level { get; set; } = null!;
    [Required, MaxLength(22)]
    public string Language { get; set; } = null!;
    [Required]
    public int Students { get; set; }
    [Required, MaxLength(20)]
    public string Assesments { get; set; } = null!;
    [Required]
    public int CourseFee { get; set; }


    // Foreign Key
    public int CoursesId { get; set; }
    [ForeignKey("CoursesId")]
    public Courses Courses { get; set; }
}
