using Microsoft.EntityFrameworkCore;
using EduHome.Core.Entities;

namespace EduHomeDataAccess.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Upcomming and Speakers Many-to-Many
        modelBuilder.Entity<Event_Spkears>()
        .HasKey(us => new { us.EvSpeakersId, us.EventId });

        modelBuilder.Entity<Event_Spkears>()
            .HasOne(us => us.Event)
            .WithMany(u => u.Event_Spkears)
            .HasForeignKey(us => us.EventId);

        modelBuilder.Entity<Event_Spkears>()
            .HasOne(us => us.EvSpeakers)
            .WithMany(s => s.Event_Spkears)
            .HasForeignKey(us => us.EvSpeakersId);


        //Company and Speakers Many-to-Many

        modelBuilder.Entity<EvCompanySpeakers>().HasKey(cs => new { cs.EvSpeakersId, cs.EvCompanyId });

        modelBuilder.Entity<EvCompanySpeakers>()
            .HasOne(cs => cs.EvCompany)
            .WithMany(c => c.EvCompanySpeakers)
            .HasForeignKey(cs => cs.EvCompanyId);

        modelBuilder.Entity<EvCompanySpeakers>()
            .HasOne(cs => cs.EvSpeakers)
            .WithMany(s => s.EvCompanySpeakers)
            .HasForeignKey(cs => cs.EvSpeakersId);

        base.OnModelCreating(modelBuilder);
    }



    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Slider> Sliders { get; set; }
    public DbSet<Notice> Notices { get; set; }
    public DbSet<Info> Infos { get; set; }
    public DbSet<Courses> Coursess { get; set; }
    public DbSet<CoursesDetails> CoursesDetailss { get; set; }
    public DbSet<Categories> Categoriess { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<EventDetails> EventDetailss { get; set; }
    public DbSet<EvSpeakers> EvSpeakerss { get; set; }
    public DbSet<EvCompany> EvCompanys { get; set; }
    public DbSet<EvCompanySpeakers> EvCompanySpeakerss { get; set; }
    public DbSet<Event_Spkears> Event_Spkearss { get; set; }
    public DbSet<Hecne> Hecne { get; set; }
}
