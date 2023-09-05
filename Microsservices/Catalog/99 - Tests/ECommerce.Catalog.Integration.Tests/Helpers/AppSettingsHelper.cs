using Microsoft.Extensions.Configuration;

namespace ValeECOS.ExternalInterface.GarbageCollection.TestHelpers.AppSettingsConfigHelper
{
    public static class AppSettingsHelper
    {
        public static IConfigurationRoot GetAppSettings()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Path.Combine(SolutionPathHelper.TryGetSolutionDirectoryInfo().FullName,
                     @"ECommerce.Catalog.InfrastructureAdapter.In.Bus.Kafka"))
                 .AddJsonFile("appsettings.json")
                 .Build();
        }
    }
}
