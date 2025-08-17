using AppOperations;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AppWeb
{
    public class LoginPage : ILoginPage
    {
        private readonly IWebDriver _driver;

        // Updated locators
        private readonly By logoLocator = By.CssSelector("img[src*='logologin.png']");
        private readonly By loginFormLocator = By.CssSelector("form[action='login'][method='post']");
        private readonly By usernameInput = By.Id("email");  // Changed from "username" to "email"
        private readonly By passwordInput = By.Id("password");
        private readonly By loginButton = By.CssSelector("input[type='submit'][value='Log in']");
        private readonly By errorMessageLocator = By.ClassName("error-message"); // Keep or update if error markup changes

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public bool IsLogoVisible()
        {
            return _driver.FindElement(logoLocator).Displayed;
        }

        public bool IsLoginFormVisible()
        {
            return _driver.FindElement(loginFormLocator).Displayed;
        }

        public void EnterUsername(string username)
        {
            var usernameField = _driver.FindElement(usernameInput);
            usernameField.Clear();
            usernameField.SendKeys(username);
        }

        public void EnterPassword(string password)
        {
            var passwordField = _driver.FindElement(passwordInput);
            passwordField.Clear();
            passwordField.SendKeys(password);
        }

        public void ClickLogin()
        {
            _driver.FindElement(loginButton).Click();
        }

        public string GetErrorMessage()
        {
            try
            {
                // Wait for either h2 or p in the jumbotron
                var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
                var errorHeader = wait.Until(d => d.FindElement(By.CssSelector(".jumbotron h2")));
                var errorParagraph = _driver.FindElement(By.CssSelector(".jumbotron p"));

                return $"{errorHeader.Text} {errorParagraph.Text}".Trim();
            }
            catch (NoSuchElementException)
            {
                return string.Empty;
            }
            catch (WebDriverTimeoutException)
            {
                return string.Empty;
            }
        }


        public bool IsLoginButtonEnabled()
        {
            return _driver.FindElement(loginButton).Enabled;
        }

        public bool IsLoginSuccessful()
        {
            return !_driver.Url.Contains("login");
        }
    }
}