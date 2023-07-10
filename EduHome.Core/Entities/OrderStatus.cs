using EduHome.Core.Interface;
using System.ComponentModel.DataAnnotations;

namespace EduHome.Core.Entities;

public class OrderStatus : IEntity
{
    public int Id { get; set; }
    [Required]
    public int StatusId { get; set; }
    [Required,MaxLength(20)]
    public string ?StatusName { get; set; }
    
}
