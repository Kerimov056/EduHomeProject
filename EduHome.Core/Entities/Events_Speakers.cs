using EduHome.Core.Interface;

namespace EduHome.Core.Entities;

public class Events_Speakers:IEntity
{
    public int Id { get ; set ; }
    public int EventsId { get; set; }
    public Events Events { get; set; }


    public int SpeakersId { get; set; }
    public Speakers Speakers { get; set;}
}
