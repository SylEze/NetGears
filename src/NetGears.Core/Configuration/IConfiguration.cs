using System;

namespace NetGears.Core.Configuration
{
    public interface IConfiguration
    {
        T Load<T>(string configPath);

        bool Save<T>(T configuration, string configPath);
    }
}