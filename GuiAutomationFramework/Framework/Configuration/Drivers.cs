using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuiAutomationFramework.Framework.Configuration
{
    public class Drivers
    {
        public string name { get; set; }
        public Proxies proxy { get; set; }
        public List<Capabilities> Capabilities { get; set; }
    }
}
