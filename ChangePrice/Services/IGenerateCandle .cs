using ChangePrice.Models;

namespace ChangePrice.Services
{
    public interface IGenerateCandle
    {
        CandlestickModel ResponseToCustomLastCandle(string response);
    }
}
