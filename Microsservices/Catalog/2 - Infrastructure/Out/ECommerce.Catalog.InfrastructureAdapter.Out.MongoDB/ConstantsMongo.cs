namespace ECommerce.Catalog.InfrastructureAdapter.Out.MongoDB
{
    public static class ConstantsMongo
    {
        public const string MongoDBConnection = "MongoDBConnection";
        public const string MongoDataBaseName = "Catalog";
        public const int NumberOfTries = 5;
        public const int TimeBaseForRetryInterval = 2;
    }
}
