using ChangePrice.Data.Dto;
using ChangePrice.DataBase;
using Microsoft.EntityFrameworkCore;

namespace ChangePrice.Data.Repository
{
    public class AlertRepository : IAlertRepository
    {
        private TestCryptoCreatQueryContext _db;

        public AlertRepository(TestCryptoCreatQueryContext context)
        {
            _db = context;
        }

        public List<AlertDto> GetAll()
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
        public void InsertAlert(Alert alert)
        {
            _db.Alert.Add(alert);

        }

        public void UpdateAlert(Alert alert)
        {
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
