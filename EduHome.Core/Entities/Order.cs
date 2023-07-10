using EduHome.Core.Interface;
using System.ComponentModel.DataAnnotations;

namespace EduHome.Core.Entities;

public class Order : IEntity
{
    public int Id { get ; set; }
    [Required]
    public string UserId { get; set; }
    [Required]
    public DateTime CreateDate { get; set; } = DateTime.UtcNow;
    [Required]
    public bool IsDeleted { get; set; }=false;
    [Required]
    public int OrderStatusId { get; set; }
    public OrderStatus OrderStatus { get; set; }
    [Required]
    public List<OrderDetail> orderDetails { get; set; }

}
