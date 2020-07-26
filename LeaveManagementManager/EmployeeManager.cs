using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using LeaveManagementModels;
using LeaveManagementRepository;

namespace LeaveManagementManager
{
    public class EmployeeManager
    {
        public RegisterModel EmployeeDetails(string username)
        {
            EmployeeRepository employeeRepository = new EmployeeRepository();
            return employeeRepository.EmployeeDetails(username);
        }

        public void LeaveDetails(LeaveModel leaveModel)
        {
            EmployeeRepository employeeRepository = new EmployeeRepository();
            employeeRepository.LeaveDetails(leaveModel);
        }

        public List<LeaveModel> GetLeaveDetails(int empid)
        {
            EmployeeRepository employeeRepository = new EmployeeRepository();
            return employeeRepository.GetLeaveDetails(empid);
        }

        public void UpdateProfileDetails(ProfileModel profileModel)
        {
            EmployeeRepository employeeRepository = new EmployeeRepository();
            employeeRepository.UpdateProfileDetails(profileModel);
        }

        public void CancelLeave(LeaveModel leaveModel)
        {
            EmployeeRepository employeeRepository = new EmployeeRepository();
            employeeRepository.CancelLeave(leaveModel);
        }
    }
}
