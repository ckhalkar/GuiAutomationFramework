using GuiAutomationFramework.Framework.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuiAutomationFramework.Framework.Configuration
{
    public class Configuration
    {
        // The list of remote servers.
        public List<RemoteServers> RemoteServers = new List<RemoteServers>();

        // The supported timeouts values.
        public Timeouts Timeouts = new Timeouts();

        // The supported drivers values.
        public List<Drivers> Drivers = new List<Drivers>();

        /// <summary>
        /// Constructor
        /// </summary>
        public Configuration()
        {
        }

        /// <summary>
        /// Gets the implicity timeout in TimeSpan.
        /// </summary>
        /// <returns>Implicit timeout</returns>
        public TimeSpan GetImplicitlyTimeout()
        {
            return TimeSpan.FromSeconds(Timeouts.implicitly);
        }

        /// <summary>
        /// Gets the explicity timeout in TimeSpan.
        /// </summary>
        /// <returns>Explicity timeout</returns>
        public TimeSpan GetExplicitlyTimeout()
        {
            return TimeSpan.FromSeconds(Timeouts.explicitly);
        }

        /// <summary>
        /// Gets the page load timeout in TimeSpan.
        /// </summary>
        /// <returns>Page load timeout</returns>
        public TimeSpan GetPageLoadTimeout()
        {
            return TimeSpan.FromSeconds(Timeouts.pageLoad);
        }

        /// <summary>
        /// Gets the script timeout in TimeSpan.
        /// </summary>
        /// <returns>Script timeout</returns>
        public TimeSpan GetScriptTimeout()
        {
            return TimeSpan.FromSeconds(Timeouts.script);
        }

        /// <summary>
        /// Gets the capabilities for the specific driver.
        /// </summary>
        /// <returns>List of capabilities</returns>
        /// <param name="browser">Browsers</param>
        public List<Capabilities> GetDriverCapabilities(Browsers browser)
        {
            return Drivers.Find(item => item.name.Equals(browser.ToString())).Capabilities;
        }


        public String GetRemoteServerByRole(Roles AuthRole)
        {
            String Url = "Default Url";

            if (AuthRole.Equals("Student"))
            {
                Url = "";
            }
            else if (AuthRole.Equals("Teacher"))
            {
                Url = "";
            }
            else if (AuthRole.Equals("Admin"))
            {
                Url = "";
            }
            return Url;
        }

    }
}
