using System;
using System.Collections.Generic;
using System.Text;
using LeaveManagementModels;
using LeaveManagementRepository;

namespace LeaveManagementManager
{
    public class RegisterManager
    {
        public void ManageRegister(RegisterModel register)
        {
            InsertRegister insertRegister = new InsertRegister();
            insertRegister.InsertRegisterData(register);
        }

        public void ForgetPassword(string email)
        {
            InsertRegister insertRegister = new InsertRegister();
            insertRegister.ForgetPassword(email);
        }
    }

    public class LoginManager
    {
        public string ManageLogin(LoginModel login)
        {
            InsertRegister insertRegister = new InsertRegister();
            return insertRegister.CheckLogin(login);
        }
    }

    
}
