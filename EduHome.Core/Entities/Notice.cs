using EduHome.Core.Interface;
using System.ComponentModel.DataAnnotations;

namespace EduHome.Core.Entities
{
    public class Notice : IEntity
    {
        public int Id { get ; set ; }
        public DateTime Date_Time { get ; set ; }
        [Required,MaxLength(255)]
        public string Description { get; set; } = null!;
    }
}
