using EduHome.UI.Areas.Admin.Data.Base;
using EduHome.UI.Areas.Admin.Data.Base.CoursesRepository;
using EduHome.UI.Areas.Admin.Data.Services;
using EduHomeDataAccess.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using EduHome.Core.Entities;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(Program));


builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped(typeof(IEntityBaseRepository<>), typeof(EntityBaseRepository<>));
builder.Services.AddScoped(typeof(ICoursesBaseRepository<>), typeof(CoursesBaseRepository<>));

builder.Services.AddScoped<ISpkearServices, SpkearServices>();
builder.Services.AddScoped<ICoursesServices, CoursesServices>();
builder.Services.AddScoped<IBlogsService, BlogsServices>();
builder.Services.AddScoped<INoticeService, NoticesServices>();
builder.Services.AddScoped<IInfoService, InfosServices>();
builder.Services.AddScoped<ISliderServices, SliderServices>();
builder.Services.AddScoped<IEventServices, EventServices>();


builder.Services.AddIdentity<User, IdentityRole>(opt =>
{
    opt.Password.RequiredUniqueChars = 3;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequiredLength = 6;
    opt.Password.RequireDigit = true;
    opt.Password.RequireLowercase = true;
    opt.Password.RequireUppercase = false;

    opt.User.RequireUniqueEmail= false;
    opt.Lockout.MaxFailedAccessAttempts = 5;
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);

}).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();


var app = builder.Build();
app.UseStaticFiles();

app.UseExceptionHandler("/Home/Error");
app.UseStatusCodePages();
app.UseStatusCodePagesWithReExecute("/StatusCodeError/{0}");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
     name: "areas",
     pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}"
    );

app.MapControllerRoute(
       name: "default",
       pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
