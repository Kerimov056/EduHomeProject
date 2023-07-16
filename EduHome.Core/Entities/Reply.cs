//using EduHome.Core.Interface;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace EduHome.Core.Entities
//{
//    public class Reply : IEntity
//    {
//        public int Id { get; set; }
//        public string ReplyText { get; set; }
//        public DateTime DateTime { get; set; }

//        [ForeignKey("User")]
//        public string UserId { get; set; }
//        public User User { get; set; }

//        [ForeignKey("CourseComment")]
//        public int CourseCommentId { get; set; }
//        public CourseComment CourseComment { get; set; }
//    }
//}
