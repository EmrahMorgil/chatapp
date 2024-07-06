using Microsoft.Extensions.Configuration;

namespace Server.Shared;

public class Configuration
{
     public static T GetSettings<T>(string key)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        return configuration.GetSection(key).Get<T>();
    }
}
