﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ChangePrice.Data.DataBase
{
    public partial class Alert
    {
        public int AlertId { get; set; }
        public string UserId { get; set; }
        public DateTime? DateRegisterTime { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
        public DateTime? LastTouchPrice { get; set; }
        public bool? IsCrossedUp { get; set; }
        public decimal? PriceDifference { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsTemproprySuspended { get; set; }
    }
}