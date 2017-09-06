using GuiAutomationFramework.Framework.Driver;
using GuiAutomationFramework.Framework.Enums;
using GuiAutomationFramework.Framework.Environment;
using GuiAutomationFramework.Framework.Log;
using GuiAutomationFramework.Framework.PageObject;
using log4net;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GuiAutomationFramework.Framework.TestSuite
{
        /// <summary>
        /// BaseTestSuite class is used by every TestSuite. 
        /// It has basic common functions needed by each Suite , such as Setup and TearDown.
        /// </summary>
        public abstract partial class BaseTestSuite
        {

            [OneTimeSetUp]
            public void BeforeSuite()
            {
                OnBeforeSuite();
            }

            [OneTimeTearDown]
            public void AfterSuite()
            {
                OnAfterSuite();
            }

            [SetUp]
            public void BeforeTest()
            {
                OnBeforeTest();
            }

            [TearDown]
            public void AfterTest()
            {
                if (TestContext.CurrentContext.Result.Outcome.Equals(ResultState.Failure))
                {
                    Console.WriteLine("Test Status : FAILED");
                }
                else if (TestContext.CurrentContext.Result.Outcome.Equals(ResultState.Success))
                {
                    Console.WriteLine("Test Status : PASSED");
                }
                OnAfterTest();
                if (TestContext.CurrentContext.Result.Outcome.Equals(ResultState.Failure))
                {
                    Console.WriteLine("Fail");
                }
                else if (TestContext.CurrentContext.Result.Outcome.Equals(ResultState.Success))
                {
                    Console.WriteLine("PASS");
                }
                LogHandler.Info("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<  END OF SCENARIO   >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>\n\n");
                LogManager.Shutdown();

            }

            protected virtual void OnBeforeSuite()
            {
            }

            protected virtual void OnAfterSuite()
            {
            }

            protected virtual void OnBeforeTest()
            {
            }

            protected virtual void OnAfterTest()
            {

            }

            /// <summary>
            /// Gets an instance of a particular PageObject of IHD application.
            /// </summary>
            /// <typeparam name="T">The PO class to be created by reflection</typeparam>
            /// <param name="role">The agent role <see cref="Roles"/></param>
            /// <returns>the PageObject</returns>
            protected T GetPage<T>() where T : BasePage
            {
                return PageFactoryHelper.GetPage<T>();
            }

            /// <summary>
            /// Init logs based on Logger configuration.
            /// </summary>
            /// <param name="role">the role</param>
            /// <param name="parameters">test parameters</param>
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            protected void InitLogs(Roles role, List<string> parameters)
            {
                String DataSet = "";
                if (EnvironmentReader.Logger)
                {
                    var callingMethod = new System.Diagnostics.StackTrace(1, false).GetFrame(0).GetMethod().Name;
                    var callingClassName = new System.Diagnostics.StackTrace(1, false).GetFrame(0).GetMethod().DeclaringType.Name;
                    foreach (string item in parameters)
                    {
                        DataSet = DataSet + item + ",";
                    }
                    LogHandler.setLogs(callingMethod, callingClassName, DataSet, role, EnvironmentReader.Logger);
                }
                DataSet = "";
            }

            /// <summary>
            /// Highlights an element.
            /// </summary>
            /// <param name="element">the element to be highlighted</param>
            protected void Highlight(IWebElement element)
            {
                // IsElementDisplayed(element, "", "");
                var jsDriver = (IJavaScriptExecutor)DriverManager.GetDriver();
                string highlightJavascript = @"arguments[0].style.cssText = ""border-width: 4px; border-style: solid; border-color: red"";";
                jsDriver.ExecuteScript(highlightJavascript, new object[] { element });
            }





            /// <summary>
            /// GenerateRandomName. Helpful for Creating new Student / Teacher ( Roles )
            /// </summary>
            /// <param name="size">the size for the random name</param>
            /// <returns></returns>
            protected string GenerateRandomName(int size)
            {
                StringBuilder builder = new StringBuilder();
                Random random = new Random();
                char ch;
                for (int i = 0; i < size; i++)
                {
                    ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                    builder.Append(ch);
                }
                return builder.ToString();
            }

            /// <summary>
            /// To generate random 7 digit number
            /// </summary>
            /// <returns>phone number</returns>
            protected string GenerateContactNumber()
            {
                string number = null;
                Random random = new Random();
                for (int i = 1; i < 11; i++)
                {
                    number += random.Next(1, 9).ToString();
                }
                return number;
            }

            /// <summary>
            /// Generate a random string to be used to update text  fields
            /// </summary>
            /// <param name="size">Number of elements in the string</param>
            /// <returns>Random string</returns>
            protected string GetRandomStrings(int size, bool numbersOnly = false)
            {
                var builder = new StringBuilder();
                if (size > 0)
                {
                    var random = new Random(DateTime.Now.Millisecond);
                    char ch;
                    if (numbersOnly)
                        ch = Convert.ToChar(random.Next(8) + 49);
                    else
                        ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                    builder.Append(ch);
                    for (int i = 1; i < size; i++)
                    {
                        if (numbersOnly)
                            ch = Convert.ToChar(random.Next(9) + 48);
                        else
                            ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                        builder.Append(ch);
                    }
                }
                return builder.ToString();
            }

            /// <summary>
            /// This method verifies if the page is opened or not with the specified title.
            /// </summary>
            /// <param name="title">Page Title That to be Verify</param>
            /// <returns></returns>
            public bool IsPageOpened(string title)
            {
                IWebDriver currentWindowHandle = null;
                try
                {
                    ReadOnlyCollection<string> windowHandles = DriverManager.GetDriver().WindowHandles;
                    foreach (string handle in windowHandles)
                    {
                        currentWindowHandle = DriverManager.GetDriver().SwitchTo().Window(handle);
                        if (currentWindowHandle.Url.ToLower().Contains(title.ToLower()) || currentWindowHandle.Title.ToLower().Contains(title.ToLower()))
                        {
                            return true;
                        }
                    }
                }
                catch (Exception e)
                {
                    LogHandler.Error("IsPageOpened::NoSuchElementException - " + e.Message);
                    throw new NoSuchElementException("IsPageOpened::" + e.Message);
                }
                return false;
            }

            /// <summary>
            /// Message info constructor to add parameters information in the assert message
            /// </summary>
            /// <param name="message"></param>
            /// <param name="parameters"></param>
            /// <returns></returns>
            public string MessageInfo(string message, List<string> parameters)
            {
                return String.Format(message + ", Parameters: {0} ", String.Join(",", parameters.ToArray()));
            }
        }    
}
