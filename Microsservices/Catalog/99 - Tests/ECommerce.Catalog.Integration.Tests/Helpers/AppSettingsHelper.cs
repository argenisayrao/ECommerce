using Microsoft.Extensions.Configuration;

namespace ECommerce.Catalog.Integration.Tests.Helpers
{
    public static class AppSettingsHelper
    {
        public static IConfigurationRoot GetAppSettings()
        {
            var a = SolutionPathHelper.TryGetSolutionDirectoryInfo().FullName;

            var builder = new ConfigurationBuilder()
                     .SetBasePath(Path.Combine(SolutionPathHelper.TryGetSolutionDirectoryInfo().FullName,
                     @"Microsservices\Catalog\2 - Infrastructure\In\ECommerce.Catalog.InfrastructureAdapter.In.Bus.Kafka"))
                    .AddJsonFile($"appsettings.json", true, true)
                    .AddEnvironmentVariables();
            var configuration = builder.Build();

            return configuration;
        }
    }
}
