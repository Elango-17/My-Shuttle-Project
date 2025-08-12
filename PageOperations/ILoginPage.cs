// =================================================================================================
// © 2025 MyShuttle Team. All rights reserved.
// This file is part of the MyShuttle Project.
// Unauthorized copying, modification, or distribution of this file, via any medium, is strictly prohibited.
// Proprietary and confidential.
//
// File Name   : ILoginPage.cs
// Namespace   : AppOperations
// Author      : MyShuttle Team
// Created On  : 12-Aug-2025
//
// Description : 
//     The ILoginPage interface defines the essential operations for interacting with the login 
//     page of the MyShuttle application. It provides methods to verify UI elements, enter 
//     credentials, initiate the login process, and validate login results.
//
// Usage Notes :
//     - Implement this interface in a page object class that maps to the login page’s UI elements.
//     - Ensure proper waits (explicit or implicit) when interacting with elements to avoid flaky tests.
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
    /// Defines the contract for interacting with the login page in the MyShuttle application.
    /// </summary>
    public interface ILoginPage
    {
        /// <summary>
        /// Checks if the company or application logo is visible on the login page.
        /// </summary>
        /// <returns>True if the logo is visible; otherwise, false.</returns>
        bool IsLogoVisible();

        /// <summary>
        /// Checks if the login form (username, password fields) is visible.
        /// </summary>
        /// <returns>True if the login form is visible; otherwise, false.</returns>
        bool IsLoginFormVisible();

        /// <summary>
        /// Enters the username into the login form.
        /// </summary>
        /// <param name="username">The username to enter.</param>
        void EnterUsername(string username);

        /// <summary>
        /// Enters the password into the login form.
        /// </summary>
        /// <param name="password">The password to enter.</param>
        void EnterPassword(string password);

        /// <summary>
        /// Clicks the login button to initiate the login process.
        /// </summary>
        void ClickLogin();

        /// <summary>
        /// Retrieves the error message displayed after a failed login attempt.
        /// </summary>
        /// <returns>
        /// The error message text if present; otherwise, an empty string.
        /// </returns>
        string GetErrorMessage();

        /// <summary>
        /// Checks if the login button is currently enabled.
        /// </summary>
        /// <returns>True if the login button is enabled; otherwise, false.</returns>
        bool IsLoginButtonEnabled();

        /// <summary>
        /// Checks if the login attempt was successful.
        /// </summary>
        /// <returns>True if login succeeded; otherwise, false.</returns>
        bool IsLoginSuccessful();
    }
}
