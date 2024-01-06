using ChangePrice.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Net;

namespace ChangePrice.Repository
{
    public class ExchangeBinanceProvider : IExchangeProvider
    {
        
        public CandlestickModel GetLastCandle()
        {
            CandlestickModel candlestickModel = new CandlestickModel();
            try
            {
                string tradingPair = "BTCUSDT";
                string interval = "15m";
                int limit = 2;

                var client = new HttpClient();
                var requestUri = $"https://api.binance.com/api/v3/klines?symbol={tradingPair}&interval={interval}&limit={limit}";
                var response = client.GetStringAsync(requestUri).Result;

                var data = JsonConvert.DeserializeObject<string[][]>(response);

                decimal HighLastCandel = 0;
                decimal LowLastCandel=0;


                foreach (var item in data)
                {
                    //candlestickModel.OpenTime = Convert.ToInt64(item[0]);
                    
                    candlestickModel.HighPrice = Math.Max(HighLastCandel, decimal.Parse(item[2]));
                    candlestickModel.LowPrice = Math.Min(LowLastCandel, decimal.Parse(item[3]));
                    candlestickModel.ClosePrice = decimal.Parse(item[4]);

                    //candlestickModel.Volume = decimal.Parse(item[5], CultureInfo.InvariantCulture);
                    //candlestickModel.CloseTime = Convert.ToInt64(item[6]);
                    //candlestickModel.QuoteAssetVolume = decimal.Parse(item[7], CultureInfo.InvariantCulture);
                    //candlestickModel.NumberOfTrades = Convert.ToInt64(item[8]);
                    //candlestickModel.TakerBuyQuoteAssetVolume = decimal.Parse(item[9], CultureInfo.InvariantCulture);
                    //candlestickModel.TakerBuyBaseAssetVolume = decimal.Parse(item[10], CultureInfo.InvariantCulture);
                    //candlestickModel.Ignore = Convert.ToInt32(item[11]);
                    
                    HighLastCandel = decimal.Parse(item[2]);
                    LowLastCandel = decimal.Parse(item[3]);

                    if (candlestickModel.OpenPrice == 0)
                    {
                        candlestickModel.OpenPrice = decimal.Parse(item[1]);
                    }
                }
                return candlestickModel;
            }

            catch (HttpRequestException e)
            {
                Console.WriteLine($"Exception Caught! ExchangeBinanceProvider GetLastCandle");
                Console.WriteLine($"Message :{e.Message} ");
            }
            return candlestickModel;
        }

        public List<CandlestickModel> GetCandlelList()
        {
            throw new NotImplementedException();
        }
    }
}

