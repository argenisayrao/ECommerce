using MongoDB.Bson;

namespace ECommerce.Catalog.Application.UseCase.UseCase.GetProductById
{
    public class GetProductByIdPortOut
    {
        public GetProductByIdPortOut(ObjectId id, string name, double value)
        {
            Id = id;
            Name = name;
            Value = value;
            IsExists = true;
        }

        public GetProductByIdPortOut(bool isExists)
        {
            IsExists = isExists;
        }

        public ObjectId Id { get; private set; }
        public string Name { get; private set; }
        public double Value { get; private set; }
        public bool IsExists { get; private set; }
    }
}
