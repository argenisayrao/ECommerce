using FluentValidator;
using FluentValidator.Validation;
using ECommerce.Application.DomainModel.Notifiables;

namespace ECommerce.Application.DomainModel.Entities
{
    public class Product : Notifiable
    {
        public Product(Guid id, string name, double value)
        {
            Id = id;
            Name = name;
            Value = value;

            AddNotifications(new ValidationContract()
                .HasMinLen(Name, 3, nameof(Name),
                    string.Format(NotificationMessage.HaveAtLeast, nameof(Name), "3"))

            .HasMaxLen(Name, 100, nameof(Name),
                string.Format(NotificationMessage.ExceededCharacters, nameof(Name), "100"))

                .IsGreaterThan(Value, 0, nameof(Value),
                    string.Format(NotificationMessage.PropertyInvalid,nameof(Value))));
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public double Value { get; private set; }
    }
}
