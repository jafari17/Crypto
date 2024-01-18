using ChangePrice.Data.Dto;
using ChangePrice.DataBase;

namespace ChangePrice.Data.Repository
{
    public interface IUserRepository
    {
        
        List<UserDto> GetAllUser();
        UserDto GetUserById(int userId);
        void InsertUser(UserName user);
        void UpdateUser(UserName user);
        void DeleteUserById(int userId);
        void DeleteUser(UserName user);


        List<ReportUserAlertsDto> GetAllReportUserAlerts();
        List<ReportUserAlertsDto> GetReportUserAlertsByUserId(int userId);
        void Save();
    }
}
