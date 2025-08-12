// =================================================================================================
// © 2025 MyShuttle Team. All rights reserved.
// This file is part of the MyShuttle Project.
// Unauthorized copying, modification, or distribution of this file, via any medium, is strictly prohibited.
// Proprietary and confidential.
//
// File Name   : IFairHistory.cs
// Namespace   : AppOperations
// Author      : MyShuttle Team
// Created On  : 12-Aug-2025
//
// Description : 
//     The IFairHistory interface defines the core methods for interacting with the Fare History 
//     page in the MyShuttle application. It provides functionalities for verifying the fare table’s 
//     visibility, retrieving and filtering fare records, sorting by column, and navigating back 
//     to the dashboard.
//
// Usage Notes :
//     - Implement this interface in a class that performs UI automation tasks for the Fare History page.
//     - Ensure proper waits/synchronization when accessing fare records to avoid timing issues.
//     - Intended for automated UI testing scenarios using Selenium with C#.
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
    /// Defines the contract for Fare History page operations in the MyShuttle application.
    /// </summary>
    public interface IFairHistory
    {
        /// <summary>
        /// Checks if the Fare History table is currently visible.
        /// </summary>
        /// <returns>True if the fare table is visible; otherwise, false.</returns>
        bool IsFareTableVisible();

        /// <summary>
        /// Retrieves the total number of fare records displayed in the table.
        /// </summary>
        /// <returns>The count of fare records.</returns>
        int GetNumberOfFareRecords();

        /// <summary>
        /// Retrieves fare details for a specific row index.
        /// </summary>
        /// <param name="rowIndex">The zero-based index of the row to retrieve details from.</param>
        /// <returns>A string containing the fare details for the specified row.</returns>
        string GetFareDetailsByRow(int rowIndex);

        /// <summary>
        /// Sorts the Fare History table by the specified column name.
        /// </summary>
        /// <param name="columnName">The name of the column to sort by.</param>
        void SortFareHistoryBy(string columnName);

        /// <summary>
        /// Filters the Fare History records based on a search term.
        /// </summary>
        /// <param name="searchTerm">The term to filter fare records by.</param>
        void FilterFareHistory(string searchTerm);

        /// <summary>
        /// Navigates back to the Dashboard from the Fare History page.
        /// </summary>
        void ClickBackToDashboard();

        /// <summary>
        /// Checks if the "Internal Use Only" label is visible on the Fare History page.
        /// </summary>
        /// <returns>True if the label is visible; otherwise, false.</returns>
        bool IsInternalUseOnlyLabelVisible();
    }
}
