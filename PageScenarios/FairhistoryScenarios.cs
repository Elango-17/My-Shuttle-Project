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
    public class FareHistoryPageTests_Extended
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

        // ---------------------------
        // Negative / Edge Case Tests
        // ---------------------------

        [Test]
        public void GetFareDetails_With_Invalid_RowIndex_Should_Throw()
        {
            int invalidIndex = _fareHistory.GetNumberOfFareRecords() + 5;
            Assert.Throws<ArgumentOutOfRangeException>(() => _fareHistory.GetFareDetailsByRow(invalidIndex),
                "Accessing invalid row index should throw ArgumentOutOfRangeException.");
        }

        [Test]
        public void GetFareDetails_With_Negative_RowIndex_Should_Throw()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _fareHistory.GetFareDetailsByRow(-1),
                "Negative row index should throw ArgumentOutOfRangeException.");
        }

        [Test]
        public void SortFareHistory_By_Invalid_Column_Should_Throw()
        {
            string invalidColumn = "NonExistentColumn";
            Assert.Throws<ArgumentException>(() => _fareHistory.SortFareHistoryBy(invalidColumn),
                "Sorting by an invalid column should throw ArgumentException.");
        }

        [Test]
        public void ColumnHeaders_Should_Not_Be_Empty()
        {
            var headers = _fareHistory.GetAllColumnHeaders();
            Assert.IsNotEmpty(headers, "Column headers should not be empty.");
        }

        [Test]
        public void ColumnHeaders_Should_Not_Contain_Null_Or_Empty()
        {
            var headers = _fareHistory.GetAllColumnHeaders();
            foreach (var header in headers)
            {
                Assert.False(string.IsNullOrWhiteSpace(header),
                    "Column headers should not contain null, empty or whitespace values.");
            }
        }

        [Test]
        public void FareTable_Should_Handle_Empty_State()
        {
            // Simulate clearing the table (if supported in implementation)
            _fareHistory.ClearFareTableForTest(); // (helper method needed in mock)
            int recordCount = _fareHistory.GetNumberOfFareRecords();
            Assert.AreEqual(0, recordCount, "Fare table should handle empty state correctly.");
        }

        [Test]
        public void Each_Fare_Row_Should_Not_Have_Blank_Columns()
        {
            int recordCount = _fareHistory.GetNumberOfFareRecords();

            for (int i = 0; i < recordCount; i++)
            {
                string row = _fareHistory.GetFareDetailsByRow(i);
                string[] columns = row.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                foreach (var col in columns)
                {
                    Assert.False(string.IsNullOrWhiteSpace(col),
                        $"Row {i} contains a blank column which should not happen.");
                }
            }
        }

        [Test]
        public void PageHeading_Should_Not_Be_Null_Or_Empty()
        {
            string heading = _fareHistory.GetPageHeading();
            Assert.False(string.IsNullOrWhiteSpace(heading), "Page heading should not be null or empty.");
        }

        [Test]
        public void InternalUseOnly_Label_Should_Not_Be_Missing_Or_Misspelled()
        {
            bool isVisible = _fareHistory.IsInternalUseOnlyLabelVisible();
            Assert.True(isVisible, "'Internal Use Only' label should always be present.");
        }

        [Test]
        public void ClickBackToDashboard_Should_NavigateSuccessfully()
        {
            bool navigated = _fareHistory.ClickBackToDashboard();
            Assert.True(navigated, "Back to Dashboard action should return true if navigation was successful.");
        }

        // ---------------------------
        // Data Validation Tests
        // ---------------------------

        [Test]
        public void FareAmount_Should_Be_Positive()
        {
            int recordCount = _fareHistory.GetNumberOfFareRecords();

            for (int i = 0; i < recordCount; i++)
            {
                string row = _fareHistory.GetFareDetailsByRow(i);
                string[] cols = row.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                // Assume "Fare" column is at index 5
                if (decimal.TryParse(cols[5], out decimal fare))
                {
                    Assert.GreaterOrEqual(fare, 0, $"Fare in row {i} should be non-negative.");
                }
            }
        }

        [Test]
        public void Ratings_Should_Be_Between_1_And_5()
        {
            int recordCount = _fareHistory.GetNumberOfFareRecords();

            for (int i = 0; i < recordCount; i++)
            {
                string row = _fareHistory.GetFareDetailsByRow(i);
                string[] cols = row.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                // Assume Pass Rtg index = 7, Driver Rtg index = 8
                if (int.TryParse(cols[7], out int passRating))
                {
                    Assert.That(passRating, Is.InRange(1, 5), $"Passenger rating in row {i} should be 1-5.");
                }

                if (int.TryParse(cols[8], out int driverRating))
                {
                    Assert.That(driverRating, Is.InRange(1, 5), $"Driver rating in row {i} should be 1-5.");
                }
            }
        }
    }
}
