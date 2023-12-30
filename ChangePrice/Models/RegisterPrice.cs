namespace ChangePrice.Models
{
    public class RegisterPrice
    {
        public DateTime DateRegisterTime { get; set; }
        public decimal price { get; set; }
        public string Description { get; set; }
        public string LastTouchPrice { get; set; }
    }
}
