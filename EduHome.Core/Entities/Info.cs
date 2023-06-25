using EduHome.Core.Interface;

namespace EduHome.Core.Entities
{
    public class Info : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
