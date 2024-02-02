﻿namespace ChangePrice.Data.Dto
{
    public class ReportUserAlertsDto
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        //public List<AlertDto> AlertlistForUser { get; set; }

        public int AlertId { get; set; }
        public string? UserId { get; set; }
        public DateTime? DateRegisterTime { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
        public DateTime? LastTouchPrice { get; set; }
        public bool? IsCrossedUp { get; set; }
        public decimal? PriceDifference { get; set; }
        public bool? IsActive { get; set; }
        public bool IsAutoSet { get; set; }
        public bool? IsTemproprySuspended { get; set; }
    }
}
