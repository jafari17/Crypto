using ChangePrice.Controllers;
using ChangePrice.Data.Dto;
using ChangePrice.Data.Repository;
using ChangePrice.Models;
using ChangePrice.Notification;
using System;

namespace ChangePrice.Services
{
    public class PriceTracking : IPriceTracking
    {






        private IAlertRepository _alertRepository;
        
        private IReportUserAlertsDtoRepository _reportUserAlertsDtoRepository;
        private IExchangeProvider _exchangeProvider;
        private INotificationEmail _notificationEmail;
        private INotificationTelegram _notificationTelegram;
        private readonly ILogger _logger;

        private readonly IConfiguration _configuration;

        private readonly int _minutesBehind;
        //-sdkofjdsojfsd


        //AlertSuspensionPeriod
        public PriceTracking(IAlertRepository alertRepository, IExchangeProvider exchangeProvider, INotificationEmail notificationEmail, 
                             ILogger<PriceTracking> logger, INotificationTelegram notificationTelegram, IConfiguration configuration, IReportUserAlertsDtoRepository reportUserAlertsDtoRepository)
        {
            _alertRepository = alertRepository;
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

                    EmailModel emailModel = sdfdsfsdf(price: itemReportUserAlerts.Price.Value, emailAddress: itemReportUserAlerts.EmailAddress,lastTouchPrice: itemReportUserAlerts.LastTouchPrice.Value,
                        touchDirection: direction, Description: itemReportUserAlerts.Description, ClosePrice: candle.ClosePrice);

                    var isEmailSent = _notificationEmail.Send(emailModel);
                    var isTelegramSent = _notificationTelegram.SendTextMessageToChannel($"T: {itemReportUserAlerts.Price} {direction} C: {Convert.ToInt32(candle.ClosePrice)}  \n\n {itemReportUserAlerts.Description} ");


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
            var Minutes33 = -1 * _minutesBehind;
            var dat = DateTime.Now.AddMinutes(Minutes33);

            if (LastTouchPrice <= DateTime.Now.AddMinutes(Minutes33))
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

        EmailModel sdfdsfsdf(decimal price, string emailAddress, DateTime lastTouchPrice, string touchDirection, string Description,decimal ClosePrice)
        {

            string ToAddres33 = emailAddress;
            string Subject33 = $"Touch Price {price}";
            string Body33 = $"T:{price} {touchDirection} C: {Convert.ToInt32(ClosePrice)}\n\n {Description} ";

            EmailModel emailModel = new EmailModel(ToAddres33, Subject33, Body33) { };

            return emailModel;
        }
    }
}
