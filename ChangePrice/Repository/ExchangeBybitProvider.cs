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
    public class ExchangeBybitProvider : IExchangeProvider
    {
        private CandlestickModel _candlestickModel;

        private readonly IConfiguration _configuration;

        private readonly string _tradingPair;
        private readonly string _interval;
        private readonly int _limit;

        public ExchangeBybitProvider(CandlestickModel candlestickModel, IConfiguration configuration)
        {
            _candlestickModel = candlestickModel;
            _configuration = configuration;

            _tradingPair = _configuration.GetValue<string>("CandlestickRequest:tradingPair");
            _interval = _configuration.GetValue<string>("CandlestickRequest:interval");
            _limit = _configuration.GetValue<int>("CandlestickRequest:limit");

        }
        public CandlestickModel GetLastCandle()
        {
            
            try
            {


                var client = new HttpClient();
                var requestUri = $"https://api.bybit.com/v5/market/kline?symbol={_tradingPair}&interval={_interval}&limit={_limit}";
                var response = client.GetStringAsync(requestUri).Result;

                Console.WriteLine(response);

                dynamic data = JsonConvert.DeserializeObject(response);



                dynamic data2 = data.result.list;

                dynamic data3 = JsonConvert.DeserializeObject(data2);


                decimal HighLastCandle = 0;
                decimal LowLastCandel = 0;


                foreach (var item in data.result.list)
                {

                    _candlestickModel.HighPrice = Math.Max(HighLastCandle, decimal.Parse(item[2]));
                    _candlestickModel.LowPrice = Math.Min(LowLastCandel, decimal.Parse(item[3]));
                    _candlestickModel.ClosePrice = decimal.Parse(item[4]);


                    HighLastCandle = decimal.Parse(item[2]);
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

