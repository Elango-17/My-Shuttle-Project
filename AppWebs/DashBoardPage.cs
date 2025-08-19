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
        private By dashboardTitle => By.XPath("//h2[text()='Dashboard']");
        private By fareHistoryButton => By.LinkText("Access Your Fare History");
        private By signOutButton => By.XPath("//a[contains(@href,'logout.jsp')]");
        private By internalUseLabel => By.XPath("//h5[contains(text(),'Internal Use Only')]");


        public bool IsDashboardVisible()
        {
            return _driver.FindElement(dashboardTitle).Displayed;
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
                string currentUrl = _driver.Url.ToLower();
                // adjust according to your app's dashboard URL
                return currentUrl.Contains("dashboard") || currentUrl.Contains("login") || currentUrl.Contains("home.jsp");
            }
            catch (WebDriverException)
            {
                return false;
            }
        }

        //public bool IsUserLoggedIn()
        //{
        //    try
        //    {
        //        // If sign out button is present, user is logged in
        //        return _driver.FindElement(By.XPath("//a[contains(@href,'logout.jsp')]")).Displayed;
        //    }
        //    catch (NoSuchElementException)
        //    {
        //        return false;
        //    }
        //}


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
