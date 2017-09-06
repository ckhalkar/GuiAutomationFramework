using GuiAutomationFramework.Framework.Waits;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace GuiAutomationFramework.Framework.PageObject
{
    /// <summary>
    /// BasePage contains all the common functionality wrappers for Selenium operations.
    /// </summary>
    public abstract partial class BasePage
    {
        //The Driver instance.
        protected IWebDriver BaseDriver;

        /// <summary>
        /// Default constructor.
        /// Use PageFactory to initialize web elements.
        /// </summary>
        /// <param name="Driver">the <see cref="IWebDriver"/></param>
        public BasePage(IWebDriver Driver)
        {
            BaseDriver = Driver;
            PageFactory.InitElements(BaseDriver, this);
            WaitsHandler.WaitForAjaxToComplete(BaseDriver);
        }

    }
}
