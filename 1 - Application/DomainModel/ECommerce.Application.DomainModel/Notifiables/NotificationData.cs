namespace ECommerce.Application.DomainModel.Notifiables
{
    public class NotificationData
    {
        public NotificationData(string property, string message)
        {
            Property = property;
            Message = message;
        }

        public string Property { get; private set; }
        public string Message { get; private set; }
    }
}
