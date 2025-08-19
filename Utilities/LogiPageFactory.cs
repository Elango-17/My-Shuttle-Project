using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using AppOperations;
using AppWeb;

namespace Utilities
{
    public static class LogiPageFactory
    {
        public static ILoginPage Create(bool headless = false)
        {
            var options = new ChromeOptions();

            if (headless)
            {
                options.AddArgument("--headless");
                options.AddArgument("--disable-gpu"); // Recommended for Windows OS headless
                options.AddArgument("--window-size=1920,1080"); // Optional: define window size
            }

            IWebDriver driver = new ChromeDriver(options);
            driver.Navigate().GoToUrl("http://localhost:8080/myshuttledev/");
            return new LoginPage(driver);
        }
    }
}
