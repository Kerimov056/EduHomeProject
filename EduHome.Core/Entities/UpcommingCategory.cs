using EduHome.Core.Interface;

namespace EduHome.Core.Entities;

public class UpcommingCategory : IEntity
{
    public int Id { get; set ; }
    public string Category { get; set ; }
    public List<Upcomming> Upcommings { get; set ; }
}
