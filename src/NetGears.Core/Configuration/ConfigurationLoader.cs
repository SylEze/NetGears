using System;
using System.IO;
using Newtonsoft.Json;
using NetGears.Core.Misc;

namespace NetGears.Core.Configuration
{
    public sealed class ConfigurationLoader : LazySingleton<ConfigurationLoader>, IConfigurationLoader
    {
        public T Load<T>(string configPath)
        {
            string data = string.Empty;
            T result;

            try
            {
                data = File.ReadAllText(configPath);
                result = JsonConvert.DeserializeObject<T>(data);
            }
            catch (FileNotFoundException)
            {
                Logger.Logger.Warn($"Cannot load {configPath}.");
                Logger.Logger.Warn("Creating an empty configuration file.");
                File.Create(configPath);
                return default(T);
            }
            catch (Exception e)
            {
                Logger.Logger.Error($"Cannot load {configPath}.", e);
                return default(T);
            }

            Logger.Logger.Info($"{configPath} has been loaded successfully.");

            return result;
        }

        public bool Save<T>(T configuration, string configPath)
        {
            string result = JsonConvert.SerializeObject(configuration);

            try
            {
                File.WriteAllText(configPath, result);
            }
            catch (Exception e)
            {
                Logger.Logger.Error($"Cannot save {configPath}.", e);
                return false;
            }

            Logger.Logger.Info($"{configPath} has been saved successfully.");

            return true;
        }
    }
}