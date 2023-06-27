using EduHome.Core.Interface;
using System.ComponentModel.DataAnnotations;

namespace EduHome.Core.Entities;

public class Blog : IEntity
{
    public int Id { get ; set ; }
    [Required,MaxLength(400)]
    public string ImagePath { get; set; } = null!;
    [Required, MaxLength(90)]
    public string Name { get ; set ; } = null!;
    [Required, MaxLength(30)]
    public string PersonName { get; set; } = null!;
    [Required,MaxLength(1200)]
    public string Description { get; set; } = null!;
    public DateTime Data_Time { get; set; }
    public int MessageNum { get; set; }
}
