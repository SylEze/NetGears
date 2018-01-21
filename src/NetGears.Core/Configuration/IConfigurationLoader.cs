using System;

namespace NetGears.Core.Configuration
{
    public interface IConfigurationLoader
    {
        T Load<T>(string configPath);

        bool Save<T>(T configuration, string configPath);
    }
}