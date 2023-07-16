using EduHome.Core.Interface;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduHome.Core.Entities
{
    public class CourseComment : IEntity
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedDate { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }

        public int CoursesId { get; set; }
        [ForeignKey("CoursesId")]
        public Courses Courses { get; set; }

        public List<Like> Likes { get; set; }
        //public List<Reply> Replies { get; set; }
    }
}