using System;
using Oracle.DataAccess.Client;
using LeaveManagementModels;
using System.Data;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;

namespace LeaveManagementRepository
{
    public class InsertRegister
    {
        string str = "Data Source=(DESCRIPTION =" + "(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))" + "(CONNECT_DATA =" + "(SERVER = DEDICATED)" + "(SERVICE_NAME = ORCL)));" + "User Id= system;Password=system;";

        public void InsertRegisterData(RegisterModel register)
        {

            OracleConnection con = new OracleConnection(str);
            con.Open();
            OracleCommand cmd = new OracleCommand("INSERT_REGISTERTABLE", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("E_NAME", register.name);
            cmd.Parameters.Add("E_USERTYPE", register.usertype);
            cmd.Parameters.Add("E_USERNAME", register.username);
            cmd.Parameters.Add("E_PASSWORD", register.password);
            cmd.Parameters.Add("E_GENDER", register.gender);
            cmd.Parameters.Add("E_MOBILE", register.mobile);
            cmd.Parameters.Add("E_EMAIL", register.email);
            cmd.Parameters.Add("E_DATEOFBIRTH", register.dateofbirth);
            cmd.Parameters.Add("E_ADDRESS", register.address);
            cmd.Parameters.Add("E_CITY", register.city);
            cmd.Parameters.Add("E_STATE", register.state);
            cmd.ExecuteNonQuery();
            
            con.Close();
        }

        public string CheckLogin(LoginModel login)
        {

            OracleConnection con = new OracleConnection(str);
            con.Open();
            OracleCommand cmd = new OracleCommand("SELECT USERTYPE FROM REGISTERTABLE WHERE USERNAME = '" + login.username + "' AND EMP_PASSWORD = '" + login.password + "'", con);

            OracleDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                login.usertype = reader["USERTYPE"].ToString();
            }
            con.Close();
            
            if(login.usertype == "Admin")
            {
                return "Admin Success";
            }
            else if(login.usertype == "Employee")
            {
                return "Login Success";
            }
            else if(login.usertype == "Blocked User")
            {
                return "Blocked User";
            }
            else
            {
                return "Login Error";
            }
            
        }

        public string ForgetPassword(string email)
        {
            RegisterModel register = new RegisterModel();
            OracleConnection con = new OracleConnection(str);
            con.Open();
            OracleCommand cmd = new OracleCommand("SELECT USERNAME,EMP_PASSWORD FROM REGISTERTABLE WHERE EMAIL='" + email + "'", con);
            OracleDataReader reader = cmd.ExecuteReader();

            while(reader.Read())
            {                
                if (!reader.IsDBNull(reader.GetOrdinal("USERNAME")))
                {
                    register.username = reader.GetString(reader.GetOrdinal("USERNAME"));
                }
                if (!reader.IsDBNull(reader.GetOrdinal("EMP_PASSWORD")))
                {
                    register.password = reader.GetString(reader.GetOrdinal("EMP_PASSWORD"));
                }

                MailMessage message = new MailMessage("darshilbavishi2@gmail.com", email);
                message.Subject = "Forgot Password !!";
                message.Body = string.Format("Hello : <h1>{0}</h1> is your Username <br/> Your Password is <h1>{1}</h1>", register.username, register.password);
                message.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential networkCredential = new NetworkCredential();
                networkCredential.UserName = "darshilbavishi2@gmail.com";
                networkCredential.Password = "darshil021198";
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = networkCredential;
                smtp.Port = 587;
                smtp.Send(message);
            }
            con.Close();
            

            if(!string.IsNullOrEmpty(register.username))
            {
                return "Registered Email";
            }
            else
            {
                return "Unregistered Email";
            }

            
        }
    }
}
