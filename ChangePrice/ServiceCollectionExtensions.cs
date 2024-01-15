using ChangePrice.Models;
using ChangePrice.Notification;
using ChangePrice.Repository;
using ChangePrice.Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddScopedServices(this IServiceCollection services)
    {
        services.AddScoped<IPriceRepository, PriceJsonFileRepository>();
        services.AddScoped<IExchangeProvider, ExchangeBinanceProvider>();
        services.AddScoped<INotificationEmail, NotificationEmail>();
        services.AddScoped<INotificationTelegram, NotificationTelegram>();
        services.AddScoped<IPriceTracking, PriceTracking>();
        services.AddScoped<IGenerateCandle, GenerateCandle>();

        services.AddScoped<CandlestickModel, CandlestickModel>();

        return services;
    }
}
