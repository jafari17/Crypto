using ChangePrice.Data.Dto;
using ChangePrice.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Xml.Linq;

namespace ChangePrice.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private TestCryptoCreatQueryContext _db;

        public UserRepository(TestCryptoCreatQueryContext context)
        {
            _db = context;
        }

        public List<UserDto> GetAllUser()
        {
            var userList = _db.UserName.ToList();

            List< UserDto > userDtoList = new List< UserDto >();

            foreach (var userItem in userList)
            {
                UserDto userDto = new UserDto()
                {
                    UserId = userItem.UserId,
                    EmailAddress = userItem.EmailAddress,
                    Name = userItem.Name,
                    IsActive = userItem.IsActive
                };
                userDtoList.Add(userDto);
            }

            return userDtoList;
        }

        public UserDto GetUserById(int userId)
        {
            var user = _db.UserName.Find(userId);
            UserDto userDto = new UserDto()
            {
                UserId = user.UserId,
                EmailAddress = user.EmailAddress,
                Name = user.Name,
                IsActive = user.IsActive
            };
            return userDto;
        }
        public void InsertUser(UserName User)
        {
            _db.UserName.Add(User);
        }

        public void UpdateUser(UserName User)
        {
            _db.Entry(User).State = EntityState.Modified;
        }
        public void DeleteUser(UserName User)
        {
            _db.Entry(User).State = EntityState.Deleted;

        }

        public void DeleteUserById(int userId)
        {
            var user = _db.UserName.Find(userId);
            if (user != null)
            {
                DeleteUser(user);
            }
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
        //    var UserContext = db.UserName.Include(u => u.Alert).ToList();

        //    var reportUserAlertsList = new List<ReportUserAlertsDto>();

        //    foreach (var user in UserContext)
        //    {
        //        var alertList = new List<AlertDto>();
        //        foreach (var alertItem in user.Alert)
        //        {


        //            var alert = new AlertDto() 
        //            {
        //                AlertId= alertItem.AlertId,
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

        //            reportUserAlerts.Name = user.Name;
        //            reportUserAlerts.EmailAddress = user.EmailAddress;
        //            reportUserAlerts.Alertlist = alertList;

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
