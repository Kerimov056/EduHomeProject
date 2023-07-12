using EduHome.Core.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduHome.Core.Entities;

public class Courses : IEntity
{
    [Key]
    public int Id { get ; set ; }
    [Required,MaxLength(300)]
    public string ImagePath { get; set; } = null!;
    [Required, MaxLength(40)]
    public string Name { get; set ; } = null!;
    [Required, MaxLength(700)]
    public string Descripton { get; set; } = null!;

    //Categori relationships 
    public int CategoriesId { get; set; }
    [ForeignKey("CategoriesId")]
    public Categories Categories { get; set; } = null!;

    /* EF Relation */
    public CoursesDetails CoursesDetails { get; set; }

    public List<CartDetail> CartDetails { get; set; }
    public List<OrderDetail> OrderDetails { get; set; }
    public List<CourseComment>? CourseComments { get; set; }


}
