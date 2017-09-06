using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using GuiAutomationFramework.Framework.PageObject;


namespace Edgenuity.Educator.PageObject
{
    public class WelcomePage : BasePage
    {
        #region Locators

        [FindsBy(How = How.CssSelector, Using = "#ctl00_conBody_pnlMain > h1")]
        public IWebElement lblWelcome { get; private set; }


        #endregion
        /// <summary>
        /// Default Constructor for WelcomePage
        /// </summary>
        /// <param name="Driver"></param>
        public WelcomePage(IWebDriver Driver) : base(Driver)
        {
        }


        
    }
}
