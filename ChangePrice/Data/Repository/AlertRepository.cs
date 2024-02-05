using ChangePrice.Data.DataBase;
using ChangePrice.Data.Dto;
using ChangePrice.Models;
using Microsoft.EntityFrameworkCore;
using ChangePrice.ModelDataBase;
using ChangePrice.ModelDataBase;
namespace ChangePrice.Data.Repository
{
    public class AlertRepository : IAlertRepository
    {
        private CryptoDbContext _db;

        public AlertRepository(CryptoDbContext context)
        {
            _db = context;
        }
        
        public List<AlertDto> GetAllAlertDto()
        {
            var alertList = _db.Alert.ToList();

            List<AlertDto> alertDtoList = new List<AlertDto>();

            foreach (var alertItem in alertList)
            {
                AlertDto alertDto = new AlertDto()
                {
                    AlertId = alertItem.AlertId,
                    UserId = alertItem.UserId,
                    DateRegisterTime = alertItem.DateRegisterTime,
                    Price = alertItem.Price,
                    Description = alertItem.Description,
                    LastTouchPrice = alertItem.LastTouchPrice,
                    IsCrossedUp = alertItem.IsCrossedUp,
                    PriceDifference = alertItem.PriceDifference,
                    IsActive = alertItem.IsActive,
                    IsTemproprySuspended = alertItem.IsTemproprySuspended
                };
                alertDtoList.Add(alertDto);
            }
            return alertDtoList;
        }
        public AlertDto GetAlertById(int alertId)
        {
            var alert = _db.Alert.Find(alertId);

            AlertDto alertDto = new AlertDto()
            {
                AlertId = alert.AlertId,
                UserId = alert.UserId,
                DateRegisterTime = alert.DateRegisterTime,
                Price = alert.Price,
                Description = alert.Description,
                LastTouchPrice = alert.LastTouchPrice,
                IsCrossedUp = alert.IsCrossedUp,
                PriceDifference = alert.PriceDifference,
                IsActive = alert.IsActive,
                IsTemproprySuspended = alert.IsTemproprySuspended
            };

            return alertDto;
        }

        public List<Alert> GetAllAlert()
        {
            return _db.Alert.ToList();
        }
        public void InsertAlert(Alert alert)
        {
            _db.Alert.Add(alert);

        }

        public void UpdateAlert(Alert alert)
        {
            _db.Entry(alert).State = EntityState.Modified;
        }
        public void UpdateAlert(ReportUserAlertsDto reportUserAlertsDto)
        {
            var alert = _db.Alert.Find(reportUserAlertsDto.AlertId);

            //alert.AlertId = reportUserAlertsDto.AlertId;
            alert.UserId = reportUserAlertsDto.UserId;
            alert.DateRegisterTime = reportUserAlertsDto.DateRegisterTime;
            alert.Price = reportUserAlertsDto.Price;
            alert.Description = reportUserAlertsDto.Description;
            alert.LastTouchPrice = reportUserAlertsDto.LastTouchPrice;
            alert.IsCrossedUp = reportUserAlertsDto.IsCrossedUp;
            alert.PriceDifference = reportUserAlertsDto.PriceDifference;
            alert.IsActive = reportUserAlertsDto.IsActive;
            alert.IsTemproprySuspended = reportUserAlertsDto.IsTemproprySuspended;

            _db.Entry(alert).State = EntityState.Modified;
        }
        public void DeleteAlert(Alert alert)
        {
            _db.Entry(alert).State = EntityState.Deleted;
        }

        public void DeleteAlertById(int alertId)
        {
            var alert = _db.Alert.Find(alertId);
            if (alert != null)
            {
                DeleteAlert(alert);
            }
        }
        public void Save()
        {
            _db.SaveChanges();

        }


    }
}
