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
        public Product(string name, double value)
        {
            Name = name;
            Value = value;

            AddNotifications(new ValidationContract()
                .HasMinLen(Name, 3, nameof(Name),
                    string.Format(NotificationMessage.HaveAtLeast, nameof(Name), "3"))

            .HasMaxLen(Name, 100, nameof(Name),
                string.Format(NotificationMessage.ExceededCharacters, nameof(Name), "100"))

                .IsGreaterThan(Value, 0, nameof(Value),
                    string.Format(NotificationMessage.PropertyInvalid, nameof(Value))));
        }

        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; private set; }
        public string Name { get; private set; }
        public double Value { get; private set; }
    }
}
