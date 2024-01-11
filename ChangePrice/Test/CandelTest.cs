using ChangePrice.Models;

namespace ChangePrice.Test
{
    public class CandelTest
    {
        public static CandlestickModel GetLastCandleTest()
        {
            CandlestickModel candlestickModel = new CandlestickModel()
            {
                OpenPrice = 40000,
                HighPrice = 41000,
                LowPrice = 39000,
                ClosePrice = 41100
            };

            return candlestickModel;
        }
    }
}
