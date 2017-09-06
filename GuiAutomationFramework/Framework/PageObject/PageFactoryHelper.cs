using GuiAutomationFramework.Framework.Driver;
using GuiAutomationFramework.Framework.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuiAutomationFramework.Framework.PageObject
{
    /// <summary>
    /// PageFactoryHelper supports the creation of a particular PageObject.
    /// 
    /// PageFactoryHelper is integrated with <<see cref="DriverManager"/> in order to get the <<see cref="IWebDriver"/> instance
    /// for the PageObjects to be created.
    /// 
    /// </summary>
    public class PageFactoryHelper
    {
        /// <summary>
        /// Gets an instance of a particular PageObject.
        /// </summary>
        /// <typeparam name="T">The PO class to be created by reflection</typeparam>
        /// <returns>the PageObject</returns>
        public static T GetPage<T>() where T : BasePage
        {
            return (T)Activator.CreateInstance(typeof(T), DriverManager.PopulateDriver());
        }
    }
}
