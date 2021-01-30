using System.Collections.Generic;

namespace Task_2
{
    public class SettingsWrapper
    {
        public IEnumerable<Settings> Settings { get; set; }
        public bool IsSuccess { get; set; }
        public string Error { get; set; }

        public SettingsWrapper(IEnumerable<Settings> settings, bool isSuccess, string error = "")
        {
            Settings = settings;
            IsSuccess = isSuccess;
            Error = error;
        }
    }
}