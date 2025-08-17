using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppOperations
{
    public interface ILoginPage
    {
        bool IsLogoVisible();
        bool IsLoginFormVisible();
        void EnterUsername(string username);
        void EnterPassword(string password);
        void ClickLogin();
        string GetErrorMessage();
        bool IsLoginButtonEnabled();
        bool IsLoginSuccessful();
    }
}