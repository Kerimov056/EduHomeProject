using EduHome.Core.Interface;
using System.ComponentModel.DataAnnotations;

namespace EduHome.Core.Entities;

public class Event : IEntity
{

    public int Id { get; set; }
    [Required, MaxLength(40)]
    public string Name { get; set; } = null!;
    public DateTime DateTime { get; set; }
    [Required, MaxLength(100)]
    public string Location { get; set; } = null!;
    [Required]
    public EventDetails EventDetails { get; set; }
    [Required]
    public int CategoriesId { get; set; }
    [Required]
    public Categories Categories { get; set; }
    public List<Event_Spkears> Event_Spkears { get; set; }

}
