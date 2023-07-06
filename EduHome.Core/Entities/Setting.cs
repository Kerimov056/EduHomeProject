using EduHome.Core.Interface;

namespace EduHome.Core.Entities;

public class Setting : IEntity
{
    public int Id { get; set; }
    public string Key { get; set; } = null!;
    public string Value { get; set; }=null!;
}
