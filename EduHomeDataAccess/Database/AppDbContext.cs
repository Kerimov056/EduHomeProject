using EduHome.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EduHomeDataAccess.Database;

public class AppDbContext : IdentityDbContext<User>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Events_Speakers>()
        .HasKey(us => new { us.EventsId, us.SpeakersId });

        modelBuilder.Entity<Events_Speakers>()
            .HasOne(us => us.Events)
            .WithMany(u => u.Events_Speakers)
            .HasForeignKey(us => us.EventsId);

        modelBuilder.Entity<Events_Speakers>()
            .HasOne(us => us.Speakers)
            .WithMany(s => s.Events_Speakers)
            .HasForeignKey(us => us.SpeakersId);



        base.OnModelCreating(modelBuilder);
    }

    public object Find(int courseId)
    {
        throw new NotImplementedException();
    }

    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Slider> Sliders { get; set; }
    public DbSet<Notice> Notices { get; set; }
    public DbSet<Info> Infos { get; set; }
    public DbSet<Courses> Coursess { get; set; }
    public DbSet<CoursesDetails> CoursesDetailss { get; set; }
    public DbSet<Categories> Categoriess { get; set; }

    public DbSet<Events> Eventss { get; set; }
    public DbSet<Speakers> Speakerss { get; set; }
    public DbSet<EventsDetails> EventsDetailss { get; set; }
    public DbSet<Events_Speakers> EventsDetails { get; set; }
    public DbSet<About> Abouts { get; set; }
    public DbSet<TeacherDetails> TeacherDetails { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Setting> Settings { get; set; }
    public DbSet<Viewer> Viewers { get; set; }

    //shop
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<CartDetail> CartDetails { get; set; }
    public DbSet<OrderStatus> OrderStatuss { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }

    //Comment
    public DbSet<CourseComment> CourseComments { get; set; }

}
