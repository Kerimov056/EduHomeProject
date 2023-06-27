using Microsoft.EntityFrameworkCore;
using EduHome.Core.Entities;

namespace EduHomeDataAccess.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Burda Upcomming ve Speakers arasindaki Many-to-Many
            modelBuilder.Entity<UpcommingSpeakers>().HasKey(us => new
            {
                us.Speakers.Id,
                us.UpcommingId
            });

            modelBuilder.Entity<UpcommingSpeakers>().HasOne(m => m.Upcomming).WithMany(us => us.UpcommingSpeakers).HasForeignKey(m =>
                m.UpcommingId);
            modelBuilder.Entity<UpcommingSpeakers>().HasOne(m => m.Speakers).WithMany(us => us.UpcommingSpeakers).HasForeignKey(m =>
                m.UpcommingId);


            //Burda Company ve Speakers arasindaki Many-to-Many
            modelBuilder.Entity<CompanySpeakers>().HasKey(cs => new
            {
                cs.SpeakersId,
                cs.CompanyId
            });

            modelBuilder.Entity<CompanySpeakers>().HasOne(m => m.Company).WithMany(cs => cs.CompanySpeakers).HasForeignKey(m =>
                m.CompanyId);
            modelBuilder.Entity<CompanySpeakers>().HasOne(m => m.Speakers).WithMany(cs => cs.CompanySpeakers).HasForeignKey(m =>
                m.SpeakersId);

            base.OnModelCreating(modelBuilder);
        }



        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Notice> Notices { get; set; }
        public DbSet<Info> Infos { get; set; }
        public DbSet<Courses> Coursess { get; set; }
        public DbSet<CoursesDetails> CoursesDetailss { get; set; }
        public DbSet<Categories> Categoriess { get; set; }
    }
}
