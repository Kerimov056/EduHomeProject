using System.ComponentModel.DataAnnotations.Schema;

namespace EduHome.Core.Entities;

public class Event_Spkears
{
    public int EventId { get; set; }
    [ForeignKey("EventId")]
    public Event Event { get; set; }
    
    public int EvSpeakersId { get; set; }
    [ForeignKey("EvSpeakersId")]
    public EvSpeakers EvSpeakers { get; set; }
}
