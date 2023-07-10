using EduHome.Core.Interface;
using System.ComponentModel.DataAnnotations;

namespace EduHome.Core.Entities;

public class CartDetail : IEntity
{
    public int Id { get ; set; }
    [Required]
    public int ShoppingCartId { get; set; }
    public ShoppingCart ShoppingCart { get; set; }
    [Required]
    public int CoursesId { get; set; }
    public Courses Courses { get; set;}

}
