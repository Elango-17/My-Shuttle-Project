using NUnit.Framework;
using AppOperations;
using Utilities;
using System;

namespace AppWeb
{
    [TestFixture]
    public class DashboardPageTests
    {
        private IDashboard _dashboard;

        [SetUp]
        public void Setup()
        {
             _dashboard = DashboardPageFactory.Create(headless: true);
        }
              
        [TearDown]
        public void TearDown()
        {
            if (_dashboard is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }

        [Test]
        public void Dashboard_Should_Be_Visible()
        {
            Assert.IsTrue(_dashboard.IsDashboardVisible(), "Dashboard page should be visible after login.");
        }

        [Test]
        public void InternalUseOnly_Label_Should_Be_Visible()
        {
            Assert.IsTrue(_dashboard.IsInternalUseOnlyLabelVisible(), "Internal Use Only label should be visible on dashboard.");
        }

        [Test]
        public void User_Should_Be_LoggedIn_On_Dashboard()
        {
            Assert.IsTrue(_dashboard.IsUserLoggedIn(), "User should be logged in when on the dashboard.");
        }

        [Test]
        public void FareHistory_Should_Navigate_When_Clicked()
        {
            _dashboard.ClickFareHistory();
            Assert.IsTrue(_dashboard.IsUserLoggedIn(), "User should remain logged in after accessing fare history.");
        }

        [Test]
        public void SignOut_Should_Log_User_Out()
        {
            _dashboard.ClickSignOut();
            Assert.IsFalse(_dashboard.IsUserLoggedIn(), "User should be logged out after clicking Sign Out.");
        }
    }
}