using System;
using System.Collections.Generic;
using System.Text;

namespace Faberton.Energy.Worker.Helpers
{
    public static class AzureFunctions
    {
        public static string GetSetting(string settingName) =>
            Environment.GetEnvironmentVariable(settingName, EnvironmentVariableTarget.Process);
    }
}
