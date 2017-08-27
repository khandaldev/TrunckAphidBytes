using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace AphidBytes.Core.Configuration
{
    internal class ConfigurationReader
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ConfigurationReader)); 

        public const string ConfigLocation = @"c:\config\aphid-config.txt";
        private static object _lockObject = new object();
        private static Dictionary<string, string> _availableValues = null;

        public static Dictionary<string, string> GetValues()
        {
            lock(_lockObject)
            {
                if (_availableValues == null)
                {
                    _availableValues = ReadFile();
                }

                return _availableValues;
            }
        }
        public static Dictionary<string, string> ReadFile()
        {
            var configContents = File.ReadAllLines(ConfigLocation); 
            if(configContents == null || !configContents.Any())
            {
                return null;
            }

            var configOptions = new Dictionary<string, string>(); 
            for (var lineIndex = 0; lineIndex < configContents.Length; lineIndex++)
            {
                if (string.IsNullOrWhiteSpace(configContents[lineIndex]))
                {
                    Log.Debug("Skipping empty line in configuration file");
                    continue; 
                }

                var configSplit = configContents[lineIndex].Split('='); 
                if (configSplit == null || configSplit.Length != 2)
                {
                    Log.Warn($"Configuration file parsing error, line invalid {configContents[lineIndex]}"); 
                    continue; 
                }

                if (string.IsNullOrWhiteSpace(configSplit[0]))
                {
                    Log.Warn($"Configuration file parsing error, key is empty {configContents[lineIndex]}");
                }

                var keyValue = configSplit[0].Trim();
                var valueValue = string.IsNullOrWhiteSpace(configSplit[1])
                    ? null
                    : configSplit[1].Trim();
                Log.Info($"Configuration file loaded value, key={keyValue} value={valueValue}");

                if (configOptions.ContainsKey(keyValue))
                {
                    configOptions[keyValue] = valueValue; 
                }
                else
                {
                    configOptions.Add(keyValue, valueValue); 
                }
            }

            return configOptions;
        } 
    }
}
