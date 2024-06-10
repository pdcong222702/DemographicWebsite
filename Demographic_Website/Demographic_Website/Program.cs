using AspNetCoreHero.ToastNotification;
using Demographic_Website.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
//add connection string
var stringConnectDB = builder.Configuration.GetConnectionString("dbDemoGraphic");
builder.Services.AddDbContext<DemoGraphicContext>(options => options.UseSqlServer(stringConnectDB));
// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
//add Indentity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<DemoGraphicContext>().AddDefaultTokenProviders();
builder.Services.AddNotyf(config => { config.DurationInSeconds = 3; config.IsDismissable = true; config.Position = NotyfPosition.TopRight; });
builder.Services.AddSession();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Admin}/{controller=RecedentceBooklet}/{action=Index}/{id?}");

app.Run();
