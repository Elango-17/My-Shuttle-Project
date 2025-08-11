using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using AppOperations;

namespace AppWeb
{
    public class FareHistoryPage : IFairHistory
    {
        private readonly IWebDriver driver;

        // Constructor
        public FareHistoryPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        // Locators (update based on your actual HTML)
        private By fareTable = By.Id("fareTable");
        private By fareRows = By.CssSelector("#fareTable tbody tr");
        private By dashboardButton = By.Id("dashboardBtn");
        private By internalUseOnlyLabel = By.XPath("//footer[contains(text(), 'Internal Use Only')]");
        private By searchInput = By.Id("searchInput");
        private By columnHeader(string columnName) => By.XPath($"//th[contains(text(), '{columnName}')]");

        // Methods implementing the IFairHistory interface

        public bool IsFareTableVisible()
        {
            return driver.FindElement(fareTable).Displayed;
        }

        public int GetNumberOfFareRecords()
        {
            return driver.FindElements(fareRows).Count;
        }

        public string GetFareDetailsByRow(int rowIndex)
        {
            var rows = driver.FindElements(fareRows);
            if (rowIndex < 0 || rowIndex >= rows.Count)
                throw new ArgumentOutOfRangeException("Row index is out of range");

            return rows[rowIndex].Text;
        }

        public void SortFareHistoryBy(string columnName)
        {
            driver.FindElement(columnHeader(columnName)).Click();
        }

        public void FilterFareHistory(string searchTerm)
        {
            var search = driver.FindElement(searchInput);
            search.Clear();
            search.SendKeys(searchTerm);
        }

        public void ClickBackToDashboard()
        {
            driver.FindElement(dashboardButton).Click();
        }

        public bool IsInternalUseOnlyLabelVisible()
        {
            return driver.FindElement(internalUseOnlyLabel).Displayed;
        }
    }
}
