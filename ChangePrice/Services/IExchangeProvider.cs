using System.Collections.Generic;
using ChangePrice.Models;

namespace ChangePrice.Services
{
    public interface IExchangeProvider
    {
        CandlestickModel GetLastCandle();
        List<CandlestickModel> GetCandlelList();
        string GetLastPrice();
    }
}
