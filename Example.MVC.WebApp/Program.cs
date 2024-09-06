using Example.EF.DbContexts;
using Example.EF.Entities.Identity;
using Example.MVC.WebApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ExampleDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("NorthwindDb"));
});

builder.Services.AddIdentity<AppUser, IdentityRole>(setup =>
{
	setup.Password.RequireDigit = false;
	setup.Password.RequiredLength = 4;
	setup.Password.RequireNonAlphanumeric = false;
	setup.Password.RequireUppercase = false;
	setup.Password.RequireLowercase = false;
}).AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();

builder.Services.AddDbContext<IdentityContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("NorthwindDb"));
});


builder.Services.AddScoped<EmployeeService>();
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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
