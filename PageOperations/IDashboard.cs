using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppOperations
{
    public interface IDashboard
    {
        bool IsDashboardVisible();
        void ClickFareHistory();
        void ClickSignOut();
        bool IsUserLoggedIn();
        bool IsInternalUseOnlyLabelVisible();
    }
}

