using EduHome.Core.Interface;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduHome.Core.Entities;

public class UpcommingDetails : IEntity
{
    public int Id { get ; set ; }
    public string ImagePath { get; set ; } = null;
    public string Title { get; set; } = null;
    public string Description { get; set; } = null;
    public int UpcommingId { get; set; }
    [ForeignKey("UpcommingId")]
    public Upcomming Upcomming { get; set; }

}
