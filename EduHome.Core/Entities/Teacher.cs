using EduHome.Core.Interface;
using System.ComponentModel.DataAnnotations;

namespace EduHome.Core.Entities;

public class Teacher : IEntity
{
    public int Id { get; set; }
    public string ImagePath { get; set; }
    [Required, MaxLength(22)]
    public string Name { get; set; } = null!;
    [Required, MaxLength(30)]
    public string Posistion { get; set; } = null!;
    public TeacherDetails teacherDetails { get; set; }
}
