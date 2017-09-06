using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using GuiAutomationFramework.Framework.PageObject;


namespace Edgenuity.Educator.PageObject
{
    public class LoginPage : BasePage
    {
        #region WebElements
        [FindsBy(How = How.Id, Using = "LoginUsername")]
        public IWebElement TxtUserName{ get; private set; }

        [FindsBy(How = How.Id, Using = "LoginPassword")]
        public IWebElement TxtPassword{ get; private set; }

        [FindsBy(How = How.Id, Using = "LoginSubmit")]
        public IWebElement BtnLogin { get; private set; }

        #endregion

        /// <summary>
        /// Default constructor for LoginPage
        /// </summary>
        /// <param name="Driver"></param>
        public LoginPage(IWebDriver Driver) : base(Driver)
        {

        }

        
        /// <summary>
        /// Click on Login Button
        /// </summary>
        /// <returns></returns>
        public LoginPage ClickLogin()
        {
            BtnLogin.Click();
            return this;
        }


        public void EnterUserName(string Username)
        {
            TypeText(TxtUserName, Username);
        }

        public void EnterPassword(string password)
        {
            TypeText(TxtUserName, password);
        }


        public WelcomePage Login(string username , string password)
        {
            EnterUserName(username);
            EnterPassword(password);
            ClickLogin();
            return new WelcomePage(BaseDriver);            
        }
    }
}
