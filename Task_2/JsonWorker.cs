using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Text.Json;

namespace Task_2
{
    public class JsonWorker
    {
        private readonly string _settingsFilePath = "settings.json";
        private readonly string _resultFilePath = "result.json";
        public SettingsWrapper ReadSettings()
        {
            string fileContent;
            try
            {
                fileContent = File.ReadAllText(_settingsFilePath);
            }
            catch (UnauthorizedAccessException)
            {
                return new SettingsWrapper(null, false, $"{_settingsFilePath} file is read-only. " +
                                                        "-or- This operation is not supported on the current platform. " +
                                                        "-or- The caller does not have the required permission.");
            }
            catch (FileNotFoundException)
            {
                return new SettingsWrapper(null, false, $"The file {_settingsFilePath} was not found.");
            }
            catch (SecurityException)
            {
                return new SettingsWrapper(null, false, "The caller does not have the required permission.");
            }
            catch (IOException)
            {
                return new SettingsWrapper(null, false, $"I/O error occured while reading {_settingsFilePath}");
            }
            
            List<Settings> settings;
            try
            {
                settings = JsonSerializer.Deserialize<List<Settings>>(fileContent);
                if (settings == null)
                {
                    throw new JsonException();
                }
            }
            catch (ArgumentNullException)
            {
                return new SettingsWrapper(null, false, $"{_settingsFilePath} is damaged");
            }
            catch (JsonException)
            {
                return new SettingsWrapper(null, false, $"{_settingsFilePath} is damaged");
            }
            
            
            return new SettingsWrapper(settings, true);
        }

        public void WriteResult(Result result)
        {
            var content = JsonSerializer.Serialize(result);
            File.WriteAllText(_resultFilePath, content);

        }
    }
}