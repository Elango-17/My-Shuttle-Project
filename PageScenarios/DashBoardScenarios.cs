using NUnit.Framework;
using AppOperations;
using Utilities;

namespace AppWeb
{
    [TestFixture]
    public class DashboardPageScenarios
    {
        private ILoginPage _loginPage;
        private IDashboardPage _dashboardPage;

        [SetUp]
        public void SetUp()
        {
            // Get login page (driver created internally in LoginPageFactory)
            _loginPage = LoginPageFactory.Create();

            // Perform login (credentials only here)
            _loginPage.EnterUsername("fred");
            _loginPage.EnterPassword("fredpassword");
            _loginPage.ClickLogin();

            // Get driver through factory helper (so we don’t touch LoginPage directly)
            var driver = DriverFactory.GetDriverFrom(_loginPage);

            // Create dashboard with the same driver
            _dashboardPage = DashboardPageFactory.Create(driver);
        }

        [TearDown]
        public void TearDown()
        {
            // Close driver via helper factory
            DriverFactory.QuitDriver(_loginPage);
        }

        [Test]
        public void Dashboard_Header_ShouldBeVisible()
        {
            Assert.IsTrue(_dashboardPage.IsDashboardHeaderVisible(),
                "Dashboard header should be visible after login.");
        }

        [Test]
        public void Dashboard_Logo_ShouldBeVisible()
        {
            Assert.IsTrue(_dashboardPage.IsLogoVisible(),
                "Dashboard logo should be visible.");
        }

        [Test]
        public void Dashboard_InternalMessage_ShouldMatch()
        {
            string message = _dashboardPage.GetInternalMessage();
            Assert.IsNotNull(message, "Internal message should not be null.");
            TestContext.WriteLine($"Internal Message: {message}");
        }

        [Test]
        public void Dashboard_FareHistory_ShouldBeClickable()
        {
            Assert.DoesNotThrow(() => _dashboardPage.ClickFareHistory(),
                "Fare history button should be clickable.");
        }

        [Test]
        public void Dashboard_SignOut_ShouldWork()
        {
            _dashboardPage.ClickSignOut();
            Assert.IsTrue(_loginPage.IsLoginFormVisible(),
                "After signing out, login form should be visible.");
        }
    }
}
