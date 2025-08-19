using AppOperations;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Utilities;

namespace AppWeb
{
    [TestFixture]
    public class FareHistoryPageTests
    {
        private IFairHistory _fareHistory;

        [SetUp]
        public void Setup()
        {
            _fareHistory = FareHistoryPageFactory.Create(headless: true);
        }

        [TearDown]
        public void TearDown()
        {
            if (_fareHistory is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }

        [Test]
        public void PageHeading_Should_Be_EmployeeFares()
        {
            string expectedHeading = "Employee Fares for";
            string actualHeading = _fareHistory.GetPageHeading();
            StringAssert.StartsWith(expectedHeading, actualHeading, "Heading text did not match expected value.");
        }

        [Test]
        public void FareTable_Should_Be_Visible()
        {
            Assert.IsTrue(_fareHistory.IsFareTableVisible(), "Fare table should be visible.");
        }

        [Test]
        public void FareTable_Should_Have_AtLeast_One_Record()
        {
            int recordCount = _fareHistory.GetNumberOfFareRecords();
            Assert.Greater(recordCount, 0, "Fare table should have at least one record.");
        }

        [Test]
        public void Each_Fare_Row_Should_Have_All_Columns()
        {
            string firstRow = _fareHistory.GetFareDetailsByRow(0);
            string[] columns = firstRow.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            Assert.GreaterOrEqual(columns.Length, 7, "Each row should have at least 7 columns of data.");
        }

        [Test]
        public void GetFareDetails_Should_Return_Different_Rows()
        {
            string row0 = _fareHistory.GetFareDetailsByRow(0);
            string row1 = _fareHistory.GetFareDetailsByRow(1);

            Assert.AreNotEqual(row0, row1, "Row details for different rows should not be identical.");
        }

        [Test]
        public void ColumnHeaders_Should_Match_Expected()
        {
            var expectedHeaders = new List<string>
            {
               "ID",   "Start" ,  "Pickup" , "End", "Dropoff" ,"Fare" ,   "Driver",  "Pass Rtg"  ,  "Drvr Rtg"
            };

            var actualHeaders = _fareHistory.GetAllColumnHeaders();


            CollectionAssert.AreEquivalent(expectedHeaders, actualHeaders,
                "Fare history table headers should match the expected set.");
        }

        [Test]
        public void All_Columns_Should_Be_Sortable()
        {
            var headers = _fareHistory.GetAllColumnHeaders();

            foreach (var header in headers)
            {
                Assert.DoesNotThrow(() => _fareHistory.SortFareHistoryBy(header),
                    $"Column '{header}' should be sortable.");
            }
        }

        [Test]
        public void InternalUseOnly_Label_Should_Be_Visible()
        {
            Assert.IsTrue(_fareHistory.IsInternalUseOnlyLabelVisible(),
                "Internal Use Only label should be visible.");
        }

        [Test]
        public void ClickBackToDashboard_Should_Work()
        {
            Assert.DoesNotThrow(() => _fareHistory.ClickBackToDashboard(),
                "Clicking back to dashboard should not throw.");
        }
    }
}
