using EduHome.Core.Interface;

namespace EduHome.Core.Entities;

public class OrderDetail : IEntity
{
    public int Id { get ; set ; }
    public int Quantity { get; set ; }
    public double UnitPrice { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; }
    public int CoursesId { get; set; }
    public Courses Courses { get; set; }
}
