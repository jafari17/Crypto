namespace ChangePrice.Models
{
    public class RegisterPriceModel
    {
        public Guid Id { get; set; }
        public DateTime DateRegisterTime { get; set; }
        public decimal price { get; set; }
        public string Description { get; set; }
        public DateTime LastTouchPrice { get; set; }
        public string TouchDirection { get; set; }  
        public decimal PriceDifference { get; set; }
        public  bool IsNotification { get; set; } = true;
        public bool IsActive { get; set; } = false;
    }
}
