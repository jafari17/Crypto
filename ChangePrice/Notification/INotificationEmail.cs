using ChangePrice.Models;

namespace ChangePrice.Notification
{
    public interface INotificationEmail
    {
        bool Send(EmailModel emailModel);
        
    }
}
