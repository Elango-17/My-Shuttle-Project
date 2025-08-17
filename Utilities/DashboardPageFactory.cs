using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using AppOperations;
using AppWeb;

namespace Utilities
{
    public static class DashboardPageFactory
    {
        public static IDashboardPage Create(IWebDriver driver)
        {
            // Instead of launching a new browser, reuse the same driver
            // passed from LoginPageFactory after login.
            return new DashboardPage(driver);
        }
    }
}
