using GuiAutomationFramework.Framework.Enums;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;

namespace GuiAutomationFramework.Framework.Driver.Builder
{
    /// <summary>
    /// RemoteDriverBuilder builds <see cref="RemoteWebDriver"/> with the required capabilities for the selected browser.
    /// </summary>
    internal class RemoteDriverBuilder : IDriverBuilder
    {
        private DesiredCapabilities desiredCapabilities;

        private Roles role;

        private Browsers browser;

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="role">the <see cref="Roles"/>, refers to the specific remote server for it</param>
        public RemoteDriverBuilder()
        {
            this.role = role;
        }

        /// <summary>
        /// <see cref="IDriverBuilder.SetCapabilities(Browsers)"/>
        /// </summary>
        public IDriverBuilder SetCapabilities(Browsers browser)
        {
            this.browser = browser;
            if (desiredCapabilities == null)
            {
                desiredCapabilities = GetDefaultCapabilities(browser);
                
                var capabilitySet = Configuration.ConfigurationReader.FrameworkConfig.GetDriverCapabilities(browser);
                foreach (var capability in capabilitySet)
                {
                    desiredCapabilities.SetCapability(capability.Name, capability.Value);
                }
                
                /*check the error*/
                //desiredCapabilities.IsJavaScriptEnabled = true;
            }
            return this;
        }

        /// <summary>
        /// <see cref="IDriverBuilder.Build"/>.
        /// </summary>
        public IWebDriver Build()
        {
            var remoteAddress = new Uri(Configuration.ConfigurationReader.FrameworkConfig.GetRemoteServerByRole(role));
            var proxyAddress = "";
            if (Browsers.Chrome.Equals(browser))
            {
                proxyAddress = Configuration.ConfigurationReader.FrameworkConfig.GetRemoteServerByRole(role);
            }
            if (Browsers.IExplorer.Equals(browser))
            {
                proxyAddress = Configuration.ConfigurationReader.FrameworkConfig.GetRemoteServerByRole(role);
            }
            if (proxyAddress != null && proxyAddress.Length > 0)
            {
                OpenQA.Selenium.Proxy proxy = new OpenQA.Selenium.Proxy();
                proxy.HttpProxy = proxyAddress;
                proxy.FtpProxy = proxyAddress;
                proxy.SslProxy = proxyAddress;
                desiredCapabilities.SetCapability("proxy", proxy);
            }
            return new RemoteWebDriver(remoteAddress, desiredCapabilities);
        }

        /// <summary>
        /// Gets the default capabilities for the current browsers.
        /// </summary>
        /// <param name="browser">the <see cref="Browsers"/></param>
        /// <returns>the <see cref="DesiredCapabilities"/></returns>
        private DesiredCapabilities GetDefaultCapabilities(Browsers browser)
        {
            DesiredCapabilities desiredCapabilitites = null;
            switch (browser)
            {
                case Browsers.Chrome:
                    desiredCapabilitites = DesiredCapabilities.Chrome();
                    break;
                case Browsers.IExplorer:
                    desiredCapabilitites = DesiredCapabilities.InternetExplorer();
                    break;
                case Browsers.Firefox:
                    desiredCapabilitites = DesiredCapabilities.Firefox();
                    break;
            }
            return desiredCapabilitites;
        }
    }
}
