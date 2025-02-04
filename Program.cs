//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddControllersWithViews();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.Run();


using Microsoft.AspNetCore.Identity;

using CourseraClone.Repositories;

using WebApplication1.Infrastructure;
using WebApplication1.Application.Interfaces;
using System;
using WebApplication1.Application.Interfaces;
using WebApplication1.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register repositories for dependency injection
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
builder.Services.AddScoped<IUserCoursesRepository, UserCoursesRepository>();
builder.Services.AddTransient<IDropCourseRepository, DropCourseRepository>();
builder.Services.AddScoped<IComplaintRepository, ComplaintRepository>();


builder.Services.AddDistributedMemoryCache(); // To store session data in memory
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout to 30 minutes
    options.Cookie.HttpOnly = true; // Make the session cookie accessible only to the server
    options.Cookie.IsEssential = true; // Necessary for session to work without requiring consent
});





// // Add ASP.NET Core Identity services
// builder.Services.AddIdentity<IdentityUser, IdentityRole>()
//     .AddEntityFrameworkStores<AppDBContext>()
//     .AddDefaultTokenProviders();


// Optionally configure logging
builder.Logging.AddConsole();


// Build the app
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseSession();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication(); // Enable Authentication
app.UseAuthorization();  // Enable Authorization

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

