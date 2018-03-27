using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Infrastructure.Common.Interfaces;

namespace TypiconOnline.AppServices.Standard.Configuration
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        IConfiguration _configuration;
        public ConfigurationRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException("IConfiguration");
        }

        public T GetConfigurationValue<T>(string key)
        {
            return GetConfigurationValue(key, default(T), true);
        }

        public string GetConfigurationValue(string key)
        {
            return GetConfigurationValue<string>(key);
        }

        public T GetConfigurationValue<T>(string key, T defaultValue)
        {
            return GetConfigurationValue(key, defaultValue, false);
        }

        public string GetConfigurationValue(string key, string defaultValue)
        {
            return GetConfigurationValue<string>(key, defaultValue);
        }

        private T GetConfigurationValue<T>(string key, T defaultValue, bool throwException)
        {
            var value = _configuration[key];
            if (value == null)
            {
                if (throwException)
                    throw new KeyNotFoundException("Key " + key + " not found.");
                return defaultValue;
            }
            try
            {
                if (typeof(Enum).IsAssignableFrom(typeof(T)))
                    return (T)Enum.Parse(typeof(T), value);
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch (Exception ex)
            {
                if (throwException)
                    throw ex;
                return defaultValue;
            }
        }
    }
}
