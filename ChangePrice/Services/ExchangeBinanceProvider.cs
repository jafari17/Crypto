using ChangePrice.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;
namespace ChangePrice.Services
{
    public class ExchangeBinanceProvider : IExchangeProvider
    {
        private IGenerateCandle _generateCandle;
        private readonly IConfiguration _configuration;

        private IWebHostEnvironment _webHostEnvironment;

        private readonly string _tradingPair;
        private readonly string _interval;
        private readonly int _limit;


        public ExchangeBinanceProvider(IGenerateCandle generateCandle, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _generateCandle = generateCandle;
            _configuration = configuration;

            _tradingPair = _configuration.GetValue<string>("CandlestickRequest:tradingPair");
            _interval = _configuration.GetValue<string>("CandlestickRequest:interval");
            _limit = _configuration.GetValue<int>("CandlestickRequest:limit");
            _webHostEnvironment = webHostEnvironment;
        }
        public CandlestickModel GetLastCandle()
        {
            var lastCandle = new CandlestickModel();


            //if (_webHostEnvironment.IsDevelopment())
            //{
            //    return lastCandle;
            //}

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development" &&
                Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT2") == "Exchange")
            {
                return lastCandle;
            }


            try
            {
                bool isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
                bool isDevelopment2 = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT2") == "Exchange";

                var client = new HttpClient();
                string requestUri = $"https://api.binance.com/api/v3/klines?symbol={_tradingPair}&interval={_interval}&limit={_limit}";
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

        public string GetLastPriceAndSymbol()
        {
            string lastPrice = "";


            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development" &&
                Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT2") == "Exchange")
            {
                return lastPrice;
            }


            try
            {

                var client = new HttpClient();
                var requestUri = $"https://api.binance.com/api/v3/ticker/price?symbol=BTCUSDT";
                var response = client.GetStringAsync(requestUri).Result;

                lastPrice = _generateCandle.ResponseToLastPrice(response);


                return lastPrice;
            }

            catch (HttpRequestException e)
            {
                Console.WriteLine($"Exception Caught! ExchangeBinanceProvider GetLastCandle");
                Console.WriteLine($"Message :{e.Message} ");
            }
            return lastPrice;
        }

        public decimal GetLastPrice()
        {
            

            var client = new HttpClient();
            var requestUri = $"https://api.binance.com/api/v3/ticker/price?symbol=BTCUSDT";
            var response = client.GetStringAsync(requestUri).Result;

            dynamic data = JsonConvert.DeserializeObject(response);

            string symbol = data.symbol;
            string price =  data.price;

            decimal lastPrice = Convert.ToDecimal(price);

            return lastPrice;

        }
    }
}

