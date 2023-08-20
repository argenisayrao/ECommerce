using ECommerce.Application.DomainModel.Notifiables;

namespace ECommerce.Application.UseCase.Result
{
    public class NotificationResult
    {
        public NotificationResult(bool success, string message, IReadOnlyCollection<FluentValidator.Notification> notifications)
        {
            Success = success;
            Message = message;

            foreach (var notification in notifications)
            {
                Notifications.Add(new NotificationData
                    (notification.Property, notification.Message));
            }
        }

        public bool Success { get; private set; }
        public string Message { get; private set; }
        public List<NotificationData> Notifications { get; private set; } = new();
    }
}
