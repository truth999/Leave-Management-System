using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using LeaveManagementModels;
using Oracle.DataAccess.Client;

namespace LeaveManagementRepository
{
    public class AdminRepository
    {
        string str = "Data Source=(DESCRIPTION =" + "(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))" + "(CONNECT_DATA =" + "(SERVER = DEDICATED)" + "(SERVICE_NAME = ORCL)));" + "User Id= system;Password=system;";

        public List<AdminLeaveModel> GetAdminLeaveDetails()
        {
            List<AdminLeaveModel> leavelist = new List<AdminLeaveModel>();
            OracleConnection con = new OracleConnection(str);
            con.Open();
            OracleCommand cmd = new OracleCommand("GET_ADMIN_LEAVE", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("E_DISPLAY", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            OracleDataReader reader = cmd.ExecuteReader();

            while(reader.Read())
            {
                AdminLeaveModel adminLeaveModel = new AdminLeaveModel();

                if (!reader.IsDBNull(reader.GetOrdinal("EMP_ID")))
                {
                    adminLeaveModel.empid = reader.GetInt32(reader.GetOrdinal("EMP_ID"));
                }
                if (!reader.IsDBNull(reader.GetOrdinal("EMP_NAME")))
                {
                    adminLeaveModel.name = reader.GetString(reader.GetOrdinal("EMP_NAME"));
                }
                if (!reader.IsDBNull(reader.GetOrdinal("LEAVE_TYPE")))
                {
                    adminLeaveModel.leavetype = reader.GetString(reader.GetOrdinal("LEAVE_TYPE"));
                }
                if (!reader.IsDBNull(reader.GetOrdinal("LEAVE_START")))
                {
                    adminLeaveModel.startdate = reader.GetDateTime(reader.GetOrdinal("LEAVE_START"));
                }
                if (!reader.IsDBNull(reader.GetOrdinal("LEAVE_END")))
                {
                    adminLeaveModel.enddate = reader.GetDateTime(reader.GetOrdinal("LEAVE_END"));
                }
                if (!reader.IsDBNull(reader.GetOrdinal("LEAVE_DAYS")))
                {
                    adminLeaveModel.leaveduration = reader.GetInt32(reader.GetOrdinal("LEAVE_DAYS"));
                }
                if (!reader.IsDBNull(reader.GetOrdinal("LEAVE_DESCRIPTION")))
                {
                    adminLeaveModel.leavedescription = reader.GetString(reader.GetOrdinal("LEAVE_DESCRIPTION"));
                }
                if (!reader.IsDBNull(reader.GetOrdinal("LEAVE_STATUS")))
                {
                    adminLeaveModel.leavestatus = reader.GetString(reader.GetOrdinal("LEAVE_STATUS"));
                }
                if (!reader.IsDBNull(reader.GetOrdinal("LEAVE_BALANCE")))
                {
                    adminLeaveModel.leavebalance = reader.GetInt32(reader.GetOrdinal("LEAVE_BALANCE"));
                }

                leavelist.Add(adminLeaveModel);
            }

            con.Close();
            return leavelist;
        }

        public List<EmployeeModel> GetEmployeeDetails()
        {
            List<EmployeeModel> employeelist = new List<EmployeeModel>();
            OracleConnection con = new OracleConnection(str);
            con.Open();
            OracleCommand cmd = new OracleCommand("GET_EMPLOYEES", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("E_DISPLAY", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            OracleDataReader reader = cmd.ExecuteReader();

            while(reader.Read())
            {
                EmployeeModel employeeModel = new EmployeeModel();

                if (!reader.IsDBNull(reader.GetOrdinal("EMP_ID")))
                {
                    employeeModel.emp_id = reader.GetInt32(reader.GetOrdinal("EMP_ID"));
                }
                if (!reader.IsDBNull(reader.GetOrdinal("EMP_NAME")))
                {
                    employeeModel.name = reader.GetString(reader.GetOrdinal("EMP_NAME"));
                }
                if (!reader.IsDBNull(reader.GetOrdinal("USERTYPE")))
                {
                    employeeModel.usertype = reader.GetString(reader.GetOrdinal("USERTYPE"));
                }
                if (!reader.IsDBNull(reader.GetOrdinal("EMAIL")))
                {
                    employeeModel.email = reader.GetString(reader.GetOrdinal("EMAIL"));
                }
                if (!reader.IsDBNull(reader.GetOrdinal("MOBILE")))
                {
                    employeeModel.mobile = reader.GetString(reader.GetOrdinal("MOBILE"));
                }
                if (!reader.IsDBNull(reader.GetOrdinal("GENDER")))
                {
                    employeeModel.gender = reader.GetString(reader.GetOrdinal("GENDER"));
                }
                if (!reader.IsDBNull(reader.GetOrdinal("DATEOFBIRTH")))
                {
                    employeeModel.dateofbirth = reader.GetDateTime(reader.GetOrdinal("DATEOFBIRTH"));
                }
                if (!reader.IsDBNull(reader.GetOrdinal("ADDRESS")))
                {
                    employeeModel.address = reader.GetString(reader.GetOrdinal("ADDRESS"));
                }
                if (!reader.IsDBNull(reader.GetOrdinal("CITY")))
                {
                    employeeModel.city = reader.GetString(reader.GetOrdinal("CITY"));
                }
                if (!reader.IsDBNull(reader.GetOrdinal("EMP_STATE")))
                {
                    employeeModel.state = reader.GetString(reader.GetOrdinal("EMP_STATE"));
                }

                employeelist.Add(employeeModel);

            }

            con.Close();
            return employeelist;

        }

        public List<AdminLeaveModel> GetHistoryDetails()
        {
            List<AdminLeaveModel> leavelist = new List<AdminLeaveModel>();
            OracleConnection con = new OracleConnection(str);
            con.Open();
            OracleCommand cmd = new OracleCommand("GET_HISTORY", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("E_DISPLAY", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            OracleDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                AdminLeaveModel adminLeaveModel = new AdminLeaveModel();

                if (!reader.IsDBNull(reader.GetOrdinal("EMP_ID")))
                {
                    adminLeaveModel.empid = reader.GetInt32(reader.GetOrdinal("EMP_ID"));
                }
                if (!reader.IsDBNull(reader.GetOrdinal("EMP_NAME")))
                {
                    adminLeaveModel.name = reader.GetString(reader.GetOrdinal("EMP_NAME"));
                }
                if (!reader.IsDBNull(reader.GetOrdinal("LEAVE_TYPE")))
                {
                    adminLeaveModel.leavetype = reader.GetString(reader.GetOrdinal("LEAVE_TYPE"));
                }
                if (!reader.IsDBNull(reader.GetOrdinal("LEAVE_START")))
                {
                    adminLeaveModel.startdate = reader.GetDateTime(reader.GetOrdinal("LEAVE_START"));
                }
                if (!reader.IsDBNull(reader.GetOrdinal("LEAVE_END")))
                {
                    adminLeaveModel.enddate = reader.GetDateTime(reader.GetOrdinal("LEAVE_END"));
                }
                if (!reader.IsDBNull(reader.GetOrdinal("LEAVE_DAYS")))
                {
                    adminLeaveModel.leaveduration = reader.GetInt32(reader.GetOrdinal("LEAVE_DAYS"));
                }
                if (!reader.IsDBNull(reader.GetOrdinal("LEAVE_DESCRIPTION")))
                {
                    adminLeaveModel.leavedescription = reader.GetString(reader.GetOrdinal("LEAVE_DESCRIPTION"));
                }
                if (!reader.IsDBNull(reader.GetOrdinal("LEAVE_STATUS")))
                {
                    adminLeaveModel.leavestatus = reader.GetString(reader.GetOrdinal("LEAVE_STATUS"));
                }
                if (!reader.IsDBNull(reader.GetOrdinal("LEAVE_BALANCE")))
                {
                    adminLeaveModel.leavebalance = reader.GetInt32(reader.GetOrdinal("LEAVE_BALANCE"));
                }

                leavelist.Add(adminLeaveModel);
            }

            con.Close();
            return leavelist;
        }

        public void ApproveLeave(AdminLeaveModel adminLeave)
        {

            OracleConnection con = new OracleConnection(str);
            con.Open();
            OracleCommand cmd = new OracleCommand("UPDATE_LEAVESTATUS", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("E_ID", adminLeave.empid);
            cmd.Parameters.Add("E_STARTDATE", adminLeave.startdate);
            cmd.Parameters.Add("E_LEAVESTATUS", adminLeave.leavestatus);
            cmd.ExecuteNonQuery();

            con.Close();
        }

        public void RejectLeave(AdminLeaveModel adminLeave)
        {

            OracleConnection con = new OracleConnection(str);
            con.Open();
            OracleCommand cmd = new OracleCommand("UPDATE_LEAVESTATUS", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("E_ID", adminLeave.empid);
            cmd.Parameters.Add("E_STARTDATE", adminLeave.startdate);
            cmd.Parameters.Add("E_LEAVESTATUS", adminLeave.leavestatus);
            cmd.ExecuteNonQuery();

            con.Close();
        }

        public void BlockUser(int empid)
        {
            OracleConnection con = new OracleConnection(str);
            con.Open();
            OracleCommand cmd = new OracleCommand("UPDATE REGISTERTABLE SET USERTYPE = 'Blocked User' WHERE EMP_ID='"+empid+"'", con);
            cmd.CommandType = CommandType.Text;

            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void UnblockUser(int empid)
        {
            OracleConnection con = new OracleConnection(str);
            con.Open();
            OracleCommand cmd = new OracleCommand("UPDATE REGISTERTABLE SET USERTYPE = 'Employee' WHERE EMP_ID='" + empid + "'", con);
            cmd.CommandType = CommandType.Text;

            cmd.ExecuteNonQuery();
            con.Close();
        }

    }
}
