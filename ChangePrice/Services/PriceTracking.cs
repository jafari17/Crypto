using ChangePrice.Controllers;
using ChangePrice.Models;
using ChangePrice.Notification;
using ChangePrice.Repository;


namespace ChangePrice.Services
{
    public class PriceTracking : IPriceTracking
    {
        private IPriceRepository _priceRepository;
        private IExchangeProvider _exchangeProvider;
        private INotificationEmail _notificationEmail;
        private INotificationTelegram _notificationTelegram;
        private readonly ILogger _logger;
        public PriceTracking(IPriceRepository priceRepository, IExchangeProvider exchangeProvider, INotificationEmail notificationEmail, 
                             ILogger<PriceTracking> logger, INotificationTelegram notificationTelegram)
        {
            _priceRepository = priceRepository;
            _exchangeProvider = exchangeProvider;
            _notificationEmail = notificationEmail;
            _logger = logger;
            _notificationTelegram = notificationTelegram;
        }


        public void TrackPriceListChanges() //Track
        {
            List<AlertModel> listAlert = _priceRepository.GetList();

            CandlestickModel candle = _exchangeProvider.GetLastCandle();

            _exchangeProvider.GetLastPrice();

            foreach (var itemAlert in listAlert)
            {
                itemAlert.PriceDifference = itemAlert.price - candle.ClosePrice;

                if (itemAlert.IsActive == true && itemAlert.IsTemproprySuspended == false &&
                    DosePriceConditionMeet(itemAlert.price, candle.HighPrice, candle.LowPrice) && AlertSuspensionPeriod(itemAlert.LastTouchPrice))

                {
                    itemAlert.LastTouchPrice = DateTime.Now;
                    itemAlert.IsCrossedUp = IsCrossedUp(itemAlert.price, candle.OpenPrice);
                    var direction = itemAlert.IsCrossedUp ? "↗" : "↘";

                    EmailModel emailModel = CreateEmailModel(price: itemAlert.price, emailAddress: itemAlert.EmailAddress,
                                            lastTouchPrice: itemAlert.LastTouchPrice, touchDirection: direction, Description: itemAlert.Description);

                    var isEmailSent = _notificationEmail.Send(emailModel);
                    var isTelegramSent = _notificationTelegram.SendTextMessageToChannel($"Touch Price {itemAlert.price} in datetime {itemAlert.LastTouchPrice} {direction}  \n Description: \n {itemAlert.Description} ");

                    //itemAlert.IsTemproprySuspended = NeedtoBeSusspended(isEmailSent);

                    _logger.LogInformation($"Touch Price {itemAlert.price} in datetime {itemAlert.LastTouchPrice} {direction}");
                }
            }

            _priceRepository.Add(listAlert);
        }

        private bool NeedtoBeSusspended(bool isEmailSent)
        {
            return isEmailSent;
        }

        private bool AlertSuspensionPeriod(DateTime LastTouchPrice)
        {
            var HourBehind = DateTime.Now.AddMinutes(-60);

            if (LastTouchPrice < DateTime.Now.AddMinutes(-60))
            {
                return true;
            }
        
            return false;
        }

        bool DosePriceConditionMeet(decimal price, decimal HighPrice, decimal LowPrice)
        {
            return price <= HighPrice && price >= LowPrice;
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
