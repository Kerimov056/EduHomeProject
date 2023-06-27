
using System.ComponentModel.DataAnnotations.Schema;

namespace EduHome.Core.Entities;

public class UpcommingSpeakers 
{   
   //Upcomming
   public int UpcommingId { get; set; }
   [ForeignKey("UpcommingId")]
   public Upcomming Upcomming { get; set; }

   //Spekars
   public int SpeakersId { get; set; }
   public Speakers Speakers { get; set; }

}
