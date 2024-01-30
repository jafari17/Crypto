namespace ChangePrice.Models
{
    public class CandlestickModel
    {
        //public int Id { get; set; }
        //public long OpenTime { get; set; }

        public decimal OpenPrice { get; set; }
        public decimal HighPrice { get; set; }
        public decimal LowPrice { get; set; }
        public decimal ClosePrice { get; set; }

        //public decimal Volume { get; set; }
        //public long CloseTime { get; set; }

        //public decimal QuoteAssetVolume { get; set; }
        //public long NumberOfTrades { get; set; }

        //public decimal TakerBuyQuoteAssetVolume { get; set; }
        //public decimal TakerBuyBaseAssetVolume { get; set; }

        //public int Ignore { get; set; }
    }
}