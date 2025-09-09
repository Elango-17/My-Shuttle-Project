/***************************************************************************************
* Project   : MyShuttle Application
* File Name : PageFactories.cs
* Namespace : Utilities
* 
* Description:
* This file contains static factory classes for creating instances of page objects
* in the MyShuttle application using Selenium WebDriver. It includes:
* - LogiPageFactory : LoginPage
* - DashboardPageFactory : DashboardPage
* - FareHistoryPageFactory : FareHistoryPage
* Uses appsettings.json for BaseUrl and Browser. Provides optional headless mode and dynamic
* credentials for login automation.
* 
* Author(s) :
* - Elangovan
* - Gayathri
* - Teja
* 
* License  : MIT License
* Copyright (c) 2025 MyShuttle Team (Elangovan, Gayathri, Teja)
***************************************************************************************/

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using AppOperations;
using AppWeb;
using System;
using Microsoft.Extensions.Configuration;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace Utilities
{
    /// <summary>
    /// Central driver manager for creating WebDriver instances.
    /// </summary>
    public static class DriverManager
    {
        private static readonly IConfiguration _config;

        static DriverManager()
        {
            _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        /// <summary>
        /// Creates a WebDriver instance based on appsettings.json settings for browser and BaseUrl.
        /// </summary>
        /// <param name="headless">Run in headless mode if true.</param>
        /// <returns>IWebDriver instance.</returns>
        public static IWebDriver CreateDriver(bool headless = false)
        {
            string browser = _config["Browser"]?.ToLower();
            IWebDriver driver;

            switch (browser)
            {
                case "chrome":
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.Latest);
                    var chromeOptions = new ChromeOptions();
                    if (headless)
                    {
                        chromeOptions.AddArgument("--headless");
                        chromeOptions.AddArgument("--disable-gpu");
                        chromeOptions.AddArgument("--window-size=1920,1080");
                    }
                    driver = new ChromeDriver(chromeOptions);
                    break;

                case "firefox":
                    new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig(), VersionResolveStrategy.MatchingBrowser);
                    var firefoxOptions = new FirefoxOptions();
                    if (headless)
                        firefoxOptions.AddArgument("--headless");
                    driver = new FirefoxDriver(firefoxOptions);
                    break;

                case "edge":
                    new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig(), VersionResolveStrategy.Latest);
                    var edgeOptions = new EdgeOptions();
                    if (headless)
                        edgeOptions.AddArgument("headless");
                    driver = new EdgeDriver(edgeOptions);
                    break;

                default:
                    throw new NotSupportedException(
                        $"Browser '{browser}' is not supported. Use Chrome, Firefox, or Edge.");
            }

            string baseUrl = _config["BaseUrl"];
            if (string.IsNullOrEmpty(baseUrl))
            {
                throw new Exception("BaseUrl is not set in appsettings.json");
            }

            driver.Navigate().GoToUrl(baseUrl);
            return driver;
        }
    }

    // ================================================================
    // Page Factories
    // ================================================================

    public static class LogiPageFactory
    {
        public static ILoginPage Create(bool headless = false)
        {
            IWebDriver driver = DriverManager.CreateDriver(headless);
            return new LoginPage(driver);
        }

        internal static void PerformLogin(IWebDriver driver, string username = "fred", string password = "fredpassword")
        {
            var loginPage = new LoginPage(driver);
            loginPage.EnterUsername(username);
            loginPage.EnterPassword(password);
            loginPage.ClickLogin();
        }
    }

    public static class DashboardPageFactory
    {
        public static IDashboard Create(bool headless = false, string username = "fred", string password = "fredpassword")
        {
            IWebDriver driver = DriverManager.CreateDriver(headless);
            LogiPageFactory.PerformLogin(driver, username, password);
            return new DashboardPage(driver);
        }
    }

    public static class FareHistoryPageFactory
    {
        public static IFairHistory Create(bool headless = false, string username = "fred", string password = "fredpassword")
        {
            IWebDriver driver = DriverManager.CreateDriver(headless);
            LogiPageFactory.PerformLogin(driver, username, password);
            var dashboardPage = new DashboardPage(driver);
            dashboardPage.ClickFareHistory();
            return new FareHistoryPage(driver);
        }
    }
}
