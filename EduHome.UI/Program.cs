using EduHome.UI.Areas.Admin.Data.Base;
using EduHome.UI.Areas.Admin.Data.Base.CoursesRepository;
using EduHome.UI.Areas.Admin.Data.Services;
using EduHomeDataAccess.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using EduHome.UI.Data;
using EduHome.UI.Areas.Identity.Data;

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


//builder.Services.AddDefaultIdentity<ApplicationAdminUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<EduHomeUIContext>();

//builder.Services.AddRazorPages();


var app = builder.Build();

app.UseStatusCodePagesWithRedirects("/Shared/NotFound?statusCode={0}");
app.UseStaticFiles();
//app.UseAuthentication();

app.UseExceptionHandler("/Home/Error");
app.UseStatusCodePages();
app.UseStatusCodePagesWithReExecute("/StatusCodeError/{0}");


app.MapControllerRoute(
     name: "areas",
     pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}"
    );
//app.MapRazorPages();

app.MapControllerRoute(
       name: "default",
       pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
