using ChangePrice.Data.Dto;
using ChangePrice.Data.Repository;
using ChangePrice.Model_DataBase;
using ChangePrice.Models;
using ChangePrice.Notification;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Security.Claims;
using static Mysqlx.Crud.Order.Types;

namespace ChangePrice.Services
{
    public class AlertAutoServies : IAlertAutoServies
    {
        private IExchangeProvider _exchangeProvider;
        private IAlertRepository _alertRepository;
        private IAlertAutoRepository _alertAutoRepository;
        private INotificationTelegram _notificationTelegram;
        private INotificationEmail _notificationEmail;
        private IReportUserAlertsDtoRepository _reportUserAlertsDtoRepository;
        private readonly UserManager<IdentityUser> _userManager;
        public AlertAutoServies(IExchangeProvider exchangeProvider, IAlertRepository alertRepository,
            IReportUserAlertsDtoRepository reportUserAlertsDtoRepository, IAlertAutoRepository alertAutoRepository,
            INotificationTelegram notificationTelegram, UserManager<IdentityUser> userManager, INotificationEmail notificationEmail)
        {
            _exchangeProvider = exchangeProvider;
            _alertRepository = alertRepository;
            _reportUserAlertsDtoRepository = reportUserAlertsDtoRepository;
            _alertAutoRepository = alertAutoRepository;

            _notificationTelegram = notificationTelegram;
            _notificationEmail = notificationEmail;

            _userManager = userManager;

        }

        public void AddPriceRandNumbers(string userId)
        {
            RemovePriceRandNumbers(userId);
            int LastPrice = Convert.ToInt32(_exchangeProvider.GetLastPrice());

            int PriceThousand = GetPriceThousand(LastPrice,100);

            //var alertAuto = _alertAutoRepository.GetAllAlertAuto();

            AlertAuto alertAuto = new AlertAuto()
            {
                UserId = userId,
                PriceAlert = PriceThousand,
                PriceSteps = 100,
                NotificationActive = true,
                isActive = true
            };
            _alertAutoRepository.InsertAlertAuto(alertAuto);
            _alertAutoRepository.Save();
        }

        public void TrackPriceAlertAuto()
        {
            int LastPrice = Convert.ToInt32(_exchangeProvider.GetLastPrice());


            var AllAlertAuto = _alertAutoRepository.GetAllAlertAuto();
            

            
            foreach (var alertAuto in AllAlertAuto)
            {
                int PriceThousand = GetPriceThousand(LastPrice,alertAuto.PriceSteps);
                if (alertAuto.PriceAlert == PriceThousand && alertAuto.NotificationActive == true)
                {

                }
                else if(alertAuto.PriceAlert < PriceThousand && alertAuto.NotificationActive == true)
                {
                    alertAuto.PriceAlert = PriceThousand;
                    string direction = "↗";

                    var time = DateTime.Now;
                    string Message = CreateMessage(alertAuto.PriceAlert, direction, LastPrice)+ $"\n {time}" ;
                    _notificationTelegram.SendTextMessageToChannel(Message);
                    //_notificationEmail.Send(CreateEmailModel(Message, alertAuto.PriceAlert, alertAuto.UserId));

                }
                else if (alertAuto.PriceAlert > PriceThousand && alertAuto.NotificationActive == true)
                {
                    alertAuto.PriceAlert = PriceThousand;
                    string direction = "↘";

                    var time = DateTime.Now;
                    string Message = CreateMessage(alertAuto.PriceAlert, direction, LastPrice) + $"\n {time}";
                    _notificationTelegram.SendTextMessageToChannel(Message);
                    //_notificationEmail.Send(CreateEmailModel(Message, alertAuto.PriceAlert, alertAuto.UserId));

                }
                _alertAutoRepository.UpdateAlertAuto(alertAuto);
            }
            _alertAutoRepository.Save();
        }
        private string CreateMessage(int PriceAlert, string direction, int LastPrice)
        {
            string message = $"T: {PriceAlert} {direction} C: {LastPrice} ";

            return message;
        }

        EmailModel CreateEmailModel(string Message, int PriceAlert, string userId)
        {
            var User = _userManager.FindByIdAsync(userId).Result;
            string ToAddres = User.Email;
            string Subject = $"Enter the channel {PriceAlert}";
            string Body = Message;

            EmailModel emailModel = new EmailModel(ToAddres, Subject, Body) { };

            return emailModel;
        }

        public void RemovePriceRandNumbers(string userId)
        {
            var alertAuto = _alertAutoRepository.GetAllAlertAuto().Find(x => x.UserId == userId);

            if (alertAuto != null)
            {
                _alertAutoRepository.DeleteAlertAuto(alertAuto);
            }
        }

        public int GetPriceThousand(int LastPrice, int PriceSteps)
        {
            int PriceThousand = (LastPrice / PriceSteps) * PriceSteps;
            return PriceThousand;
        }
    }
}

