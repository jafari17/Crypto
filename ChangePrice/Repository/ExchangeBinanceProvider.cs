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
using System.Net.Mail;

namespace ChangePrice.Repository
{
    public class ExchangeBinanceProvider : IExchangeProvider
    {
        private CandlestickModel _candlestickModel;

        private readonly IConfiguration _configuration;

        private readonly string _tradingPair;
        private readonly string _interval;
        private readonly int _limit;

        public ExchangeBinanceProvider(CandlestickModel candlestickModel, IConfiguration configuration)
        {
            _candlestickModel = candlestickModel;
            _configuration = configuration;

            _tradingPair = _configuration.GetValue<string>("CandlestickRequest:tradingPair");
            _interval = _configuration.GetValue<string>("CandlestickRequest:interval");
            _limit = _configuration.GetValue<int>("CandlestickRequest:limit");

        }
        public CandlestickModel GetLastCandle()
        {
            //CandlestickModel candlestickModel = new CandlestickModel();
            try
            {
                //string tradingPair = "BTCUSDT";
                //string interval = "15m";
                //int limit = 2;

                var client = new HttpClient();
                var requestUri = $"https://api.binance.com/api/v3/klines?symbol={_tradingPair}&interval={_interval}&limit={_limit}";
                var response = client.GetStringAsync(requestUri).Result;

                var data = JsonConvert.DeserializeObject<string[][]>(response);

                decimal HighLastCandel = 0;
                decimal LowLastCandel=0;


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

            catch (HttpRequestException e)
            {
                Console.WriteLine($"Exception Caught! ExchangeBinanceProvider GetLastCandle");
                Console.WriteLine($"Message :{e.Message} ");
            }
            return _candlestickModel;
        }

        public List<CandlestickModel> GetCandlelList()
        {
            throw new NotImplementedException();
        }
    }
}

