using Edgenuity.Educator.PageObject;
using NUnit.Framework;
using GuiAutomationFramework.Framework.TestSuite;

namespace Edgenuity.Educator.TestSuite
{


    [TestFixture]
    [Category("Sanity")]

    public class LoginSuite : BaseTestSuite
    {
        [Test]
        [TestCase("ChetanAdmin", "chetan")]
        [TestCase("","")]
        public void LoginTest(string username,string password)
        {
            LoginPage objlogin = GetPage<LoginPage>();
            WelcomePage objWelcomePage = objlogin.Login(username, password);
            IsElementDisplayed(objWelcomePage.lblWelcome, "Whats Broken", "Welcome Page");
        }

    }
}
