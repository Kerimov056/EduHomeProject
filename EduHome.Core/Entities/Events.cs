using EduHome.Core.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduHome.Core.Entities;

public class Events : IEntity
{
    public int Id { get ; set ; }
    [Required]
    public DateTime DateTime { get; set ; }
    [Required,MaxLength(46)]
    public string Name { get; set; } = null!;
    [Required,MaxLength(70)]
    public string Location { get; set; } = null!;

    //Relationships
    public EventsDetails Details { get; set; }

   
    public List<Events_Speakers> Events_Speakers { get; set; }
}
