using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuiAutomationFramework.Framework.Configuration
{
    /// <summary>
    /// Timeouts represents the timeouts.
    /// </summary>
    public class Timeouts
    {
        public double explicitly { get; set; }
        public double implicitly { get; set; }
        public double pageLoad { get; set; }
        public double script { get; set; }
    }
}
