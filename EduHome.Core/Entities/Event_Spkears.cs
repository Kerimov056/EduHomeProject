using System.ComponentModel.DataAnnotations.Schema;

namespace EduHome.Core.Entities;

public class Event_Spkears
{
    public int EventId { get; set; }
    [ForeignKey("EventId")]
    public virtual Event Event { get; set; }


    public int EvSpeakersId { get; set; }
    [ForeignKey("EvSpeakersId")]
    public virtual EvSpeakers EvSpeakers { get; set; }
}
