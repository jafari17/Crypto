using ChangePrice.Models;

namespace ChangePrice.Notification
{
    public interface INotificationEmail
    {
        void Send(EmailModel emailModel);
        
    }
}
