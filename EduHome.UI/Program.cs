using EduHomeDataAccess.Interfaces;
using EduHomeDataAccess.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using EduHome.Core.Entities;
using System.Configuration;
using Microsoft.AspNetCore.Identity.UI.Services;
using EduHome.UI.Helpers;
using EduHome.UI.HeaderAndFooterService.Interface;
using EduHome.UI.HeaderAndFooterService;
using EduHome.UI.Areas.Admin.Data.Services.Concrets;
using EduHome.UI.Areas.Admin.Data.Services.Interfaces;
using EduHomeDataAccess.Repository;
using EduHome.UI.ShopServices.Interfaces;
using EduHome.UI.ShopServices.Concrets;


var builder = WebApplication.CreateBuilder(args);




//services.AddAuthentication()
//    .AddGoogle(options =>
//    {
//        options.ClientId = "YOUR_CLIENT_ID";
//        options.ClientSecret = "YOUR_CLIENT_SECRET";
//    });



builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

builder.Services.AddScoped(typeof(IEntityBaseRepository<>), typeof(EntityBaseRepository<>));
builder.Services.AddScoped(typeof(ICoursRepository<>), typeof(CoursRepository<>));

builder.Services.AddScoped<ISpkearServices, SpkearServices>();
builder.Services.AddScoped<ICoursesServices, CoursesServices>();
builder.Services.AddScoped<IBlogsService, BlogsServices>();
builder.Services.AddScoped<INoticeService, NoticesServices>();
builder.Services.AddScoped<IInfoService, InfosServices>();
builder.Services.AddScoped<ISliderServices, SliderServices>();
builder.Services.AddScoped<IEventServices, EventServices>();
builder.Services.AddScoped<ISettingService, SettingService>();
builder.Services.AddScoped<ICourseDetailsServices, CourseDetailsServices>();
builder.Services.AddScoped<ICategoryServices, CategoryServices>();
builder.Services.AddScoped<IEventsDetailsServices, EventsDetailsServices>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IUserOrderServices, UserOrderService>();

builder.Services.AddScoped<ISearchServices, SearchService>();
builder.Services.AddScoped<ICartService, CartService>();

builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<EmailSettings>();

builder.Services.AddIdentity<User, IdentityRole>(opt =>
{
    opt.Password.RequiredUniqueChars = 3;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequiredLength = 6;
    opt.Password.RequireDigit = true;
    opt.Password.RequireLowercase = true;
    opt.Password.RequireUppercase = false;

    opt.User.RequireUniqueEmail = false;
    opt.Lockout.MaxFailedAccessAttempts = 5;
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);

}).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/SiginUp/LogIn";
});

var configuration = builder.Configuration;

builder.Services.AddAuthentication().AddGoogle(googleOptions =>
{
    googleOptions.ClientId = configuration["Authentication:Google:ClientId"];
    googleOptions.ClientSecret = configuration["Authentication:Google:ClientSecret"];
});



var app = builder.Build();

//NotFound Page
app.UseStatusCodePagesWithRedirects("/Shared/NotFound?statusCode={0}");
app.UseStaticFiles();
app.UseAuthentication();

app.UseExceptionHandler("/Home/Error");
app.UseStatusCodePages();
app.UseStatusCodePagesWithReExecute("/StatusCodeError/{0}");
//-----------------------------------------------------

app.UseStaticFiles();

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
