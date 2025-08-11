using OpenQA.Selenium;
using AppOperations;

namespace AppWeb
{
    public class LoginPage : ILoginPage
    {
        private readonly IWebDriver _driver;

        // Locators
        private readonly By logoLocator = By.Id("appLogo"); // Adjust based on actual ID
        private readonly By loginFormLocator = By.Id("loginForm"); // Adjust based on actual form container
        private readonly By usernameInput = By.Id("username");
        private readonly By passwordInput = By.Id("password");
        private readonly By loginButton = By.Id("loginButton");
        private readonly By errorMessageLocator = By.ClassName("error-message"); // Adjust based on your actual class

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
                return _driver.FindElement(errorMessageLocator).Text;
            }
            catch (NoSuchElementException)
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
            // Example check: replace with actual logic like checking redirection or session
            return !_driver.Url.Contains("login");
        }
    }
}
