using ChangePrice.Data.Dto;

namespace ChangePrice.Data.Repository
{
    public interface IReportUserAlertsDtoRepository
    {
        List<ReportUserAlertsDto> GetAllReportUserAlerts();
        List<ReportUserAlertsDto> GetReportUserAlertsByUserId(string userId);
        void Save();

    }
}
