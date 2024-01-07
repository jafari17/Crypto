namespace ChangePrice.Models
{
    public class EmailModel
    {
        public EmailModel(string toAddress, string subject, string body)
        {
            if (string.IsNullOrEmpty(toAddress))
            {
                throw new Exception("to address is null or Empty");
            }

            if (string.IsNullOrEmpty(subject))
            {
                throw new Exception("Subject is null or Empty");
            }

            ToAddress = toAddress;
            Subject = subject;
            Body = body;
        }

        public string ToAddress { get; set; } 
        public string Subject { get; set; } 
        public string Body { get; set; } 

    }
}
