namespace ECommerce.Catalog.InfrastructureAdapter.Out.MongoDB.Constants
{    public static class ConstantsMongo
    {
        public const string MongoDBConnection = "MongoDBConnection";
        public const string MongoDataBaseName = "admin";
        public const int NumberOfTries = 5;
        public const int TimeBaseForRetryInterval = 2;
    }
}
