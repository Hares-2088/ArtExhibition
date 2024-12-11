using ArtExhibition.Data;
using ArtExhibition.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Configure the database context to use SQL Server
builder.Services.AddDbContext<GalleryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Identity services
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<GalleryDbContext>()
    .AddDefaultTokenProviders();

// Add controllers with views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Enable Authentication and Authorization
app.UseAuthentication(); // Authentication Middleware
app.UseAuthorization();  // Authorization Middleware

// Add default route for HomeController
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Add route for ArtworksController
app.MapControllerRoute(
    name: "artworks",
    pattern: "Artworks/{action=Index}/{id?}",
    defaults: new { controller = "Artworks" });

// Seed Users and Roles
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<User>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    await SeedUsersAndRolesAsync(userManager, roleManager);
}

app.Run();

async Task SeedUsersAndRolesAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
{
    // Create Roles
    var roles = new[] { "Admin", "Artist", "User" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    // Create Admin User
    if (await userManager.FindByEmailAsync("admin@artgallery.com") == null)
    {
        var admin = new User
        {
            UserName = "admin",
            Email = "admin@artgallery.com",
            FullName = "Administrator"
        };
        var result = await userManager.CreateAsync(admin, "Admin@123");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(admin, "Admin");
        }
    }

    // Create Artist User
    if (await userManager.FindByEmailAsync("artist@artgallery.com") == null)
    {
        var artist = new User
        {
            UserName = "artist",
            Email = "artist@artgallery.com",
            FullName = "Famous Artist"
        };
        var result = await userManager.CreateAsync(artist, "Artist@123");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(artist, "Artist");
        }
    }

    // Create Regular User
    if (await userManager.FindByEmailAsync("user@artgallery.com") == null)
    {
        var user = new User
        {
            UserName = "user",
            Email = "user@artgallery.com",
            FullName = "Regular User"
        };
        var result = await userManager.CreateAsync(user, "User@123");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "User");
        }
    }
}
