using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using StudentInformationSystem.Models;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure the DbContext
builder.Services.AddDbContext<SchoolDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon"));
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.LoginPath = "/User/Login"; // Giriþ sayfasýnýn adresi
    options.AccessDeniedPath = "/Home/AccessDenied"; // Eriþim reddedildiðinde yönlendirilecek sayfanýn adresi
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Login}/{id?}");

app.MapControllerRoute(
    name: "admin",
    pattern: "admin",
    defaults: new { controller = "Admin", action = "Index" }
);
app.MapControllerRoute(
    name: "student",
    pattern: "student",
    defaults: new { controller = "Student", action = "Index" }
);
app.MapControllerRoute(
    name: "teacher",
    pattern: "teacher",
    defaults: new { controller = "Teacher", action = "Index" }
);
app.MapControllerRoute(
    name: "lesson",
    pattern: "lesson",
    defaults: new { controller = "Lesson", action = "Index" }
);
app.MapControllerRoute(
    name: "user",
    pattern: "user",
    defaults: new { controller = "User", action = "Index" }
);
app.MapControllerRoute(
    name: "teachermain",
    pattern: "teachermain",
    defaults: new { controller = "TeacherMain", action = "Index" }
);
app.MapControllerRoute(
    name: "grade",
    pattern: "grade",
    defaults: new { controller = "Grade", action = "Index" }
);
app.MapControllerRoute(
    name: "announcement",
    pattern: "announcement",
    defaults: new { controller = "Announcement", action = "Index" }
);
/*
app.MapControllerRoute(
    name: "studentmain",
    pattern: "studentmain",
    defaults: new { controller = "StudentMain", action = "MyDetails" }
);*/

app.Run();