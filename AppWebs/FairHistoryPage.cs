using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
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

        // Locators
        private By headingLocator = By.CssSelector("h2");
        private By fareTable = By.CssSelector("table.table.table-striped.table-condensed");
        private By fareRows = By.CssSelector("table.table.table-striped.table-condensed tbody tr");
        private By dashboardButton = By.CssSelector("a[href*='home.jsp']");
        private By internalUseOnlyLabel = By.XPath("//h5[contains(text(),'Internal Use Only')]");
        private By allHeaders = By.CssSelector("table.table.table-striped.table-condensed tr.info th");
        private By userNameLocator;

        //private By columnHeader(string columnName) => By.XPath($"//table[contains(@class,'table')]//th[normalize-space()='{columnName}']");

        // Methods implementing the IFairHistory interface

        // Get the <h2> heading text
        public string GetPageHeading()
        {
            return driver.FindElement(headingLocator).Text.Trim();
        }

        public string GetLoggedInUserName()
        {
            return driver.FindElement(userNameLocator).Text.Trim();
        }

        public bool IsFareTableVisible()
        {
            try
            {
                return driver.FindElement(fareTable).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public int GetNumberOfFareRecords()
        {
            return driver.FindElements(fareRows).Count;
        }

        public string GetFareDetailsByRow(int rowIndex)
        {
            var rows = driver.FindElements(fareRows);
            if (rowIndex < 0 || rowIndex >= rows.Count)
                throw new ArgumentOutOfRangeException(nameof(rowIndex), "Row index is out of range");

            return rows[rowIndex].Text.Trim();
        }

        public void SortFareHistoryBy(string columnName)
        {
            var header = driver.FindElements(allHeaders)
                               .FirstOrDefault(h => h.Text.Trim().Equals(columnName, StringComparison.OrdinalIgnoreCase));

            if (header == null)
                throw new ArgumentException($"Column '{columnName}' not found in fare history table.");

            header.Click();
        }

        public void ClickBackToDashboard()
        {
            driver.FindElement(dashboardButton).Click();
        }

        public bool IsInternalUseOnlyLabelVisible()
        {
            try
            {
                return driver.FindElement(internalUseOnlyLabel).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }


        public IList<string> GetAllColumnHeaders()
        {
            return driver.FindElements(allHeaders)
                         .Select(th => th.Text.Trim())
                         .Where(text => !string.IsNullOrEmpty(text))
                         .ToList();
        }


        public bool HasColumn(string columnName)
        {
            return GetAllColumnHeaders().Any(h =>
                h.Equals(columnName, StringComparison.OrdinalIgnoreCase));
        }
    }
}