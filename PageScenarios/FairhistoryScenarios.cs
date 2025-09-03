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

            Assert.That(row1, Is.Not.EqualTo(row0), "Row details for different rows should not be identical.");
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

        [Test]
        public void PageHeading_Should_Contain_LoggedInUserName()
        {
            string heading = _fareHistory.GetPageHeading();
            string user = _fareHistory.GetLoggedInUserName();
            Assert.That(heading, Does.Contain(user), "Page heading should contain logged-in username");
        }

        [Test]
        public void FarePage_Should_Redirect_To_Login_When_SessionExpired()
        {
            _fareHistory.ExpireSession();
            _fareHistory.NavigateTo();
            Assert.That(_fareHistory.IsLoginPageVisible(), Is.True, "Expired session should redirect to login page");
        }

        // 2. Table & Records
        [Test]
        public void FareTable_Should_Display_Correct_Number_Of_Rows()
        {
            int uiCount = _fareHistory.GetFareRowCount();
            int mockCount = _fareHistory.GetMockFareRecordCount();
            Assert.That(uiCount, Is.EqualTo(mockCount), "UI fare row count should match dataset");
        }

        [Test]
        public void GetFareDetailsByRow_Should_Throw_On_InvalidIndex()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _fareHistory.GetFareDetailsByRow(-1));
            Assert.Throws<ArgumentOutOfRangeException>(() => _fareHistory.GetFareDetailsByRow(999));
        }

        [Test]
        public void FareDetails_Should_Not_Be_Empty_For_Each_Row()
        {
            foreach (var row in _fareHistory.GetAllFareDetails())
            {
                Assert.That(row, Is.Not.Null.And.Not.Empty, "Fare details must not be empty");
            }
        }

        // 3. Sorting Tests
        [Test]
        public void SortFareHistoryBy_Fare_Should_Order_Correctly()
        {
            var fares = _fareHistory.SortByFare();
            Assert.That(fares, Is.Ordered, "Fares should be sorted in ascending order");
        }

        [Test]
        public void SortFareHistoryBy_Date_Should_Order_Correctly()
        {
            var dates = _fareHistory.SortByDate();
            Assert.That(dates, Is.Ordered, "Dates should be sorted in ascending order");
        }

        [Test]
        public void SortFareHistoryBy_Driver_Should_Order_Alphabetically()
        {
            var drivers = _fareHistory.SortByDriver();
            Assert.That(drivers, Is.Ordered, "Drivers should be sorted alphabetically");
        }

        // 4. Column Header Tests
        [Test]
        public void HasColumn_Should_Return_True_For_ExistingColumn()
        {
            Assert.That(_fareHistory.HasColumn("Fare"), Is.True);
        }

        [Test]
        public void HasColumn_Should_Return_False_For_NonExistingColumn()
        {
            Assert.That(_fareHistory.HasColumn("NonExistent"), Is.False);
        }

        [Test]
        public void ColumnHeaders_Should_Be_Unique()
        {
            var headers = _fareHistory.GetAllColumnHeaders();
            Assert.That(headers, Is.Unique, "Column headers should be unique");
        }

        // 5. Navigation
        [Test]
        public void ClickBackToDashboard_Should_Navigate_To_DashboardPage()
        {
            _fareHistory.ClickBackToDashboard();
            Assert.That(_fareHistory.IsDashboardVisible(), Is.True);
        }

        [Test]
        public void ClickFareHistory_Again_Should_Not_Throw_Error()
        {
            Assert.DoesNotThrow(() => _fareHistory.ClickFareHistory());
        }

        // 6. Data Integrity
        [Test]
        public void FareRecord_Should_Display_ValidFareAmount()
        {
            foreach (var fare in _fareHistory.GetFareAmounts())
            {
                Assert.That(fare, Is.GreaterThan(0), "Fare amount should be positive");
            }
        }

        [Test]
        public void FareRecord_Should_Display_ValidRatings()
        {
            foreach (var rating in _fareHistory.GetRatings())
            {
                Assert.That(rating, Is.InRange(1, 5), "Rating must be between 1 and 5");
            }
        }

        [Test]
        public void FareRecord_Should_Display_ValidDates()
        {
            foreach (var trip in _fareHistory.GetTripDates())
            {
                Assert.That(trip.Pickup < trip.Dropoff, "Pickup time should be before dropoff time");
            }
        }

        // 7. Negative / Edge Cases
        [Test]
        public void FareTable_Should_Display_Message_When_NoRecordsExist()
        {
            _fareHistory.ClearFareRecords();
            Assert.That(_fareHistory.GetNoRecordMessage(), Is.EqualTo("No records found"));
        }

        [Test]
        public void ColumnHeaders_Should_Handle_SpecialCharacters()
        {
            Assert.DoesNotThrow(() => _fareHistory.HasColumn("Fåré$"));
        }

        [Test]
        public void Large_Number_Of_FareRecords_Should_Not_Break_UI()
        {
            _fareHistory.GenerateLargeDataset(200);
            Assert.That(_fareHistory.GetFareRowCount(), Is.EqualTo(200), "UI should handle 200+ records gracefully");
        }
    }
}