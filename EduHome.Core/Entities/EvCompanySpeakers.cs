using System.ComponentModel.DataAnnotations.Schema;

namespace EduHome.Core.Entities;

public class EvCompanySpeakers
{
    public int EvCompanyId { get; set; }
    [ForeignKey("EvCompanyId")]
    public EvCompany EvCompany { get; set; }

    public int EvSpeakersId { get; set; }
    [ForeignKey("EvSpeakersId")]
    public EvSpeakers EvSpeakers { get; set; }
}
