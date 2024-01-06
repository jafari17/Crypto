using System.Collections.Generic;
using ChangePrice.Models;

namespace ChangePrice.Repository
{
    public interface IExchangeProvider
    {
        CandlestickModel GetLastCandle();
        List<CandlestickModel> GetCandlelList();


    }
}
