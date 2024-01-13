using ChangePrice.Models;
using Newtonsoft.Json;

namespace ChangePrice.Services
{
    public class GenerateCandle: IGenerateCandle
    {
        private CandlestickModel _candlestickModel;

        public GenerateCandle(CandlestickModel candlestickModel)
        {
            _candlestickModel = candlestickModel;
        }


        public CandlestickModel ResponseToCustomLastCandle(string response)
        {
            var data = JsonConvert.DeserializeObject<string[][]>(response);

            decimal HighLastCandel = 0;
            decimal LowLastCandel = 0;


            Console.WriteLine(data);

            foreach (var item in data)
            {
                //_candlestickModel.OpenTime = Convert.ToInt64(item[0]);

                _candlestickModel.HighPrice = Math.Max(HighLastCandel, decimal.Parse(item[2]));
                _candlestickModel.LowPrice = Math.Min(LowLastCandel, decimal.Parse(item[3]));
                _candlestickModel.ClosePrice = decimal.Parse(item[4]);

                //_candlestickModel.Volume = decimal.Parse(item[5], CultureInfo.InvariantCulture);
                //_candlestickModel.CloseTime = Convert.ToInt64(item[6]);
                //_candlestickModel.QuoteAssetVolume = decimal.Parse(item[7], CultureInfo.InvariantCulture);
                //_candlestickModel.NumberOfTrades = Convert.ToInt64(item[8]);
                //_candlestickModel.TakerBuyQuoteAssetVolume = decimal.Parse(item[9], CultureInfo.InvariantCulture);
                //_candlestickModel.TakerBuyBaseAssetVolume = decimal.Parse(item[10], CultureInfo.InvariantCulture);
                //_candlestickModel.Ignore = Convert.ToInt32(item[11]);

                HighLastCandel = decimal.Parse(item[2]);
                LowLastCandel = decimal.Parse(item[3]);

                if (_candlestickModel.OpenPrice == 0)
                {
                    _candlestickModel.OpenPrice = decimal.Parse(item[1]);
                }
            }
            return _candlestickModel;
        }

        public string ResponseToLastPrice(string response)
        {

            dynamic data = JsonConvert.DeserializeObject(response);

            string symbol = data.symbol;
            string price = data.price;

           

            return $"{symbol}: {price.Split('.')[0]}";
        }
    }
}
