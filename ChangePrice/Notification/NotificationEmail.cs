using System.Net.Mail;
using System.Net;
using ChangePrice.Models;

namespace ChangePrice.Notification
{

    public class NotificationEmail : INotificationEmail
    {
        private readonly IConfiguration _configuration;

        private readonly string _FromEmailAddress;
        private readonly string _Password;
        private readonly string _SmtpClient;
        private readonly int _port;


        private readonly ILogger _logger;
        public NotificationEmail(IConfiguration configuration, ILogger logger)
        {
            _configuration = configuration;
            _logger = logger;


            _FromEmailAddress = _configuration.GetValue<string>("EmailConfig:FromEmailAddress");
            _Password = _configuration.GetValue<string>("EmailConfig:Password");
            _SmtpClient = _configuration.GetValue<string>("EmailConfig:SmtpClient");

            _port = _configuration.GetValue<int>("EmailConfig:Port");


        }
        public void Send(EmailModel emailModel)
        {

            MailMessage message = new MailMessage();
            message.From = new MailAddress(_FromEmailAddress);
            message.To.Add(new MailAddress(emailModel.ToAddress));
            message.Subject = emailModel.Subject;
            message.Body = emailModel.Body;



            SmtpClient client = new SmtpClient(_SmtpClient, _port);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(_FromEmailAddress, _Password);


            try
            {
                client.Send(message);
                //Console.WriteLine("Email sent successfully!");
                _logger.LogInformation("Email sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending email: {0}", ex.Message);
                _logger.LogError("Error sending email: {0}", ex.Message);
            }

        }

    }
}
