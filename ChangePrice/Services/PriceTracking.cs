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
        private readonly ILogger _logger;
        public PriceTracking(IPriceRepository priceRepository, IExchangeProvider exchangeProvider, INotificationEmail notificationEmail, ILogger<PriceTracking> logger)
        {
            _priceRepository = priceRepository;
            _exchangeProvider = exchangeProvider;
            _notificationEmail = notificationEmail;
            _logger = logger;
        }


        public void TrackPriceListChanges() //Track
        {
            List<AlertModel> listAlert = _priceRepository.GetList();

            CandlestickModel candle = _exchangeProvider.GetLastCandle();

            foreach (var itemAlert in listAlert)
            {
                itemAlert.PriceDifference = itemAlert.price - candle.ClosePrice;

                if (itemAlert.IsActive == true && itemAlert.IsTemproprySuspended == false &&
                    DosePriceConditionMeet(itemAlert.price, candle.HighPrice, candle.LowPrice))

                {
                    itemAlert.LastTouchPrice = DateTime.Now;
                    itemAlert.IsCrossedUp = IsCrossedUp(itemAlert.price, candle.OpenPrice);
                    var direction = itemAlert.IsCrossedUp ? "UP" : "Down";

                    EmailModel emailModel = CreateEmailModel(price: itemAlert.price, emailAddress: itemAlert.EmailAddress,
                                            lastTouchPrice: itemAlert.LastTouchPrice, touchDirection: direction);

                    var isEmailSent = _notificationEmail.Send(emailModel);

                    itemAlert.IsTemproprySuspended = NeedtoBeSusspended(isEmailSent);
                    _logger.LogInformation($"Touch Price {itemAlert.price} in datetime {itemAlert.LastTouchPrice} {direction}");
                }
            }

            _priceRepository.Add(listAlert);
        }

        private bool NeedtoBeSusspended(bool isEmailSent)
        {
            return isEmailSent;
        }

        bool DosePriceConditionMeet(decimal price, decimal HighPrice, decimal LowPrice)
        {
            return price <= HighPrice && price >= LowPrice;
        }

        bool IsCrossedUp(decimal price, decimal openPrice)
        {
            return price >= openPrice;
        }

        EmailModel CreateEmailModel(decimal price, string emailAddress, DateTime lastTouchPrice, string touchDirection)
        {

            string ToAddres = emailAddress;
            string Subject = $"Touch Price {price}";
            string Body = $"Touch Price {price} in datetime {lastTouchPrice} {touchDirection}";

            EmailModel emailModel = new EmailModel(ToAddres, Subject, Body) { };

            return emailModel;
        }
    }
}
