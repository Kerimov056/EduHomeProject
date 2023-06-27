using EduHome.Core.Interface;

namespace EduHome.Core.Entities;

public class Speakers : IEntity
{
    public int Id { get ; set ; }
    public string ImagePath { get; set; } = null!;
    public string Name { get; set ; } = null!;
    public string WhereWorks { get; set; } = null!;
    public List<UpcommingSpeakers> UpcommingSpeakers { get; set; }
    public List<CompanySpeakers> CompanySpeakers { get; set; }


}
