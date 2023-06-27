using EduHome.Core.Interface;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduHome.Core.Entities;

public class Upcomming : IEntity
{
    public int Id { get ; set ; }
    public string Name { get; set; } = null!;
    public DateTime time { get; set ; }
    public string Location { get; set; } = null!;
    public UpcommingDetails Details { get; set; }
    public int UpcommingCategoryId { get; set; }
    [ForeignKey("UpcommingCategoryId")]
    public UpcommingCategory UpcommingCategory { get; set; }
    public List<UpcommingSpeakers> UpcommingSpeakers { get; set; }


}
