using OpenQA.Selenium;
using AppOperations;
using System.Reflection;

namespace Utilities
{
    public static class DriverFactory
    {
        public static IWebDriver? GetDriverFrom(ILoginPage loginPage)
        {
            // Reflection hack to grab private _driver from LoginPage
            var field = loginPage.GetType()
                .GetField("_driver", BindingFlags.NonPublic | BindingFlags.Instance);

            return field?.GetValue(loginPage) as IWebDriver;
        }

        public static void QuitDriver(ILoginPage loginPage)
        {
            var driver = GetDriverFrom(loginPage);
            driver?.Quit();
        }
    }
}
