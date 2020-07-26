using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LeaveManagementModels;
using LeaveManagementManager;

namespace Leave_Management.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult AdminDashboard(string username)
        {
            if (!string.IsNullOrEmpty(username))
            {
                ViewBag.username = username;
                Session["username"] = username;
            }
            return View();
        }

        public ActionResult EmployeeDetails()
        {
            ViewBag.username = Session["username"];
            return View();
        }

        public ActionResult HistoryDetails()
        {
            ViewBag.username = Session["username"];
            return View();
        }

        public JsonResult AdminDetails(string username)
        {
            RegisterModel register = new RegisterModel();
            EmployeeManager employeeManager = new EmployeeManager();
            register = employeeManager.EmployeeDetails(username);

            return Json(register, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAdminLeaveDetails()
        {
            AdminLeaveModel adminLeaveModel = new AdminLeaveModel();
            AdminManager adminManager = new AdminManager();
            List<AdminLeaveModel> leavelist = adminManager.GetAdminLeaveDetails();

            return Json(leavelist, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEmployeeDetails()
        {
            AdminManager adminManager = new AdminManager();
            List<EmployeeModel> employeelist = adminManager.GetEmployeeDetails();

            return Json(employeelist, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetHistoryDetails()
        {
            AdminManager adminManager = new AdminManager();
            List<AdminLeaveModel> leavelist = adminManager.GetHistoryDetails();

            return Json(leavelist, JsonRequestBehavior.AllowGet);
        }

        public void ApproveLeave(AdminLeaveModel adminLeave)
        {
            adminLeave.startdate = Convert.ToDateTime(adminLeave.startdate);

            if(adminLeave.leavestatus == "Applied")
            {
                adminLeave.leavestatus = "Approved";
            }
            else 
            {
                adminLeave.leavestatus = "Canceled";
            }
            
            AdminManager adminManager = new AdminManager();
            adminManager.ApproveLeave(adminLeave);
        }

        public void RejectLeave(AdminLeaveModel adminLeave)
        {

            adminLeave.startdate = Convert.ToDateTime(adminLeave.startdate);

            if (adminLeave.leavestatus == "Applied")
            {
                adminLeave.leavestatus = "Rejected";
            }
            else
            {
                adminLeave.leavestatus = "Approved";
            }

            AdminManager adminManager = new AdminManager();
            adminManager.RejectLeave(adminLeave);
        }

        public void BlockUser(EmployeeModel employeeModel)
        {
            AdminManager adminManager = new AdminManager();
            adminManager.BlockUser(employeeModel.emp_id);
        }

        public void UnblockUser(EmployeeModel employeeModel)
        {
            AdminManager adminManager = new AdminManager();
            adminManager.UnblockUser(employeeModel.emp_id);
        }
    }
}