using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppOperations
{
    public interface IFairHistory
    {
        bool IsFareTableVisible();
        int GetNumberOfFareRecords();
        string GetFareDetailsByRow(int rowIndex);
        void SortFareHistoryBy(string columnName);
        void FilterFareHistory(string searchTerm);
        void ClickBackToDashboard();
        bool IsInternalUseOnlyLabelVisible();
    }
}
