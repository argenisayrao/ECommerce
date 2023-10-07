using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using FluentValidator;
using FluentValidator.Validation;
using ECommerce.Catalog.Application.DomainModel.Base;
using ECommerce.Catalog.Application.DomainModel.Notifiables;

namespace ECommerce.Catalog.Application.DomainModel.Entities
{
    [Serializable]
    [BsonCollection("Products")]
    public class Product : Notifiable
    {
        protected Product() { }
        public Product(Guid id, string name, double value)
        {
            Id = id;
            Name = name;
            Value = value;
            AddNotifications(new ValidationContract()

               .IsTrue(!Guid.Empty.ToString().Equals(Id.ToString()), nameof(Id),
               string.Format(NotificationMessage.PropertyInvalid, nameof(Id)))

                .HasMinLen(Name, 3, nameof(Name),
                    string.Format(NotificationMessage.HaveAtLeast, nameof(Name), "3"))

                .HasMaxLen(Name, 100, nameof(Name),
                string.Format(NotificationMessage.ExceededCharacters, nameof(Name), "100"))

                .IsGreaterThan(Value, 0, nameof(Value),
                 string.Format(NotificationMessage.PropertyInvalid, nameof(Value))));
        }

        [BsonId]
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public double Value { get; private set; }

        private bool valid()
        {
            return Value != 0;
        }
    }
}
