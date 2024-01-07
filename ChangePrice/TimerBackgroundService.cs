using ChangePrice.Controllers;
using ChangePrice.Notification;
using ChangePrice.Repository;
using ChangePrice.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ChangePrice
{
    public class TimerBackgroundService : BackgroundService
    {
        private Timer? _timer = null;

        private readonly IServiceScopeFactory _scopeFactory;

        public TimerBackgroundService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(60));
        }

        private void DoWork(object? state)
        {
            using (IServiceScope scope = _scopeFactory.CreateScope())
            {
                IPriceTracking _iPriceTracking = scope.ServiceProvider.GetRequiredService<IPriceTracking>();
                _iPriceTracking.TrackPriceListChanges();
                Console.WriteLine(DateTime.Now);

            }

        }
    }
}