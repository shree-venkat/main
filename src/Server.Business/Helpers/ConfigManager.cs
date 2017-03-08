namespace Server.Business.Helpers
{
    using Server.Business.Helpers.Interfaces;
    using System;
    using System.Configuration;

    public class ConfigManager : IConfigManager
    {
        public T Get<T>(string key) where T : IConvertible
        {
            try
            {
                var value = ConfigurationManager.AppSettings[key];
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch (Exception)
            {
                return default(T);
            }
        }
    }
}