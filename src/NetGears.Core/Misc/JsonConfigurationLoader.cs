using System;
using System.IO;
using Newtonsoft.Json;

namespace NetGears.Core.Misc
{
    public class JsonConfigurationLoader
    {
        private static readonly Logger.Logger Logger = NetGears.Core.Logger.Logger.GetLogger<JsonConfigurationLoader>();
        
        public static T Load<T>(string configPath)
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
                Logger?.Warn($"{configPath} not found. Creating an empty configuration file.");
                result = default(T);
                using (var fs = File.Create(configPath))
                {
                    Logger?.Info($"{configPath} created.");
                }
            }
            catch (Exception ex)
            {
                Logger?.Error($"Error when loading {configPath}", ex);
                result = default(T);
            }

            Logger?.Info($"{configPath} loaded successfully.");

            return result;
        }

        public static bool Save<T>(T configuration, string configPath)
        {
            string result = JsonConvert.SerializeObject(configuration);

            try
            {
                File.WriteAllText(configPath, result);
            }
            catch (Exception ex)
            {
                Logger?.Error($"Cannot save {configPath}.", ex);
                return false;
            }

            Logger?.Info($"{configPath} saved successfully.");

            return true;
        }
    }
}