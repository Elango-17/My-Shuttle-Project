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

        // 🔹 New methods
        IList<string> GetAllColumnHeaders();
        bool HasColumn(string columnName);
    }
}