using EduHome.Core.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduHome.Core.Entities;

public class TeacherDetails : IEntity
{
    public int Id { get ; set ; }
    [Required,MaxLength(400)]
    public string Description { get; set; } = null!;
    [Required,MaxLength(30)]
    public string Degree { get; set; } = null!;
    [Required]
    public int Experince { get; set; }
    [Required,MaxLength(50)]
    public string Hobbies { get; set; } = null!;
    [Required,MaxLength(35)]
    public string Facultry { get; set; } = null!;
    [Required,MaxLength(125)]
    public string Email { get; set; } = null!;

    [Required,MaxLength(40)]
    public string Phone { get; set; }= null!;
    [Required,MaxLength(50)]
    public string Skaype { get; set; } = null!;
    [Required]
    public  decimal LanguageDegree { get; set; }
    [Required]
    public  decimal TeamLeaderDegree { get; set; }
    [Required]
    public  decimal DevelopmentDegree { get; set; }
    [Required]
    public  decimal DesignDegree { get; set; }
    [Required]
    public  decimal InnovationDegree { get; set; }
    [Required]
    public  decimal CommunicationDegree { get; set; }

    public int TeacherId { get; set; }
    [ForeignKey("TeacherId")]
    public Teacher Teacher { get; set; }
}
