using GuiAutomationFramework.Framework.Driver;
using GuiAutomationFramework.Framework.Log;
using GuiAutomationFramework.Framework.Waits;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuiAutomationFramework.Framework.PageObject
{
    /// <summary>
    /// BasePage.Navigation provides operations to navigate to a specific page or between the pages on a particular application.
    /// </summary>
    public abstract partial class BasePage
    {

        #region Browser

        /// <summary>
        /// Goes to a specific URL.
        /// </summary>
        /// <param name="Url">The expected URL</param>
        public void GoTo(String Url)
        {
            BaseDriver.Navigate().GoToUrl(Url);
        }

        /// <summary>
        /// Goes to a specific URL.
        /// </summary>
        /// <param name="Url">The expected URL</param>
        public void GoBack(String Url)
        {
            BaseDriver.Navigate().Back();
        }

        /// <summary>
        /// Goes to a specific URL.
        /// </summary>
        /// <param name="Url">The expected URL</param>
        public void GoForward(String Url)
        {
            BaseDriver.Navigate().Forward();
        }

        /// <summary>
        /// Closes the page.
        /// </summary>
        public void Close()
        {
            BaseDriver.Close();
            BaseDriver.Dispose();
        }

        #endregion

        #region Default Content + IFrame

        /// <summary>
        /// Moves to a particular iframe in the page.
        /// </summary>
        /// <param name="xpath">???</param>
        /// <returnsthe <see cref="IWebDriver"/>returns>
        public IWebDriver SwitchToDefaultContent()
        {
            return BaseDriver.SwitchTo().DefaultContent();
        }

        /// <summary>
        /// Moves to a particular iframe in the page.
        /// </summary>
        /// <param name="xpath">???</param>
        /// <returnsthe <see cref="IWebDriver"/>returns>
        public IWebDriver SwitchToIFrame()
        {
            return SwitchToDefaultContent().SwitchTo().Frame(0);
        }
        #endregion

        #region Tabs

        /// <summary>
        /// Switch to a different windows.
        /// </summary>
        /// <param name="title">the windows title</param>
        public IWebDriver SwitchToTab(string title)
        {
            IWebDriver currentWindowHandle = null;
            try
            {
                ReadOnlyCollection<string> windowHandles = BaseDriver.WindowHandles;
                foreach (string handle in windowHandles)
                {
                    currentWindowHandle = BaseDriver.SwitchTo().Window(handle);
                    if (currentWindowHandle.Url.ToLower().Contains(title.ToLower()) || currentWindowHandle.Title.ToLower().Contains(title.ToLower()))
                    {
                        return BaseDriver.SwitchTo().Window(handle);
                    }
                }
            }
            catch (Exception e)
            {
                LogHandler.Error("SwitchToTab::NoSuchElementException - " + e.Message);
                throw new NoSuchElementException("SwitchToTab::" + e.Message);
            }
            return BaseDriver;
        }

        /// <summary>
        /// Gets the Parent Window handle
        /// </summary>
        /// <param name="title">the windows title</param>
        public IWebDriver GetParentWindowHandle(string title)
        {
            IWebDriver currentWindowHandle = null;
            try
            {
                ReadOnlyCollection<string> windowHandles = BaseDriver.WindowHandles;
                foreach (string handle in windowHandles)
                {
                    currentWindowHandle = BaseDriver.SwitchTo().Window(handle);
                    if (currentWindowHandle.Url.ToLower().Contains(title.ToLower()) || currentWindowHandle.Title.ToLower().Contains(title.ToLower()))
                    {
                        return currentWindowHandle;
                    }
                }
            }
            catch (Exception e)
            {
                LogHandler.Error("SwitchToTab::NoSuchElementException - " + e.Message);
                throw new NoSuchElementException("SwitchToTab::" + e.Message);
            }
            return BaseDriver;
        }

        /// <summary>
        /// Get New Window Handle
        /// </summary>
        /// <returns>Returns new window handle</returns>
        public string GetNewWindowTitle()
        {
            IWebDriver newtWindowHandle = null;
            string oldtWindowHandle = BaseDriver.CurrentWindowHandle;
            try
            {
                ReadOnlyCollection<string> windowHandles = BaseDriver.WindowHandles;
                if (windowHandles.Count > 1)
                {
                    foreach (string handle in windowHandles)
                    {
                        if (handle != oldtWindowHandle)
                        {
                            newtWindowHandle = BaseDriver.SwitchTo().Window(handle);
                        }
                    }
                }
                else
                {
                    newtWindowHandle = BaseDriver.SwitchTo().Window(oldtWindowHandle);
                }
            }
            catch (Exception e)
            {
                LogHandler.Error("SwitchToTab::NoSuchElementException - " + e.Message);
                throw new NoSuchElementException("SwitchToTab::" + e.Message);
            }
            return newtWindowHandle.Title;
        }

        /// <summary>
        /// Close tab by name (title).
        /// </summary>
        /// <param name="title">the title tab</param>
        public void CloseTab(string title)
        {
            SwitchToTab(title).Close();
        }

        /// <summary>
        /// Check if Tab is present 
        /// </summary>
        /// <param name="tabtitle">BTN number</param>
        /// <returns>true=if ihdcase tab is present; false=if it is not present</returns>
        public bool IsTabPresent(string tabtitle)
        {
            try
            {
                string path = "//*[@id='tabsContainer']/li";
                IList<IWebElement> tablist = DriverManager.GetDriver().FindElements(By.XPath(path));
                IWebElement customer360Tab = tablist.FirstOrDefault(t => t.Text.Contains(tabtitle));
                customer360Tab.FindElement(By.TagName("i"));
                return true;
            }
            catch (Exception)
            {
                //logger
            }
            return false;
        }

        /// <summary>
        /// Check if Tab is present 
        /// </summary>
        /// <param name="tabtitle">BTN number</param>
        /// <returns>true=if ihdcase tab is present; false=if it is not present</returns>
        public bool IsWindowPresent(string windowtitle)
        {
            WaitsHandler.WaitForAjaxToComplete(BaseDriver);
            IWebDriver currentWindowHandle = null;
            try
            {
                ReadOnlyCollection<string> windowHandles = BaseDriver.WindowHandles;
                foreach (string handle in windowHandles)
                {
                    currentWindowHandle = BaseDriver.SwitchTo().Window(handle);
                    if (currentWindowHandle.Url.ToLower().Contains(windowtitle.ToLower()) || currentWindowHandle.Title.ToLower().Contains(windowtitle.ToLower()))
                    {
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                //logger
            }
            return false;
        }

        /// <summary>
        /// Close the active tab
        /// </summary>
        public void CloseActiveTab()
        {

            try
            {
                string path = "//*[@id='tabsContainer']/li[@class='active']/span";
                DriverManager.GetDriver().FindElement(By.XPath(path)).Click();
                WaitsHandler.WaitForAjaxToComplete(BaseDriver);
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region JS Alert

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IAlert SwitchToAlert()
        {
            try
            {
                WaitsHandler.WaitForAlert(BaseDriver, "");
                return BaseDriver.SwitchTo().Alert();
            }
            catch (Exception e)
            {
                LogHandler.Error("SwitchToAlert::Exception - " + e.Message);
                throw new NoSuchElementException("SwitchToAlert::" + e.Message);
            }

        }
        /// <summary>
        /// Accept Alert message
        /// </summary>
        /// <returns></returns>
        public void AcceptAlert()
        {
            SwitchToAlert().Accept();
            WaitsHandler.WaitForAjaxToComplete(BaseDriver);
        }

        #endregion

    }
}
