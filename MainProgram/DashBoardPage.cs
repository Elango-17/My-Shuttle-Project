using OpenQA.Selenium;
using AppOperations;

namespace AppWeb
{
    public class DashboardPage : IDashboard
    {
        private readonly IWebDriver _driver;

        // Constructor
        public DashboardPage(IWebDriver driver)
        {
            _driver = driver;
        }

        // Locators
        private By dashboardContainer => By.Id("dashboard-container"); // Example ID
        private By fareHistoryButton => By.Id("fare-history");         // Adjust as per actual locator
        private By signOutButton => By.Id("sign-out");                 // Adjust as per actual locator
        private By userLabel => By.Id("user-status");                  // Could be for login status
        private By internalUseLabel => By.XPath("//*[text()='Internal Use Only']");

        // Methods matching interface

        public bool IsDashboardVisible()
        {
            return _driver.FindElement(dashboardContainer).Displayed;
        }

        public void ClickFareHistory()
        {
            _driver.FindElement(fareHistoryButton).Click();
        }

        public void ClickSignOut()
        {
            _driver.FindElement(signOutButton).Click();
        }

        public bool IsUserLoggedIn()
        {
            try
            {
                return _driver.FindElement(userLabel).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public bool IsInternalUseOnlyLabelVisible()
        {
            try
            {
                return _driver.FindElement(internalUseLabel).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}
