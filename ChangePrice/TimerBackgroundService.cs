using ChangePrice.Controllers;
using ChangePrice.Notification;
using ChangePrice.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ChangePrice
{
    public class TimerBackgroundService : BackgroundService
    {
        private Timer? _timer = null;

        private readonly int _TimeSpanSeconds;


        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IConfiguration _configuration;
        public TimerBackgroundService(IServiceScopeFactory scopeFactory, IConfiguration configuration)
        {
            _scopeFactory = scopeFactory;
            _configuration = configuration;

            _TimeSpanSeconds = _configuration.GetValue<int>("BackgroundService:TimeSpanSeconds");

        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(_TimeSpanSeconds));
        }

        private void DoWork(object? state)
        {
            using (IServiceScope scope = _scopeFactory.CreateScope())
            {
                if (CheckiActiveTime())
                {
                    IPriceTracking _iPriceTracking = scope.ServiceProvider.GetRequiredService<IPriceTracking>();
                    _iPriceTracking.TrackPriceListChanges();
                }
                Console.WriteLine(DateTime.Now);
            }
        }

        private bool CheckiActiveTime()
        { 

            double tehranOffset = 3.5;
            DateTime now = DateTime.UtcNow.AddHours(tehranOffset);
            int hour = now.Hour;

            if ( hour < 6)
            {
                Console.WriteLine(" 12pm and 6am ");
                return false;
            }

            else
            {

                return true;
            }
        }
    }
}