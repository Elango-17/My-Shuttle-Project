using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using AppOperations;
using AppWeb;

namespace Utilities
{
    public static class DashboardPageFactory
    {
        public static IDashboard Create(bool headless = false)
        {
            var options = new ChromeOptions();

            if (headless)
            {
                options.AddArgument("--headless");
                options.AddArgument("--disable-gpu");
                options.AddArgument("--window-size=1920,1080");
            }

            IWebDriver driver = new ChromeDriver(options);

            // Go to login first
            driver.Navigate().GoToUrl("http://localhost:8080/myshuttledev/");

            // Do login before returning dashboard
            var loginPage = new LoginPage(driver);
            loginPage.EnterUsername("fred");
            loginPage.EnterPassword("fredpassword");
            loginPage.ClickLogin();

            return new DashboardPage(driver);
        }
    }
}