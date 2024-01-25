using ChangePrice;
using ChangePrice.Data.DataBase;
using ChangePrice.Models;
using ChangePrice.Notification;
using ChangePrice.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScopedServices();

builder.Services.AddDbContext<CryptoDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//#region Db Context

//builder.Services.AddDbContext<TestCryptoCreatQueryContext>(options =>
//{ options.UseSqlServer("Data Source =.;Initial Catalog=TestCryptoCreatQuery;Integrated Security=true"); });
//#endregion



builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddEntityFrameworkStores<CryptoDbContext>()
    .AddDefaultTokenProviders();


//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(options =>
//    {
//        options.LoginPath = "/Account/Login";  // Set your custom login path here
//        options.AccessDeniedPath = "/Identity/Account/AccessDenied"; // Optional for access denied scenarios
//        // Other cookie options as needed
//    });

//builder.Services.AddAuthorization();




builder.Services.AddHostedService<TimerBackgroundService>();

builder.Services.AddLogging();





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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
