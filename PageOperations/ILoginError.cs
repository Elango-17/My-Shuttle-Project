// =================================================================================================
// © 2025 MyShuttle Team. All rights reserved.
// This file is part of the MyShuttle Project.
// Unauthorized copying, modification, or distribution of this file, via any medium, is strictly prohibited.
// Proprietary and confidential.
//
// File Name   : ILoginError.cs
// Namespace   : AppOperations
// Author      : MyShuttle Team
// Created On  : 12-Aug-2025
//
// Description : 
//     The ILoginError interface defines the essential methods for verifying and retrieving
//     login-related error messages in the MyShuttle application. It is designed for use in
//     automated UI testing to validate scenarios involving invalid credentials, blank inputs,
//     or unauthorized access attempts.
//
// Usage Notes :
//     - Implement this interface in a page object class that handles login page error validation.
//     - Ensure synchronization (explicit waits) when checking error message visibility to avoid
//       flakiness in UI tests.
//     - Intended for automated testing scenarios using Selenium with C#.
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
    /// Defines the contract for handling and verifying login error messages
    /// in the MyShuttle application.
    /// </summary>
    public interface ILoginError
    {
        /// <summary>
        /// Checks if any error message is currently visible on the login page.
        /// </summary>
        /// <returns>
        /// True if an error message is visible; otherwise, false.
        /// </returns>
        bool IsErrorMessageVisible();

        /// <summary>
        /// Retrieves the text of the currently displayed login error message.
        /// </summary>
        /// <returns>
        /// A string containing the error message text, or an empty string if no message is displayed.
        /// </returns>
        string GetErrorMessageText();

        /// <summary>
        /// Checks if the displayed error message matches the expected text.
        /// </summary>
        /// <param name="expectedMessage">
        /// The expected error message text to compare against.
        /// </param>
        /// <returns>
        /// True if the actual error message matches the expected message; otherwise, false.
        /// </returns>
        bool DoesErrorMessageMatch(string expectedMessage);
    }
}
