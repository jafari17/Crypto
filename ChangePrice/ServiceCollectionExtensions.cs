using ChangePrice.Data.Repository;
using ChangePrice.Models;
using ChangePrice.Notification;
using ChangePrice.Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddScopedServices(this IServiceCollection services)
    {
        services.AddScoped<IAlertRepository, AlertRepository>();
        services.AddScoped<IAlertAutoRepository, AlertAutoRepository>();
        //services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IReportUserAlertsDtoRepository, ReportUserAlertsDtoRepository>();
        services.AddScoped<IExchangeProvider, ExchangeBinanceProvider>();
        services.AddScoped<INotificationEmail, NotificationEmail>();
        services.AddScoped<INotificationTelegram, NotificationTelegram>();
        services.AddScoped<IPriceTracking, PriceTracking>();
        services.AddScoped<IGenerateCandle, GenerateCandle>();
        services.AddScoped<IAlertAutoServies, AlertAutoServies>();

        services.AddScoped<CandlestickModel, CandlestickModel>();

        services.AddScoped<IAlertAutoServies, AlertAutoServies>();
        return services;
    }
}
