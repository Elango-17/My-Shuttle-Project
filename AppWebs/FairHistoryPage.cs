using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using AppOperations;

namespace AppWeb
{
    public class FareHistoryPage : IFairHistory
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public FareHistoryPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(50));
        }

        // ===== Core Methods =====

        public string GetPageHeading() =>
            _wait.Until(d => d.FindElement(By.CssSelector("h1, h2, .page-title"))).Text;

        public string GetLoggedInUserName() =>
            _wait.Until(d => d.FindElement(By.CssSelector(".user-info, #loggedInUser"))).Text;

        public bool IsFareTableVisible() =>
            _wait.Until(d => d.FindElement(By.Id("fareTable"))).Displayed;

        public int GetNumberOfFareRecords() =>
            _driver.FindElements(By.CssSelector("#fareTable tbody tr")).Count;

        public string GetFareDetailsByRow(int rowIndex)
        {
            var rows = _driver.FindElements(By.CssSelector("#fareTable tbody tr"));
            return rows[rowIndex].Text;
        }

        public void SortFareHistoryBy(string columnName)
        {
            var headers = _driver.FindElements(By.CssSelector("#fareTable thead th"));
            var header = headers.FirstOrDefault(h => h.Text.Trim().Equals(columnName, StringComparison.OrdinalIgnoreCase));
            header?.Click();
        }

        public void ClickBackToDashboard() =>
            _driver.FindElement(By.Id("backToDashboard")).Click();

        public bool IsInternalUseOnlyLabelVisible() =>
            _driver.FindElement(By.CssSelector(".internal-use-label")).Displayed;

        public IList<string> GetAllColumnHeaders() =>
            _driver.FindElements(By.CssSelector("#fareTable thead th")).Select(e => e.Text.Trim()).ToList();

        public bool HasColumn(string columnName) =>
            GetAllColumnHeaders().Any(c => c.Equals(columnName, StringComparison.OrdinalIgnoreCase));

        // ===== Navigation & Session =====

        public void NavigateTo()
        {
            _driver.Navigate().GoToUrl("http://localhost:8080/myshuttledev/fareHistory");
            _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("fareTable")));
        }

        public void ExpireSession()
        {
            _driver.Manage().Cookies.DeleteAllCookies();
            _driver.Navigate().Refresh();
        }

        public bool IsLoginPageVisible() =>
            _driver.Url.Contains("login", StringComparison.OrdinalIgnoreCase);

        public int GetFareRowCount() =>
            _driver.FindElements(By.CssSelector("#fareTable tbody tr")).Count;

        public int GetMockFareRecordCount() => 10; // Mocked for test usage

        // ===== Data Extraction =====

        public IEnumerable<string> GetAllFareDetails() =>
            _driver.FindElements(By.CssSelector("#fareTable tbody tr")).Select(r => r.Text);

        public IEnumerable<decimal> SortByFare()
        {
            var fares = GetFareAmounts().OrderBy(f => f).ToList();
            return fares;
        }

        public IEnumerable<DateTime> SortByDate()
        {
            var rows = _driver.FindElements(By.CssSelector("#fareTable tbody tr"));
            var dates = rows.Select(r =>
            {
                var cell = r.FindElement(By.CssSelector("td.date"));
                return DateTime.Parse(cell.Text, CultureInfo.InvariantCulture);
            }).OrderBy(d => d).ToList();

            return dates;
        }

        public IEnumerable<string> SortByDriver()
        {
            var rows = _driver.FindElements(By.CssSelector("#fareTable tbody tr"));
            return rows.Select(r => r.FindElement(By.CssSelector("td.driver")).Text)
                       .OrderBy(n => n)
                       .ToList();
        }

        public bool IsDashboardVisible() =>
            _driver.Url.Contains("dashboard", StringComparison.OrdinalIgnoreCase);

        public IEnumerable<decimal> GetFareAmounts()
        {
            var rows = _driver.FindElements(By.CssSelector("#fareTable tbody tr"));
            return rows.Select(r =>
            {
                var text = r.FindElement(By.CssSelector("td.fare")).Text.Replace("$", "").Trim();
                return decimal.Parse(text, CultureInfo.InvariantCulture);
            });
        }

        public IEnumerable<int> GetRatings()
        {
            var rows = _driver.FindElements(By.CssSelector("#fareTable tbody tr"));
            return rows.Select(r => int.Parse(r.FindElement(By.CssSelector("td.rating")).Text.Trim()));
        }

        public IEnumerable<(DateTime Pickup, DateTime Dropoff)> GetTripDates()
        {
            var rows = _driver.FindElements(By.CssSelector("#fareTable tbody tr"));
            return rows.Select(r =>
            {
                var pickup = DateTime.Parse(r.FindElement(By.CssSelector("td.pickup")).Text, CultureInfo.InvariantCulture);
                var dropoff = DateTime.Parse(r.FindElement(By.CssSelector("td.dropoff")).Text, CultureInfo.InvariantCulture);
                return (pickup, dropoff);
            });
        }

        public void ClearFareRecords()
        {
            var clearButton = _driver.FindElement(By.Id("clearFares"));
            clearButton.Click();
            _wait.Until(d => d.FindElement(By.CssSelector("#fareTable tbody tr")).Text.Contains("No records"));
        }

        public string GetNoRecordMessage() =>
            _driver.FindElement(By.CssSelector("#fareTable tbody tr td")).Text;

        public void GenerateLargeDataset(int count)
        {
            for (int i = 0; i < count; i++)
            {
                _driver.FindElement(By.Id("generateFare")).Click();
            }
        }

        public void ClickFareHistory() =>
            _driver.FindElement(By.Id("fareHistoryLink")).Click();
    }
}
