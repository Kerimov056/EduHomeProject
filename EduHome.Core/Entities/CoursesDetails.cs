using EduHome.Core.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduHome.Core.Entities;

public class CoursesDetails : IEntity
{
    public int Id { get ; set ; }
    //[Required,MaxLength(50)]
    //public string AboutCours { get; set; } = null!;
    //[Required, MaxLength(350)]
    //public string AboutCoursDescription { get; set; } = null!;
    //[Required, MaxLength(50)]
    //public string ToApply { get; set; } = null!;
    //[Required, MaxLength(350)]
    //public string ToApplyDescription { get; set;} = null!;
    //[Required, MaxLength(50)]
    //public string Certification { get; set; } = null!;
    //[Required, MaxLength(400)]
    //public string CertificationDescription { get; set;} = null!;
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
