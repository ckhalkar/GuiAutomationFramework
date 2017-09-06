using GuiAutomationFramework.Framework.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuiAutomationFramework.Framework.Configuration
{
    /// <summary>
    /// RemoteServers represents the remote servers.
    /// </summary>
    public class RemoteServers
    {
        public Roles role { get; set; }
        public string url { get; set; }
        public string proxyChrome { get; set; }
        public string proxyIE { get; set; }
        public bool useGrid { get; set; }
    }
}
