using ChangePrice.Data.Dto;
using ChangePrice.DataBase;
using Microsoft.EntityFrameworkCore;

namespace ChangePrice.Data.Repository
{
    public class ReportUserAlertsDtoRepository:IReportUserAlertsDtoRepository
    {
        private TestCryptoCreatQueryContext _db;

        public ReportUserAlertsDtoRepository(TestCryptoCreatQueryContext context)
        {
            _db = context;
        }

        public List<ReportUserAlertsDto> GetAllReportUserAlerts()
        {
            var UserContext = _db.UserName.Include(u => u.Alert).ToList();

            var reportUserAlertsList = new List<ReportUserAlertsDto>();

            foreach (var user in UserContext)
            {

                foreach (var alertItem in user.Alert)
                {
                    var reportUserAlerts = new ReportUserAlertsDto();

                    reportUserAlerts.Name = user.Name;
                    reportUserAlerts.EmailAddress = user.EmailAddress;

                    reportUserAlerts.AlertId = alertItem.AlertId;
                    reportUserAlerts.UserId = alertItem.UserId;
                    reportUserAlerts.DateRegisterTime = alertItem.DateRegisterTime;
                    reportUserAlerts.Price = alertItem.Price;
                    reportUserAlerts.Description = alertItem.Description;
                    reportUserAlerts.LastTouchPrice = alertItem.LastTouchPrice;
                    reportUserAlerts.IsCrossedUp = alertItem.IsCrossedUp;
                    reportUserAlerts.PriceDifference = alertItem.PriceDifference;
                    reportUserAlerts.IsActive = alertItem.IsActive;
                    reportUserAlerts.IsTemproprySuspended = alertItem.IsTemproprySuspended;

                    reportUserAlertsList.Add(reportUserAlerts);

                }
            }
            return reportUserAlertsList;
        }


        //public List<ReportUserAlertsDto> GetAllReportUserAlerts()
        //{
        //    var UserContext = _db.UserName.Include(u => u.Alert).ToList();

        //    var reportUserAlertsList = new List<ReportUserAlertsDto>();


        //    foreach (var user in UserContext)
        //    {
        //        var alertList = new List<AlertDto>();
        //        foreach (var alertItem in user.Alert)
        //        {
        //            var alert = new AlertDto()
        //            {
        //                AlertId = alertItem.AlertId,
        //                UserId = user.UserId,
        //                DateRegisterTime = alertItem.DateRegisterTime,
        //                Price = alertItem.Price,
        //                Description = alertItem.Description,
        //                LastTouchPrice = alertItem.LastTouchPrice,
        //                IsCrossedUp = alertItem.IsCrossedUp,
        //                PriceDifference = alertItem.PriceDifference,
        //                IsActive = alertItem.IsActive,
        //                IsTemproprySuspended = alertItem.IsTemproprySuspended
        //            };
        //            alertList.Add(alert);
        //        }

        //        var reportUserAlerts = new ReportUserAlertsDto();
        //        reportUserAlerts.Name = user.Name;
        //        reportUserAlerts.EmailAddress = user.EmailAddress;
        //        reportUserAlerts.AlertlistForUser = alertList;

        //    }
        //}


        public List<ReportUserAlertsDto> GetReportUserAlertsByUserId(int userId)
        {
            var UserContext = _db.UserName.Include(u => u.Alert).Where(x => x.UserId == userId).First();

            var reportUserAlertsList = new List<ReportUserAlertsDto>();


            foreach (var alertItem in UserContext.Alert)
            {
                var reportUserAlerts = new ReportUserAlertsDto();

                reportUserAlerts.Name = UserContext.Name;
                reportUserAlerts.EmailAddress = UserContext.EmailAddress;

                reportUserAlerts.AlertId = alertItem.AlertId;
                reportUserAlerts.UserId = alertItem.UserId;
                reportUserAlerts.DateRegisterTime = alertItem.DateRegisterTime;
                reportUserAlerts.Price = alertItem.Price;
                reportUserAlerts.Description = alertItem.Description;
                reportUserAlerts.LastTouchPrice = alertItem.LastTouchPrice;
                reportUserAlerts.IsCrossedUp = alertItem.IsCrossedUp;
                reportUserAlerts.PriceDifference = alertItem.PriceDifference;
                reportUserAlerts.IsActive = alertItem.IsActive;
                reportUserAlerts.IsTemproprySuspended = alertItem.IsTemproprySuspended;

                reportUserAlertsList.Add(reportUserAlerts);
            }

            return reportUserAlertsList;
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
