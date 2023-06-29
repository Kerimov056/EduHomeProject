using EduHome.Core.Interface;

namespace EduHome.Core.Entities;

public class Speakers : IEntity
{
    public int Id { get ; set ; }
    public string ImagePath { get; set ; }
    public string Name { get; set ; }
    public string Postions { get; set ; }
    public string JobName { get; set ; }
    //Relationships
    public List<Events_Speakers> Events_Speakers { get; set ; }
}
