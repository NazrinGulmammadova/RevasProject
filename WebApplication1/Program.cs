using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Revas.Core.Entities;
using Revas.DataAccess.Contexts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
var connectionString = builder.Configuration["ConnectionStrings:Default"];
builder.Services.AddDbContext<AppDbContext>(opt =>
        opt.UseSqlServer(connectionString)
);

builder.Services.AddIdentity<UserSide, IdentityRole>(opt =>
{
    opt.Password.RequiredLength = 8;
    opt.Password.RequireLowercase = true;
    opt.Password.RequireUppercase = true;
    opt.Password.RequireDigit = true;
    opt.Password.RequireNonAlphanumeric = true;

    opt.User.RequireUniqueEmail = true;

    opt.Lockout.MaxFailedAccessAttempts = 3;
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(15);
    opt.Lockout.AllowedForNewUsers = false;
}).AddEntityFrameworkStores<AppDbContext>();

var app = builder.Build();


app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
