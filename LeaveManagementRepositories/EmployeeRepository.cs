using System;
using System.Collections.Generic;
using System.Text;
using Oracle.DataAccess.Client;
using LeaveManagementModels;
using System.Data;
using System.Configuration;

namespace LeaveManagementRepository
{
    public class EmployeeRepository
    {
        
        //string str = "Data Source=(DESCRIPTION =" + "(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))" + "(CONNECT_DATA =" + "(SERVER = DEDICATED)" + "(SERVICE_NAME = ORCL)));" + "User Id= system;Password=system;";

        string connStr = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
        
        public RegisterModel EmployeeDetails(string username)
        {
            RegisterModel registerModel = new RegisterModel();
            OracleConnection con = new OracleConnection(connStr);
            con.Open();
            OracleCommand cmd = new OracleCommand("GET_EMPLOYEEDETAIL", con);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                cmd.Parameters.Add("E_USERNAME", username);
                cmd.Parameters.Add("E_DISPLAY", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                OracleDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (!reader.IsDBNull(reader.GetOrdinal("EMP_ID")))
                    {
                        registerModel.emp_id = reader.GetInt32(reader.GetOrdinal("EMP_ID"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("EMP_NAME")))
                    {
                        registerModel.name = reader.GetString(reader.GetOrdinal("EMP_NAME"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("EMAIL")))
                    {
                        registerModel.email = reader.GetString(reader.GetOrdinal("EMAIL"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("MOBILE")))
                    {
                        registerModel.mobile = reader.GetString(reader.GetOrdinal("MOBILE"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("GENDER")))
                    {
                        registerModel.gender = reader.GetString(reader.GetOrdinal("GENDER"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("DATEOFBIRTH")))
                    {
                        registerModel.dateofbirth = reader.GetDateTime(reader.GetOrdinal("DATEOFBIRTH"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("ADDRESS")))
                    {
                        registerModel.address = reader.GetString(reader.GetOrdinal("ADDRESS"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("CITY")))
                    {
                        registerModel.city = reader.GetString(reader.GetOrdinal("CITY"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("EMP_STATE")))
                    {
                        registerModel.state = reader.GetString(reader.GetOrdinal("EMP_STATE"));
                    }

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
            
            return registerModel;
        }

        public void LeaveDetails(LeaveModel leaveModel)
        {

            OracleConnection con = new OracleConnection(connStr);
            con.Open();
            OracleCommand cmd = new OracleCommand("INSERT_LEAVETABLE", con);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                cmd.Parameters.Add("E_ID", leaveModel.empid);
                cmd.Parameters.Add("E_LEAVETYPE", leaveModel.leavetype);
                cmd.Parameters.Add("E_STARTDATE", leaveModel.startdate);
                cmd.Parameters.Add("E_ENDDATE", leaveModel.enddate);
                cmd.Parameters.Add("E_LEAVEDURATION", leaveModel.leaveduration);
                cmd.Parameters.Add("E_LEAVEDESCRIPTION", leaveModel.leavedescription);
                cmd.Parameters.Add("E_LEAVESTATUS", leaveModel.leavestatus);
                cmd.Parameters.Add("E_LEAVEBALANCE", leaveModel.leavebalance);
                cmd.Parameters.Add("E_CREATEDBY", leaveModel.name);
                cmd.Parameters.Add("E_CREATEDDATETIME", leaveModel.created_datetime);
                cmd.Parameters.Add("E_UPDATEDBY", leaveModel.name);
                cmd.Parameters.Add("E_UPDATEDDATETIME", leaveModel.created_datetime);
                cmd.ExecuteNonQuery();
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
        }

        public List<LeaveModel> GetLeaveDetails(int empid)
        {
            
            List<LeaveModel> leavelist = new List<LeaveModel>();
            OracleConnection con = new OracleConnection(connStr);
            con.Open();
            OracleCommand cmd = new OracleCommand("GET_LEAVE", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("E_ID", empid);
            cmd.Parameters.Add("E_DISPLAY", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            OracleDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                LeaveModel leaveModel = new LeaveModel();

                if (!reader.IsDBNull(reader.GetOrdinal("LEAVE_TYPE")))
                {
                    leaveModel.leavetype = reader.GetString(reader.GetOrdinal("LEAVE_TYPE"));
                }
                if (!reader.IsDBNull(reader.GetOrdinal("LEAVE_START")))
                {
                    leaveModel.startdate = reader.GetDateTime(reader.GetOrdinal("LEAVE_START"));
                }
                if (!reader.IsDBNull(reader.GetOrdinal("LEAVE_END")))
                {
                    leaveModel.enddate = reader.GetDateTime(reader.GetOrdinal("LEAVE_END"));
                }
                if (!reader.IsDBNull(reader.GetOrdinal("LEAVE_DAYS")))
                {
                    leaveModel.leaveduration = reader.GetInt32(reader.GetOrdinal("LEAVE_DAYS"));
                }
                if (!reader.IsDBNull(reader.GetOrdinal("LEAVE_DESCRIPTION")))
                {
                    leaveModel.leavedescription = reader.GetString(reader.GetOrdinal("LEAVE_DESCRIPTION"));
                }
                if (!reader.IsDBNull(reader.GetOrdinal("LEAVE_STATUS")))
                {
                    leaveModel.leavestatus = reader.GetString(reader.GetOrdinal("LEAVE_STATUS"));
                }
                if (!reader.IsDBNull(reader.GetOrdinal("LEAVE_BALANCE")))
                {
                    leaveModel.leavebalance = reader.GetInt32(reader.GetOrdinal("LEAVE_BALANCE"));
                }

                leavelist.Add(leaveModel);
            }

            con.Close();
            cmd.Dispose();
            reader.Dispose();
            return leavelist;
        }

        public void UpdateProfileDetails(ProfileModel profileModel)
        {

            OracleConnection con = new OracleConnection(connStr);
            con.Open();
            OracleCommand cmd = new OracleCommand("UPDATE_PROFILE", con);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                cmd.Parameters.Add("E_ID", profileModel.emp_id);
                cmd.Parameters.Add("E_NAME", profileModel.name);
                cmd.Parameters.Add("E_GENDER", profileModel.gender);
                cmd.Parameters.Add("E_MOBILE", profileModel.mobile);
                cmd.Parameters.Add("E_EMAIL", profileModel.email);
                cmd.Parameters.Add("E_ADDRESS", profileModel.address);
                cmd.Parameters.Add("E_CITY", profileModel.city);
                cmd.Parameters.Add("E_STATE", profileModel.state);
                cmd.Parameters.Add("E_UPDATEDBY", profileModel.name);
                cmd.Parameters.Add("E_UPDATEDDATETIME", profileModel.updated_datetime);
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

        //public void CancelLeave(LeaveModel leaveModel)
        //{
        //    OracleConnection con = new OracleConnection(str);
        //    con.Open();
        //    OracleCommand cmd = new OracleCommand("UPDATE LEAVETABLE SET LEAVE_STATUS = '"+leaveModel.leavestatus+"' WHERE EMP_ID = '"+leaveModel.empid+"' AND LEAVE_START = '"+leaveModel.startdate+"'",con);
        //    cmd.CommandType = CommandType.Text;

        //    cmd.ExecuteNonQuery();
        //    con.Close();
        //}

        public void CancelLeave(LeaveModel leaveModel)
        {
            OracleConnection con = new OracleConnection(connStr);
            con.Open();
            OracleCommand cmd = new OracleCommand("UPDATE_LEAVESTATUS", con);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                cmd.Parameters.Add("E_ID", leaveModel.empid);
                cmd.Parameters.Add("E_STARTDATE", leaveModel.startdate);
                cmd.Parameters.Add("E_LEAVESTATUS", leaveModel.leavestatus);
                cmd.ExecuteNonQuery();
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
            
        }

        public List<MasterData> GetMasterData()
        {
            List<MasterData> masterlist = new List<MasterData>();
            OracleConnection con = new OracleConnection(connStr);
            con.Open();
            OracleCommand cmd = new OracleCommand("GET_MASTERDATA",con);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                cmd.Parameters.Add("E_DISPLAY", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                OracleDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    MasterData masterData = new MasterData();

                    if (!reader.IsDBNull(reader.GetOrdinal("KEY")))
                    {
                        masterData.Key = reader.GetString(reader.GetOrdinal("KEY"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("VALUE")))
                    {
                        masterData.Value = reader.GetString(reader.GetOrdinal("VALUE"));
                    }

                    masterlist.Add(masterData);
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
            
            return masterlist;
        }

        public void UpdateLeaveBalance()
        {
            OracleConnection con = new OracleConnection(connStr);
            con.Open();
            OracleCommand cmd = new OracleCommand("UPDATE_LEAVEBALANCE", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.ExecuteNonQuery();
            con.Close();
            cmd.Dispose();
            
        }
    }
}
