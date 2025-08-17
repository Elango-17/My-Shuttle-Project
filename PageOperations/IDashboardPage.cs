using System;

namespace AppOperations
{
    public interface IDashboardPage
    {
        bool IsDashboardHeaderVisible();
        bool IsLogoVisible();
        string GetInternalMessage();
        void ClickFareHistory();
        void ClickSignOut();
    }
}
