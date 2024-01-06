namespace ChangePrice.Models
{
    public class RegisterPriceModel
    {
        public RegisterPriceModel()
        {
            IsNotification = false;
            IsActive = true;
        }

        public Guid Id { get; set; }
        public DateTime DateRegisterTime { get; set; }
        public decimal price { get; set; }
        public string Description { get; set; }
        public DateTime LastTouchPrice { get; set; }
        public string TouchDirection { get; set; }  
        public decimal PriceDifference { get; set; }
        public  bool IsNotification { get; set; }
        public bool IsActive { get; set; }
    }
}
