using ChangePrice.Controllers;
using ChangePrice.Data.Dto;
using ChangePrice.Data.Repository;
using ChangePrice.DataBase;
using ChangePrice.Models;
using ChangePrice.Notification;

namespace ChangePrice.Services
{
    public class PriceTracking : IPriceTracking
    {
        private IAlertRepository _alertRepository;
        private IUserRepository _userRepository;
        private IReportUserAlertsDtoRepository _reportUserAlertsDtoRepository;
        private IExchangeProvider _exchangeProvider;
        private INotificationEmail _notificationEmail;
        private INotificationTelegram _notificationTelegram;
        private readonly ILogger _logger;

        private readonly IConfiguration _configuration;

        private readonly int _minutesBehind;

        //AlertSuspensionPeriod
        public PriceTracking(IAlertRepository alertRepository, IExchangeProvider exchangeProvider, INotificationEmail notificationEmail, 
                             ILogger<PriceTracking> logger, INotificationTelegram notificationTelegram, IConfiguration configuration, IUserRepository userRepository, IReportUserAlertsDtoRepository reportUserAlertsDtoRepository)
        {
            _alertRepository = alertRepository;
            _userRepository = userRepository;
            _exchangeProvider = exchangeProvider;
            _notificationEmail = notificationEmail;
            _logger = logger;
            _notificationTelegram = notificationTelegram;
            _configuration = configuration;

            _minutesBehind = _configuration.GetValue<int>("AlertSuspensionPeriod:MinutesBehind");
            _reportUserAlertsDtoRepository = reportUserAlertsDtoRepository;
        }


        public void TrackPriceListChanges() //Track
        {
            List<ReportUserAlertsDto> ListReportUserAlerts = _reportUserAlertsDtoRepository.GetAllReportUserAlerts();

            CandlestickModel candle = _exchangeProvider.GetLastCandle();

            _exchangeProvider.GetLastPrice();

            foreach (var itemReportUserAlerts in ListReportUserAlerts)
            {
                
                if ( DosePriceConditionMeet(itemReportUserAlerts.Price.Value, candle.HighPrice, candle.LowPrice) && 
                    AlertSuspensionPeriod(itemReportUserAlerts.LastTouchPrice.Value))

                {
                    itemReportUserAlerts.LastTouchPrice = DateTime.Now;
                    itemReportUserAlerts.IsCrossedUp = IsCrossedUp(itemReportUserAlerts.Price.Value, candle.OpenPrice);
                    var direction = itemReportUserAlerts.IsCrossedUp.Value ? "↗" : "↘";

                    EmailModel emailModel = CreateEmailModel(price: itemReportUserAlerts.Price.Value, emailAddress: itemReportUserAlerts.EmailAddress,
                                            lastTouchPrice: itemReportUserAlerts.LastTouchPrice.Value, touchDirection: direction, Description: itemReportUserAlerts.Description);

                    //var isEmailSent = _notificationEmail.Send(emailModel);
                    //var isTelegramSent = _notificationTelegram.SendTextMessageToChannel($"Touch Price {itemAlert.price} in datetime" +
                    //                                        $" {itemAlert.LastTouchPrice} {direction}  \n Description: \n {itemAlert.Description} ");


                    //itemAlert.IsTemproprySuspended = NeedtoBeSusspended(isEmailSent);  /// کامنت تا تغییر 

                    _logger.LogInformation($"Touch Price {itemReportUserAlerts.Price} in datetime {itemReportUserAlerts.LastTouchPrice} {direction}");
                }
                itemReportUserAlerts.PriceDifference = itemReportUserAlerts.Price - candle.ClosePrice;
                _alertRepository.UpdateAlert(itemReportUserAlerts);
            }

            _alertRepository.Save();
        }

        private bool NeedtoBeSusspended(bool isEmailSent)
        {
            return isEmailSent;
        }

        private bool AlertSuspensionPeriod(DateTime LastTouchPrice)
        {
            if (LastTouchPrice < DateTime.Now.AddMinutes(_minutesBehind))
            {
                return true;
            }
            return false;
        }

        bool DosePriceConditionMeet(decimal Price, decimal HighPrice, decimal LowPrice)
        {
            return Price <= HighPrice && Price >= LowPrice;
        }

        bool IsCrossedUp(decimal price, decimal openPrice)
        {
            return price >= openPrice;
        }

        EmailModel CreateEmailModel(decimal price, string emailAddress, DateTime lastTouchPrice, string touchDirection, string Description)
        {

            string ToAddres = emailAddress;
            string Subject = $"Touch Price {price}";
            string Body = $"Touch Price {price} in datetime {lastTouchPrice} {touchDirection} \n Description: \n {Description} ";

            EmailModel emailModel = new EmailModel(ToAddres, Subject, Body) { };

            return emailModel;
        }
    }
}
