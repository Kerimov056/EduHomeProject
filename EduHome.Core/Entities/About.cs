using EduHome.Core.Interface;
using System.ComponentModel.DataAnnotations;

namespace EduHome.Core.Entities;

public class About: IEntity
{
    public int Id { get ; set ; }
    public string Imagepath { get; set; }

    [Required]
    [StringLength(50)]
    public string Title { get; set; }
    [Required]
    [StringLength(450)]
    public string Description { get; set; }
}
