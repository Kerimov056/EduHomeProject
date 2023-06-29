using EduHome.Core.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduHome.Core.Entities;

public class EventsDetails : IEntity
{
    public int Id { get; set; }
    [Required,MaxLength(300)]
    public string ImagePath { get; set; } = null!;
    [Required,MaxLength(1300)]
    public string Description { get; set; }= null!;

    //Relationships
    public int EventsId { get; set; }
    [ForeignKey("EventsId")]
    public Events Events { get; set; }
}
