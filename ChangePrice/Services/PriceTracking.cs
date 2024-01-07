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
            List<RegisterPriceModel> ListRP = _priceRepository.GetList();

            CandlestickModel Candel = _exchangeProvider.GetLastCandle();

            foreach (var ItemRP in ListRP)
            {
                ItemRP.PriceDifference = ItemRP.price - Candel.ClosePrice;

                if (ItemRP.IsActive == true && ItemRP.IsNotification == false &&
                    DosePriceConditionMeet(ItemRP.price, Candel.HighPrice, Candel.LowPrice))

                {

                    ItemRP.LastTouchPrice = DateTime.Now;


                    if (IsCrossedUp(ItemRP.price, Candel.OpenPrice))  // is crossed up
                    {
                        ItemRP.TouchDirection = "+";

                        EmailModel emailModel = CreatEmailModel(price: ItemRP.price, emailAddress: ItemRP.EmailAddress,
                                                lastTouchPrice: ItemRP.LastTouchPrice, touchDirection: ItemRP.TouchDirection);

                        //ItemRP.IsNotification = _notificationEmail.Send(emailModel);
                        


                        ///////////////////////////////////////////////////////////////////////////////
                        ItemRP.IsNotification = true;
                        _logger.LogWarning($"Touch Price {ItemRP.price} in datetime {ItemRP.LastTouchPrice} {ItemRP.TouchDirection}");
                        ////////////////////////////////////////////////////////////////////////

                        _logger.LogInformation($"Touch Price {ItemRP.price} in datetime {ItemRP.LastTouchPrice} {ItemRP.TouchDirection}");
                    }

                    if (IsCrossedDown(ItemRP.price, Candel.OpenPrice))
                    {
                        ItemRP.TouchDirection = "-";


                        EmailModel emailModel = CreatEmailModel(price: ItemRP.price, emailAddress: ItemRP.EmailAddress,
                                                lastTouchPrice: ItemRP.LastTouchPrice, touchDirection: ItemRP.TouchDirection);

                        //ItemRP.IsNotification = _notificationEmail.Send(emailModel);


                        //////////////////////////////////////////////////////////////////////////////////////
                        _logger.LogWarning($"Touch Price {ItemRP.price} in datetime {ItemRP.LastTouchPrice} {ItemRP.TouchDirection}");
                        ItemRP.IsNotification= true;

                        ////////////////////////////////////////////////////////////////////////////////////////

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

        EmailModel CreatEmailModel(decimal price, string emailAddress, DateTime lastTouchPrice, string touchDirection)
        {

            string ToAddres = emailAddress;
            string Subject = $"Touch Price {price}";
            string Body = $"Touch Price {price} in datetime {lastTouchPrice} {touchDirection}";

            EmailModel emailModel = new EmailModel(ToAddres, Subject, Body) { };

            return emailModel;
        }
    }
}
