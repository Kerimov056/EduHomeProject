namespace EduHome.Core.Entities;

public class Events_Speakers
{
    public int EventsId { get; set; }
    public Events Events { get; set; }


    public int SpeakersId { get; set; }
    public Speakers Speakers { get; set;}
}
