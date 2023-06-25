using EduHome.Core.Interface;

namespace EduHome.Core.Entities;

public class Slider : IEntity
{
    public int Id { get ; set ; }
    public string ImagePath { get; set ; } = null!;
    public string Name { get; set; } = null!;
    public string NameTwo { get; set; } = null!;
    public string Information { get; set; } = null!;

}
