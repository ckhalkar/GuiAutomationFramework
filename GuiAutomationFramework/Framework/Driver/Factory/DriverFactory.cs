using GuiAutomationFramework.Framework.Configuration;
using GuiAutomationFramework.Framework.Driver.Builder;
using GuiAutomationFramework.Framework.Enums;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuiAutomationFramework.Framework.Driver.Factory
{
    /// <summary>
    /// DriverFactory creates a new instance of a particular web driver.
    /// 
    /// DriverFactory delegates the creation itself to the specific builder.
    /// </summary>
    internal sealed class DriverFactory
    {

        /// <summary>
        /// Returns a new instance of [see cref="IWebDriver"]/>.
        /// </summary>
        /// <param name="browser">the <see cref="Browsers"/></param>
        /// <param name="remote">the boolean value to determine if the driver is remote or not</param>
        /// <param name="role">the <see cref="Roles"/></param>
        /// <returns></returns>
        public static IWebDriver NewInstance(Browsers browser, bool remote)
        {
            IWebDriver driver = null;
            if (remote)
            {
                driver = NewRemoteInstance(browser);
            }
            else
            {
                driver = NewLocalInstance(browser);
            }

            driver.Manage().Timeouts().ImplicitlyWait(ConfigurationReader.FrameworkConfig.GetImplicitlyTimeout());
            driver.Manage().Timeouts().SetScriptTimeout(ConfigurationReader.FrameworkConfig.GetScriptTimeout());
            driver.Manage().Timeouts().SetPageLoadTimeout(ConfigurationReader.FrameworkConfig.GetPageLoadTimeout());
            driver.Manage().Window.Maximize();

            return driver;
        }

        /// <summary>
        /// Returns a new local instance of <see cref="IWebDriver"/>.
        /// </summary>
        /// <param name="browser">the <see cref="Browsers"/></param>
        /// <param name="role">the <see cref="Roles"/></param>
        /// <returns>the <see cref="IWebDriver"/></returns>
        private static IWebDriver NewLocalInstance(Browsers browser)
        {
            IWebDriver driver = null;
            switch (browser)
            {
                case Browsers.Chrome:
                    driver = new ChromeDriverBuilder().Build();
                    break;
                case Browsers.IExplorer:
                    driver = new InternetExplorerDriverBuilder().Build();
                    break;
                case Browsers.Firefox:
                    break;
            }
            return driver;
        }

        /// <summary>
        /// Returns a new instance of <see cref="OpenQA.Selenium.Remote.RemoteWebDriver"/>
        /// </summary>
        /// <param name="browser">the <see cref="Browsers"/></param>
        /// <param name="role">the <see cref="Roles"/></param>
        /// <returns>the <see cref="OpenQA.Selenium.Remote.RemoteWebDriver"/></returns>
        private static IWebDriver NewRemoteInstance(Browsers browser)
        {
            return new RemoteDriverBuilder().SetCapabilities(browser).Build();
        }
    }
}
