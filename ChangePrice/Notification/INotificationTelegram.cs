using ChangePrice.Models;

namespace ChangePrice.Notification
{
    public interface INotificationTelegram
    {
        public bool SendTextMessageToChannel(string MassageTelegram);
    }
}
