using EduHome.Core.Interface;
using System.ComponentModel.DataAnnotations;

namespace EduHome.Core.Entities;

public class ShoppingCart:IEntity
{
    public int Id { get ; set ; }
    [Required]
    public string UserId { get; set; } = null!;
    public bool IsDeleted { get; set; }=false;
    public ICollection<CartDetail> CartDetails { get; set; }
}
