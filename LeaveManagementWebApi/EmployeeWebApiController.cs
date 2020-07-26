using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LeaveManagementModels;
using LeaveManagementManager;

namespace WebApi.Controllers
{
    public class EmployeeWebApiController : ApiController
    {
        [Route("api/EmployeeWebApi/EmployeeDetails")]
        [HttpGet]
        public RegisterModel EmployeeDetails(string username)
        {
            EmployeeManager employeeManager = new EmployeeManager();
            return employeeManager.EmployeeDetails(username);
        }

        [Route("api/EmployeeWebApi/ApplyForLeaveDetails")]
        [HttpPost]
        public void ApplyForLeaveDetails(LeaveModel leaveModel)
        {
            EmployeeManager employeeManager = new EmployeeManager();
            employeeManager.LeaveDetails(leaveModel);
        }

        [Route("api/EmployeeWebApi/GetLeaveDetails")]
        [HttpGet]
        public List<LeaveModel> GetLeaveDetails(int empid)
        {
            
            EmployeeManager employeeManager = new EmployeeManager();
            return employeeManager.GetLeaveDetails(empid);

        }

        [Route("api/EmployeeWebApi/UpdateLeaveBalance")]
        [HttpPost]
        public void UpdateLeaveBalance()
        {
            EmployeeManager employeeManager = new EmployeeManager();
            employeeManager.UpdateLeaveBalance();
        }
    }
}
