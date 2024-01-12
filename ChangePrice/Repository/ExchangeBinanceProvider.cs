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
using ChangePrice.Services;

namespace ChangePrice.Repository
{
    public class ExchangeBinanceProvider : IExchangeProvider
    {
        private IGenerateCandle _generateCandle;


        private readonly IConfiguration _configuration;

        private readonly string _tradingPair;
        private readonly string _interval;
        private readonly int _limit;

        public ExchangeBinanceProvider(IGenerateCandle generateCandle, IConfiguration configuration)
        {
            _generateCandle = generateCandle;
            _configuration = configuration;

            _tradingPair = _configuration.GetValue<string>("CandlestickRequest:tradingPair");
            _interval = _configuration.GetValue<string>("CandlestickRequest:interval");
            _limit = _configuration.GetValue<int>("CandlestickRequest:limit");

        }
        public CandlestickModel GetLastCandle()
        {
            var lastCandle = new CandlestickModel();
            try
            {
                //string tradingPair = "BTCUSDT";
                //string interval = "15m";
                //int limit = 2;

                var client = new HttpClient();
                var requestUri = $"https://api.binance.com/api/v3/klines?symbol={_tradingPair}&interval={_interval}&limit={_limit}";
                var response = client.GetStringAsync(requestUri).Result;

                lastCandle = _generateCandle.ResponseToCustomLastCandle(response);

                return lastCandle;
            }

            catch (HttpRequestException e)
            {
                Console.WriteLine($"Exception Caught! ExchangeBinanceProvider GetLastCandle");
                Console.WriteLine($"Message :{e.Message} ");
            }
            return lastCandle;
        }

        public List<CandlestickModel> GetCandlelList()
        {
            throw new NotImplementedException();
        }
    }
}

