using ChangePrice;
using ChangePrice.Models;
using ChangePrice.Notification;
using ChangePrice.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScopedServices();


//builder.Services.AddScoped<IPriceRepository, PriceJsonFileRepository>();
//builder.Services.AddScoped<IExchangeProvider, ExchangeBinanceProvider>();
//builder.Services.AddScoped<INotificationEmail, NotificationEmail>();
//builder.Services.AddScoped<INotificationTelegram, NotificationTelegram>();
//builder.Services.AddScoped<IPriceTracking, PriceTracking>();
//builder.Services.AddScoped<IGenerateCandle, GenerateCandle>();

//builder.Services.AddScoped<CandlestickModel, CandlestickModel>();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
