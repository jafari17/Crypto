using ChangePrice.Data.DataBase;
using ChangePrice.Data.Dto;
using Microsoft.EntityFrameworkCore;

namespace ChangePrice.Data.Repository
{
    public class ReportUserAlertsDtoRepository:IReportUserAlertsDtoRepository
    {
        private CryptoDbContext _db;

        public ReportUserAlertsDtoRepository(CryptoDbContext context)
        {
            _db = context;
        }

        public List<ReportUserAlertsDto> GetAllReportUserAlerts()
        {

            var innerJoinResult = from u in _db.Users.ToList()   // outer sequence
                                  join a in _db.Alert.ToList()   //inner sequence 
                                  on u.Id equals a.UserId // key selector 
                                  select new ReportUserAlertsDto
                                  { // result selector 
                                      Name = u.UserName,
                                      EmailAddress = u.Email,
                                      AlertId = a.AlertId,
                                      UserId = a.UserId,
                                      DateRegisterTime = a.DateRegisterTime,
                                      Price = a.Price,
                                      Description = a.Description,
                                      LastTouchPrice = a.LastTouchPrice,
                                      IsCrossedUp = a.IsCrossedUp,
                                      PriceDifference = a.PriceDifference,
                                      IsActive = a.IsActive,
                                      IsTemproprySuspended = a.IsTemproprySuspended,
                                  };


            var reportAllAlertsList = innerJoinResult.ToList();

            return reportAllAlertsList;
        }


        public List<ReportUserAlertsDto> GetReportUserAlertsByUserId(string userId)
        {
            var innerJoinResult = from u in _db.Users.ToList()   // outer sequence
                                  join a in _db.Alert.ToList()   //inner sequence 
                                  on u.Id equals a.UserId // key selector 
                                  select new ReportUserAlertsDto
                                  { // result selector 
                                      Name = u.UserName,
                                      EmailAddress = u.Email,
                                      AlertId = a.AlertId,
                                      UserId = a.UserId,
                                      DateRegisterTime = a.DateRegisterTime,
                                      Price = a.Price,
                                      Description = a.Description,
                                      LastTouchPrice = a.LastTouchPrice,
                                      IsCrossedUp = a.IsCrossedUp,
                                      PriceDifference = a.PriceDifference,
                                      IsActive = a.IsActive,
                                      IsTemproprySuspended = a.IsTemproprySuspended,
                                  };


            var reportUserAlertsList = innerJoinResult.Where(x => x.UserId == userId).ToList();



            //var UserContext = _db.UserService.Include(u => u.Alert).Where(x => x.UserId == userId).First();

            //var reportUserAlertsList = new List<ReportUserAlertsDto>();


            //foreach (var alertItem in UserContext.Alert)
            //{
            //    var reportUserAlerts = new ReportUserAlertsDto();

            //    reportUserAlerts.Name = UserContext.Name;
            //    reportUserAlerts.EmailAddress = UserContext.EmailAddress;

            //    reportUserAlerts.AlertId = alertItem.AlertId;
            //    reportUserAlerts.UserId = alertItem.UserId;
            //    reportUserAlerts.DateRegisterTime = alertItem.DateRegisterTime;
            //    reportUserAlerts.Price = alertItem.Price;
            //    reportUserAlerts.Description = alertItem.Description;
            //    reportUserAlerts.LastTouchPrice = alertItem.LastTouchPrice;
            //    reportUserAlerts.IsCrossedUp = alertItem.IsCrossedUp;
            //    reportUserAlerts.PriceDifference = alertItem.PriceDifference;
            //    reportUserAlerts.IsActive = alertItem.IsActive;
            //    reportUserAlerts.IsTemproprySuspended = alertItem.IsTemproprySuspended;

            //    reportUserAlertsList.Add(reportUserAlerts);
            //}

            return reportUserAlertsList;
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
