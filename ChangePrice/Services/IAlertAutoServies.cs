using ChangePrice.Data.Dto;

namespace ChangePrice.Services
{
    public interface IAlertAutoServies
    {
        void TrackPriceAlertAuto();

        void AddPriceRandNumbers( string userId, int PriceSteps);

        void RemovePriceRandNumbers(string userId);
    }
}
