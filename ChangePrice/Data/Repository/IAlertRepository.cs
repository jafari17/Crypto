using ChangePrice.Data.Dto;
using ChangePrice.DataBase;
using ChangePrice.Models;

namespace ChangePrice.Data.Repository
{
    public interface IAlertRepository
    {
        List<AlertDto> GetAll();
        AlertDto GetAlertById(int alertId);
        void InsertAlert(Alert alert);
        void UpdateAlert(Alert alert);
        void DeleteAlertById(int alertId);
        void DeleteAlert(Alert alert);
        void Save();
    }
}
