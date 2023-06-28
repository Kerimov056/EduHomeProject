using EduHome.UI.Areas.Admin.Data.Services;
using EduHomeDataAccess.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<IBlogsService, BlogsServices>();
builder.Services.AddScoped<INoticeService, NoticesServices>();


builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

//services.AddDbContext<IBlogsService, BlogsServices>();


var app = builder.Build();
app.UseStaticFiles();
app.MapControllerRoute(
     name: "areas",
     pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}"
    );

app.MapControllerRoute(
       name: "default",
       pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
