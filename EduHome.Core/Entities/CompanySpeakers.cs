using System.ComponentModel.DataAnnotations.Schema;

namespace EduHome.Core.Entities;

public class CompanySpeakers
{
    //Company
    public int CompanyId { get; set; }
    public Company Company { get; set; }



    //Spekars
    public int SpeakersId { get; set; }
    [ForeignKey("SpeakersId")]
    public Speakers Speakers { get; set; }
}
