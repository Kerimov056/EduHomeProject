using EduHome.Core.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduHome.Core.Entities;

public class EventDetails : IEntity
{
    public int Id { get; set; }
    [Required, MaxLength(300)]
    public string ImagePath { get; set; } = null!;
    [Required, MaxLength(50)]
    public string Title { get; set; } = null!;
    [Required, MaxLength(900)]
    public string Decsription { get; set; } = null!;
    public int EventId { get; set; }
    public Event Event { get; set; }
}

