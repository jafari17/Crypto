using ChangePrice.Data.DataBase;
using ChangePrice.Data.Dto;
using ChangePrice.Models;

namespace ChangePrice.Data.Repository
{
    public interface IAlertRepository
    {
        List<AlertDto> GetAllAlertDto();
        AlertDto GetAlertById(int alertId);

        List<Alert> GetAllAlert();
        void InsertAlert(Alert alert);
        void UpdateAlert(Alert alert);
        void UpdateAlert(ReportUserAlertsDto reportUserAlertsDto);

        void DeleteAlertById(int alertId);
        void DeleteAlert(Alert alert);
        void Save();
    }
}
