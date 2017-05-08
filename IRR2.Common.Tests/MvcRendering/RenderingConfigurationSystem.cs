using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace IRR2.Common.Tests.MvcRendering
{
    class RenderingConfigurationSystem
    {
        public static string MsCorLibDirectory => RuntimeEnvironment.GetRuntimeDirectory();
        public static string MachineConfigurationDirectory => Path.Combine(MsCorLibDirectory, "Config");
        public static string MachineConfigPath => Path.Combine(MachineConfigurationDirectory, "machine.config");
        public static string RootWebConfigPath => Path.Combine(MachineConfigurationDirectory, "web.config");
    }
}
