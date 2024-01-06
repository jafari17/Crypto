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
        public PriceTracking(IPriceRepository priceRepository, IExchangeProvider exchangeProvider, INotificationEmail notificationEmail,ILogger logger)
        {
            _priceRepository = priceRepository;
            _exchangeProvider = exchangeProvider;
            _notificationEmail = notificationEmail;
            _logger = logger;
        }


        public void TrackPriceListChanges() //Track
        {
            List<RegisterPriceModel> ListRP = _priceRepository.GetList();

            CandlestickModel Candel = _exchangeProvider.GetLastCandle();
            
            foreach (var ItemRP in ListRP)
            {
                ItemRP.PriceDifference = ItemRP.price - Candel.ClosePrice;

                if (ItemRP.IsActive == true && ItemRP.IsNotification == true &&
                    DosePriceConditionMeet(ItemRP.price, Candel.HighPrice, Candel.LowPrice))
                    
                {
                    ItemRP.IsNotification = false;
                    ItemRP.LastTouchPrice = DateTime.Now;


                    if (IsCrossedUp(ItemRP.price, Candel.OpenPrice))  // is crossed up
                    {
                        ItemRP.TouchDirection = "+";

                        EmailModel emailModel = CreatEmailModel(ItemRP.price, ItemRP.LastTouchPrice, ItemRP.TouchDirection);
                        _notificationEmail.Send(emailModel);

                        _logger.LogInformation($"Touch Price {ItemRP.price} in datetime {ItemRP.LastTouchPrice} {ItemRP.TouchDirection}");
                    }

                    if (IsCrossedDown(ItemRP.price, Candel.OpenPrice))
                    {
                        ItemRP.TouchDirection = "-";


                        EmailModel emailModel = CreatEmailModel(ItemRP.price, ItemRP.LastTouchPrice, ItemRP.TouchDirection);
                        _notificationEmail.Send(emailModel);

                        _logger.LogInformation($"Touch Price {ItemRP.price} in datetime {ItemRP.LastTouchPrice} {ItemRP.TouchDirection}");
                    }
                }
            }

            _priceRepository.Add(ListRP);
        }
        bool DosePriceConditionMeet(decimal price, decimal HighPrice, decimal LowPrice)

        {
            return price <= HighPrice && price >= LowPrice;
        }

        bool IsCrossedUp(decimal price, decimal openPrice)
        {
            return price >= openPrice;
        }
        bool IsCrossedDown(decimal price, decimal openPrice)
        {
            return price <= openPrice;
        }

        EmailModel CreatEmailModel(decimal price, DateTime lastTouchPrice, string touchDirection )
        {

            string ToAddres = "jafari17@gmail.com";
            string Subject = $"Touch Price {price}";
            string Body = $"Touch Price {price} in datetime {lastTouchPrice} {touchDirection}";

            EmailModel emailModel = new EmailModel(ToAddres, Subject, Body) { };

            return emailModel;
        }
    }
}
