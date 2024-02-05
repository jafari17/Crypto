using ChangePrice.Data.Dto;
using ChangePrice.ModelDataBase;

namespace ChangePrice.Data.Repository
{
    public interface IAlertAutoRepository
    {
        List<AlertAutoDto> GetAllAlertAutoDto();
        AlertAutoDto GetAlertAutoById(int alertId);

        List<AlertAuto> GetAllAlertAuto();
        void InsertAlertAuto(AlertAuto alert);
        void UpdateAlertAuto(AlertAuto alert);

        void DeleteAlertAutoById(int alertAutoId);
        void DeleteAlertAuto(AlertAuto alertAuto);
        void Save();
    }
}
