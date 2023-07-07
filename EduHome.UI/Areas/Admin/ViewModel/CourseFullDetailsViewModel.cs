using System.ComponentModel.DataAnnotations;

namespace EduHome.UI.Areas.Admin.ViewModel;

public class CourseFullDetailsViewModel
{
    public IFormFile ImagePath { get; set; } = null!;
    public string Cours { get; set; } = null!;
    public string Description { get; set; } = null!;
    //[Required, MaxLength(50)]
    //public string AboutCours { get; set; } = null!;
    //[Required, MaxLength(350)]
    //public string AboutCoursDescription { get; set; } = null!;
    //[Required, MaxLength(50)]
    //public string ToApply { get; set; } = null!;
    //[Required, MaxLength(350)]
    //public string ToApplyDescription { get; set; } = null!;
    //[Required, MaxLength(50)]
    //public string Certification { get; set; } = null!;
    //[Required, MaxLength(400)]
    //public string CertificationDescription { get; set; } = null!;
    public DateTime Starts { get; set; }
    [Required]
    public int Month { get; set; }  
    [Required]
    public int Hours { get; set; }
    [Required, MaxLength(25)]
    public string Level { get; set; } = null!;
    [Required, MaxLength(22)]
    public string Language { get; set; } = null!;
    [Required]
    public int Students { get; set; }
    [Required, MaxLength(20)]
    public string Assesments { get; set; } = null!;
    [Required]
    public int CourseFee { get; set; }

    public int CategorId { get; set; }
}
