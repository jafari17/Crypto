using System.Threading.Channels;

namespace ChangePrice.Notification
{
    public class NotificationTelegram: INotificationTelegram
    {
        private readonly IConfiguration _configuration;

        private readonly string _botToken;
        private readonly string _channelId;
        private readonly ILogger _logger;

        public NotificationTelegram(IConfiguration configuration, ILogger<NotificationEmail> logger)
        {
            _configuration = configuration;
            _logger = logger;

            _botToken = _configuration.GetValue<string>("TelegramConfig:BotToken");
            _channelId = _configuration.GetValue<string>("TelegramConfig:ChannelId");
        }

        public bool SendTextMessageToChannel(string MassageTelegram)
        {
            //Console.WriteLine("https://api.telegram.org/bot{0}/sendMessage?chat_id={1}&text={2}", botToken, channelId, message);
            //Console.ReadKey();
            var address = string.Format("https://api.telegram.org/bot{0}/sendMessage?chat_id={1}&text={2}", _botToken, _channelId, MassageTelegram);

            using (var webClient = new System.Net.WebClient())
            {
                webClient.DownloadString(address);
            }

            return true;
        }
    }
}
