
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NoteKeeper.Data;
using NoteKeeper.Models;
using NoteKeeper.Repositories;
using NoteKeeper.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// MVC Services
builder.Services.AddControllersWithViews();

// Database Connection for Identity
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity Configuration
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Cookie Configuration
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/Login";
});

// Repository Dependency Injection
builder.Services.AddScoped<INotebookRepository, NotebookRepository>();

var app = builder.Build();

// Middleware Configuration
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Default Route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
