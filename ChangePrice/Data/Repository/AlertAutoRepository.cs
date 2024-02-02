using ChangePrice.Data.DataBase;
using ChangePrice.Data.Dto;
using ChangePrice.Model_DataBase;
using ChangePrice.Models;
using Microsoft.EntityFrameworkCore;

namespace ChangePrice.Data.Repository
{
    public class AlertAutoRepository : IAlertAutoRepository
    {
        private CryptoDbContext _db;

        public AlertAutoRepository(CryptoDbContext context)
        {
            _db = context;
        }

        public List<AlertAuto> GetAllAlertAuto()
        {
            return _db.AlertAuto.ToList();
        }


        public List<AlertAutoDto> GetAllAlertAutoDto()
        {
            var alertList = _db.AlertAuto.ToList();

            List<AlertAutoDto> alertAutoDtoList = new List<AlertAutoDto>();

            foreach (var alertAutoItem in alertList)
            {
                AlertAutoDto alertAutoDto = new AlertAutoDto()
                {
                    AlertAutoId = alertAutoItem.AlertAutoId,
                    UserId = alertAutoItem.UserId,
                    PriceAlert = alertAutoItem.PriceAlert,
                    PriceSteps = alertAutoItem.PriceSteps,
                    NotificationActive = alertAutoItem.NotificationActive,
                    isActive = alertAutoItem.isActive

                };
                alertAutoDtoList.Add(alertAutoDto);
            }
            return alertAutoDtoList;


        }

        public AlertAutoDto GetAlertAutoById(int alertId)
        {
            var alertAutoItem = _db.AlertAuto.Find(alertId);

            AlertAutoDto alertAutoDto = new AlertAutoDto()
            {
                AlertAutoId = alertAutoItem.AlertAutoId,
                UserId = alertAutoItem.UserId,
                PriceAlert = alertAutoItem.PriceAlert,
                PriceSteps = alertAutoItem.PriceSteps,
                NotificationActive = alertAutoItem.NotificationActive,
                isActive = alertAutoItem.isActive
            };

            return alertAutoDto;
        }

        public void InsertAlertAuto(AlertAuto alertAuto)
        {
            _db.AlertAuto.Add(alertAuto);
        }

        public void UpdateAlertAuto(AlertAuto alertAuto)
        {
            _db.Entry(alertAuto).State = EntityState.Modified;
        }
        public void DeleteAlertAuto(AlertAuto alertAuto)
        {
            _db.Entry(alertAuto).State = EntityState.Deleted;
        }

        public void DeleteAlertAutoById(int alertAutoId)
        {
            var alertAuto = _db.AlertAuto.Find(alertAutoId);
            if (alertAuto != null)
            {
                DeleteAlertAuto(alertAuto);
            }
        }
        public void Save()
        {
            _db.SaveChanges();

        }
    }
}