using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using AppOperations;
using AppWeb;

namespace Utilities
{
    public static class FareHistoryPageFactory
    {
        public static IFairHistory Create(bool headless = false)
        {
            var options = new ChromeOptions();

            if (headless)
            {
                options.AddArgument("--headless");
                options.AddArgument("--disable-gpu");
                options.AddArgument("--window-size=1920,1080");
            }

            IWebDriver driver = new ChromeDriver(options);

            // Step 1: Navigate to login
            driver.Navigate().GoToUrl("http://localhost:8080/myshuttledev/");

            // Step 2: Login
            var loginPage = new LoginPage(driver);
            loginPage.EnterUsername("fred");
            loginPage.EnterPassword("fredpassword");
            loginPage.ClickLogin();

            // Step 3: From Dashboard → Go to Fare History
            var dashboardPage = new DashboardPage(driver);
            dashboardPage.ClickFareHistory();

            // Step 4: Return FareHistoryPage
            return new FareHistoryPage(driver);
        }
    }
}
