namespace ChangePrice.Models
{
    public class AlertModel
    {
        public AlertModel()
        {
            IsTemproprySuspended = false;
            IsActive = true;
        }
        public Guid Id { get; set; }
        public DateTime DateRegisterTime { get; set; }
        public decimal Price { get; set; }
        public string EmailAddress { get; set; }
        public string Description { get; set; }
        public DateTime LastTouchPrice { get; set; }
        public bool IsCrossedUp { get; set; }  
        public decimal PriceDifference { get; set; }
        public bool IsActive { get; set; }
        public bool IsTemproprySuspended { get; set; }
    }
}
