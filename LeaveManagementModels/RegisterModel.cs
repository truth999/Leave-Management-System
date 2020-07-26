using System;

namespace LeaveManagementModels
{
    public class RegisterModel
    {
        public int emp_id { get; set; }
        public string name { get; set; }
        public string usertype { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string gender { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public DateTime dateofbirth { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
    }

    public class LoginModel
    {
        public string username { get; set; }
        public string password { get; set; }
        public string usertype { get; set; }
    }
}
