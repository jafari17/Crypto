using ChangePrice.Models;
using Newtonsoft.Json;
namespace ChangePrice.Services
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

        public string GetLastPrice()
        {
            string lastPrice = "";

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
    }
}

