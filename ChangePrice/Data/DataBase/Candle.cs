﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ChangePrice.DataBase
{
    public partial class Candle
    {
        public int CandleId { get; set; }
        public long? OpenTime { get; set; }
        public decimal? OpenPrice { get; set; }
        public decimal? HighPrice { get; set; }
        public decimal? LowPrice { get; set; }
        public decimal? ClosePrice { get; set; }
        public int? Volume { get; set; }
        public long? CloseTime { get; set; }
    }
}