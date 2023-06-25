﻿using Microsoft.EntityFrameworkCore;
using EduHome.Core.Entities;

namespace EduHomeDataAccess.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Notice> Notices { get; set; }
        public DbSet<Info> Infos { get; set; }
        public DbSet<Courses> Coursess { get; set; }
        public DbSet<CoursesDetails> CoursesDetailss { get; set; }
        public DbSet<Categories> Categoriess { get; set; }
    }
}
