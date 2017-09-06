using GuiAutomationFramework.Framework.Enums;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace GuiAutomationFramework.Framework.Driver.Builder
{
    /// <summary>
    /// ChromeDriverBuilder builds <see cref="ChromeDriver"/>.
    /// </summary>
    internal class ChromeDriverBuilder : IDriverBuilder
    {
        //The chrome options.
        private ChromeOptions chromeOptions;

        private Roles role;

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="role">the <see cref="Roles"/>, refers to the specific remote server for it</param>
        public ChromeDriverBuilder()
        {
            this.role = role;
        }

        /// <summary>
        /// <see cref="IDriverBuilder.SetCapabilities"/>.
        /// </summary>
        public IDriverBuilder SetCapabilities(Browsers browser)
        {
            chromeOptions = new ChromeOptions();
            var capabilitySet = Configuration.ConfigurationReader.FrameworkConfig.GetDriverCapabilities(browser);
            foreach (var capability in capabilitySet)
            {
                chromeOptions.AddUserProfilePreference(capability.Name, capability.Value);
            }
            return this;
        }

        /// <summary>
        /// <see cref="IDriverBuilder.Build"/>.
        /// </summary>
        public IWebDriver Build()
        {
            if (chromeOptions == null)
            {
                chromeOptions = new ChromeOptions();
            }
            return new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory + "\\Framework\\Browsers", chromeOptions);
        }
    }
}

