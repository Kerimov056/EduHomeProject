using EduHome.Core.Interface;
using System.ComponentModel.DataAnnotations;

namespace EduHome.Core.Entities;

public class OrderDetail : IEntity
{
    public int Id { get ; set ; }
    [Required]
    public int Quantity { get; set ; }
    [Required]
    public double UnitPrice { get; set; }
    [Required]
    public int OrderId { get; set; }
    public Order Order { get; set; }
    [Required]
    public int CoursesId { get; set; }
    public Courses Courses { get; set; }
}
