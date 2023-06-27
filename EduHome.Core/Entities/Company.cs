using EduHome.Core.Interface;

namespace EduHome.Core.Entities;

public class Company : IEntity
{
    public int Id { get ; set ; }
    public string Name { get; set; } = null!;
    public List<CompanySpeakers> CompanySpeakers { get; set; }
}
