using System;
using System.Globalization;

namespace AphidBytes.Core.Configuration
{
    public class ConfigurationValue<TValue>
    {
        private readonly string _key = string.Empty;
        private readonly TValue _defaultValue = default(TValue);

        public ConfigurationValue(string key, TValue defaultValue)
        {
            _key = key;
            _defaultValue = defaultValue;
        }
        public ConfigurationValue(string key)
            : this(key, default(TValue))
        {
        }

        public TValue Value
        {
            get
            {
                var configValues = ConfigurationReader.GetValues();
                var configValue = configValues[_key]; 
                if(string.IsNullOrWhiteSpace(configValue))
                {
                    return _defaultValue;
                }

                return (TValue)Convert.ChangeType(configValue, typeof(TValue), CultureInfo.InvariantCulture);
            }
        }
    }
}
