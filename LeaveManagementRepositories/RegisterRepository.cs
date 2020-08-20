using System;
using Oracle.DataAccess.Client;
using LeaveManagementModels;
using System.Data;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.Configuration;

namespace LeaveManagementRepository
{
    public class InsertRegister
    {
        //string str = "Data Source=(DESCRIPTION =" + "(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))" + "(CONNECT_DATA =" + "(SERVER = DEDICATED)" + "(SERVICE_NAME = ORCL)));" + "User Id= system;Password=system;";

        string connStr = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString; 

        public void InsertRegisterData(RegisterModel register)
        {

            OracleConnection con = new OracleConnection(connStr);
            con.Open();
            OracleCommand cmd = new OracleCommand("INSERT_REGISTERTABLE", con);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {

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
                cmd.Parameters.Add("E_CREATEDBY", register.name);
                cmd.Parameters.Add("E_CREATEDDATETIME", register.created_datetime);
                cmd.Parameters.Add("E_UPDATEDBY", register.name);
                cmd.Parameters.Add("E_UPDATEDDATETIME", register.created_datetime);
                cmd.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
            
        }

        public string CheckLogin(LoginModel login)
        {

            OracleConnection con = new OracleConnection(connStr);
            con.Open();
            OracleCommand cmd = new OracleCommand("CHECK_LOGIN", con);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cmd.Parameters.Add("E_USERNAME", login.username);
                cmd.Parameters.Add("E_PASSWORD", login.password);
                cmd.Parameters.Add("E_USERTYPE", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                OracleDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (!reader.IsDBNull(reader.GetOrdinal("USERTYPE")))
                    {
                        login.usertype = reader.GetString(reader.GetOrdinal("USERTYPE"));
                    }
                    //login.usertype = reader["USERTYPE"].ToString();
                }

                reader.Dispose();
            }
            catch(Exception e)
            {
                throw e;
            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
                       
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
            OracleConnection con = new OracleConnection(connStr);
            con.Open();
            OracleCommand cmd = new OracleCommand("FORGET_PASSWORD", con);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                cmd.Parameters.Add("E_EMAIL", email);
                cmd.Parameters.Add("E_DISPLAY", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
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

                reader.Dispose();

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {

                con.Close();
                cmd.Dispose();
            }
            
            if (!string.IsNullOrEmpty(register.username))
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
