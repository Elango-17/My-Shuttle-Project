using NUnit.Framework;
using AppOperations;
using Utilities;
using System;

namespace AppWeb
{
    [TestFixture]
    public class LoginPageTests
    {
        private ILoginPage _loginPage;

        [SetUp]
        public void Setup()
        {
            _loginPage = LogiPageFactory.Create(headless: true);
        }

        [TearDown]
        public void TearDown()
        {
            if (_loginPage is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }

        [Test]
        public void Logo_Should_Be_Visible()
        {
            Assert.IsTrue(_loginPage.IsLogoVisible());
        }

        [Test]
        public void LoginForm_Should_Be_Visible()
        {
            Assert.IsTrue(_loginPage.IsLoginFormVisible());
        }

        [Test]
        public void LoginButton_Should_Be_Disabled_Initially()
        {
            Assert.IsFalse(_loginPage.IsLoginButtonEnabled());
        }

        [Test]
        public void EnterUsername_And_Password_Should_Enable_LoginButton()
        {
            _loginPage.EnterUsername("fred");
            _loginPage.EnterPassword("fredPassword");
            Assert.IsTrue(_loginPage.IsLoginButtonEnabled());
        }

        [Test]
        public void Login_Should_Fail_With_Invalid_Credentials()
        {
            _loginPage.EnterUsername("free");
            _loginPage.EnterPassword("freepassword");
            _loginPage.ClickLogin();

            string error = _loginPage.GetErrorMessage();

            Assert.That(error, Does.Contain("Sorry, please back up and try again"));
            Assert.That(error, Does.Contain("We couldn't find your email and password."));
            Assert.IsFalse(_loginPage.IsLoginSuccessful());
        }


        [Test]
        public void Login_Should_Succeed_With_Valid_Credentials()
        {
            _loginPage.EnterUsername("fred");
            _loginPage.EnterPassword("fredpassword");
            _loginPage.ClickLogin();

            Assert.IsTrue(_loginPage.IsLoginSuccessful());
        }
    }
}
