// =================================================================================================
// © 2025 My Shuttle Team. All rights reserved.
// This file is part of the MyShuttle Project.
// Unauthorized copying, modification, or distribution of this file, via any medium, is strictly prohibited.
// Proprietary and confidential.
//
// File Name   : IDashboard.cs
// Namespace   : AppOperations
// Author      : MyShuttle Team
// Created On  : 12-Aug-2025
//
// Description : 
//     The IDashboard interface defines the essential methods for interacting with the application's
//     dashboard page in the MyShuttle project. It serves as an abstraction for UI automation tests,
//     enabling verification of dashboard visibility, navigation actions (fare history, sign-out),
//     and visibility of internal-use-only labels.
//
// Usage Notes :
//     - Implement this interface in a class that interacts with the Dashboard UI elements.
//     - Ensure proper synchronization (waits) when verifying visibility or performing click actions.
//     - Intended for use in automated testing scenarios using Selenium with C#.
//
// =================================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AppOperations
{
    /// <summary>
    /// Defines the contract for Dashboard-related UI operations in the MyShuttle application.
    /// </summary>
    public interface IDashboard
    {
        /// <summary>
        /// Checks if the dashboard page is currently visible to the user.
        /// </summary>
        /// <returns>
        /// True if the dashboard is visible; otherwise, false.
        /// </returns>
        bool IsDashboardVisible();

        /// <summary>
        /// Navigates to the Fare History section from the dashboard.
        /// </summary>
        void ClickFareHistory();

        /// <summary>
        /// Signs the user out of the application from the dashboard.
        /// </summary>
        void ClickSignOut();

        /// <summary>
        /// Determines if the user is currently logged in.
        /// </summary>
        /// <returns>
        /// True if the user is logged in; otherwise, false.
        /// </returns>
        bool IsUserLoggedIn();

        /// <summary>
        /// Checks if the "Internal Use Only" label is visible on the dashboard.
        /// </summary>
        /// <returns>
        /// True if the label is visible; otherwise, false.
        /// </returns>
        bool IsInternalUseOnlyLabelVisible();
    }
}

