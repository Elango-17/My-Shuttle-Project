using System;
using System.Collections.Generic;

namespace AppOperations
{
    public interface IFairHistory
    {
        string GetPageHeading();
        string GetLoggedInUserName();
        bool IsFareTableVisible();
        int GetNumberOfFareRecords();
        string GetFareDetailsByRow(int rowIndex);
        void SortFareHistoryBy(string columnName);
        void ClickBackToDashboard();
        bool IsInternalUseOnlyLabelVisible();
        IList<string> GetAllColumnHeaders();
        bool HasColumn(string columnName);

        void NavigateTo();
        void ExpireSession();
        bool IsLoginPageVisible();
        int GetFareRowCount();
        int GetMockFareRecordCount();

        IEnumerable<string> GetAllFareDetails();
        IEnumerable<decimal> SortByFare();
        IEnumerable<DateTime> SortByDate();
        IEnumerable<string> SortByDriver();
        bool IsDashboardVisible();
        IEnumerable<decimal> GetFareAmounts();
        IEnumerable<int> GetRatings();
        IEnumerable<(DateTime Pickup, DateTime Dropoff)> GetTripDates();
        void ClearFareRecords();
        string GetNoRecordMessage();
        void GenerateLargeDataset(int count);
        void ClickFareHistory();
    }
}
