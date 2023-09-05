using ECommerce.Catalog.Application.UseCase.Result;

namespace ECommerce.Catalog.Application.UseCase.UseCase.AddProduct
{
    public class AddProductPortOut : NotificationResult
    {
        public AddProductPortOut(bool success, string message,
            IReadOnlyCollection<FluentValidator.Notification> notifications,
            string id,string name, double value)
            : base(success, message, notifications)
        {
            Id = id;
            Name = name;
            Value = value;
        }
        public string Id { get; private set; }
        public string Name { get; private set; }
        public double Value { get; private set; }
    }
}
