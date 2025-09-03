using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using AppOperations;
using AppWeb;

namespace Utilities
{
    public static class FareHistoryPageFactory
    {
        /// <summary>
        /// Creates an instance of FareHistoryPage after performing login and navigation.
        /// </summary>
        /// <param name="headless">Run Chrome in headless mode if true.</param>
        /// <returns>IFairHistory page object</returns>
        public static IFairHistory Create(bool headless = false)
        {
            var options = new ChromeOptions();

            if (headless)
            {
                options.AddArgument("--headless=new"); // modern headless mode
                options.AddArgument("--disable-gpu");
                options.AddArgument("--window-size=1920,1080");
            }

            IWebDriver driver = new ChromeDriver(options);

            // Step 1: Navigate to application login
            driver.Navigate().GoToUrl("http://localhost:8080/myshuttledev/");

            // Step 2: Login
            var loginPage = new LoginPage(driver);
            loginPage.EnterUsername("fred");
            loginPage.EnterPassword("fredpassword");
            loginPage.ClickLogin();

            // Step 3: Navigate to Fare History from Dashboard
            var dashboardPage = new DashboardPage(driver);
            dashboardPage.ClickFareHistory();

            // Step 4: Return FareHistoryPage instance
            return new FareHistoryPage(driver);
        }
    }
}
