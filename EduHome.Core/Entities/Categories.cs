using EduHome.Core.Interface;
using System.ComponentModel.DataAnnotations;

namespace EduHome.Core.Entities;

public class Categories : IEntity
{
    [Key]
    public int Id { get ; set ; }
    [Required,MaxLength(50)]
    public string Categorie { get; set; } = null!;
    public List<Courses> Courses { get; set; }
    public List<Event> Events { get; set; }
}
