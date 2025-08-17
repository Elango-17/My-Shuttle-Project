using OpenQA.Selenium;
using System;

namespace AppOperations
{
    public class DashboardPage : IDashboardPage
    {
        private readonly IWebDriver _driver;

        private readonly By dashboardHeader = By.CssSelector("h1.dashboard-title");
        private readonly By logo = By.CssSelector("img.site-logo");
        private readonly By internalMessage = By.CssSelector("div.welcome-msg");
        private readonly By fareHistoryLink = By.LinkText("Fare History");
        private readonly By signOutButton = By.Id("logout");

        public DashboardPage(IWebDriver driver)
        {
            _driver = driver ?? throw new ArgumentNullException(nameof(driver));
        }

        public bool IsDashboardHeaderVisible()
        {
            try
            {
                return _driver.FindElement(dashboardHeader).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public bool IsLogoVisible()
        {
            try
            {
                return _driver.FindElement(logo).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public string GetInternalMessage()
        {
            try
            {
                return _driver.FindElement(internalMessage).Text;
            }
            catch (NoSuchElementException)
            {
                return string.Empty;
            }
        }

        public void ClickFareHistory()
        {
            _driver.FindElement(fareHistoryLink).Click();
        }

        public void ClickSignOut()
        {
            _driver.FindElement(signOutButton).Click();
        }
    }
}
