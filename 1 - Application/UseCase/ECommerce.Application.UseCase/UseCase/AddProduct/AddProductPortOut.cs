using ECommerce.Application.UseCase.Result;

namespace ECommerce.Application.UseCase.UseCase.AddProduct
{
    public class AddProductPortOut: NotificationResult
    {
        public AddProductPortOut(bool success, string message,
            IReadOnlyCollection<FluentValidator.Notification> notifications,
            Guid id,string name,double value)
            : base(success, message, notifications)
        {
            Id = id;
            Name = name;
            Value = value;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public double Value { get; private set; }
    }
}
