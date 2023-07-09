using EduHome.Core.Interface;
using System.ComponentModel.DataAnnotations;

namespace EduHome.Core.Entities;

public class Viewer:IEntity
{
    public int Id { get ; set ; }
    [Required,MaxLength(23)]
    public string Name { get; set; } = null!;
    [Required,MaxLength(70)]
    public string Email { get; set; } = null!;
    [Required,MaxLength(100)]
    public string Subject { get; set; } = null!;
    [Required,MaxLength(3000)]
    public string Message { get; set; } = null!;
}
