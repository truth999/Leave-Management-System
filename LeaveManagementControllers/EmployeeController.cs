using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LeaveManagementModels;
using LeaveManagementManager;
using System.Data;

namespace Leave_Management.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Dashboard
        [Route("Dashboard")]
        public ActionResult Dashboard(string username)
        {
            if (!string.IsNullOrEmpty(username))
            {
                ViewBag.username = username;
                Session["username"] = username;
            }

            return View();
        }

        public ActionResult ApplyForLeave()
        {
            ViewBag.username = Session["username"];
            return View();
        }

        public ActionResult EmployeeProfile()
        {
            ViewBag.username = Session["username"];
            return View();
        }


        [HttpGet]
        public JsonResult EmployeeDetails(string username)
        {

            
            RegisterModel register = new RegisterModel();
            EmployeeManager employeeManager = new EmployeeManager();
            register = employeeManager.EmployeeDetails(username);
            Session["emp_id"] = register.emp_id;
            Session["name"] = register.name;
            Session["gender"] = register.gender;
            Session["email"] = register.email;
            Session["mobile"] = register.mobile;
            Session["dateofbirth"] = register.dateofbirth;
            Session["address"] = register.address;
            Session["city"] = register.city;
            Session["state"] = register.state;
            //return "success";
            return Json(register, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void ApplyForLeaveDetails(LeaveModel leaveModel)
        {
            leaveModel.empid = (int)Session["emp_id"];
            leaveModel.leavestatus = "Applied";
            EmployeeManager employeeManager = new EmployeeManager();
            employeeManager.LeaveDetails(leaveModel);
        }

        [HttpGet]
        public JsonResult GetLeaveDetails()
        {
            LeaveModel leaveModel = new LeaveModel();
            leaveModel.empid = (int)Session["emp_id"];
            Session["empid1"] = leaveModel.empid;
            Session["empid2"] = leaveModel.empid;
            EmployeeManager employeeManager = new EmployeeManager();
            List<LeaveModel> leavelist = employeeManager.GetLeaveDetails(leaveModel.empid);
            //List<LeaveModel> leavelist = employeeManager.GetLeaveDetails(leaveModel.empid);
            
            return Json(leavelist, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetProfileDetails()
        {
            ProfileModel profileModel = new ProfileModel();
            //profileModel.Empid = (int)Session["empid1"];
            profileModel.Name = (string)Session["name"];
            profileModel.Gender = (string)Session["gender"];
            profileModel.Mobile = (string)Session["mobile"];
            profileModel.Email = (string)Session["email"];
            profileModel.Dateofbirth = (DateTime)Session["dateofbirth"];
            profileModel.Address = (string)Session["address"];
            profileModel.City = (string)Session["city"];
            profileModel.State = (string)Session["state"];

            return Json(profileModel, JsonRequestBehavior.AllowGet);
        }

        public void UpdateProfileDetails(ProfileModel profileModel)
        {
            profileModel.Empid = (int)Session["empid1"];
            EmployeeManager employeeManager = new EmployeeManager();
            employeeManager.UpdateProfileDetails(profileModel);
        }
        

        public void CancelLeave(LeaveModel leaveModel)
        {
            //LeaveModel leaveModel = new LeaveModel();
            leaveModel.empid = (int)Session["empid2"];
            leaveModel.leavestatus = "Cancel Applied";
            leaveModel.startdate = Convert.ToDateTime(leaveModel.startdate);
            EmployeeManager employeeManager = new EmployeeManager();
            employeeManager.CancelLeave(leaveModel);
        }
    }
}


//[HttpPost]
//public string EmployeeDetails(RegisterModel registerModel)
//{
//    //RegisterModel register = new RegisterModel();
//    EmployeeManager employeeManager = new EmployeeManager();
//    RegisterModel register = employeeManager.EmployeeDetails(registerModel);
//    return "success";
//    //return Json(register, JsonRequestBehavior.AllowGet);
//}

//DataSet dataset = employeeManager.GetLeaveDetails();
//List<LeaveModel> leave = new List<LeaveModel>();

//    if(dataTable.Rows.Count > 0)
//    {
//        foreach (DataRow dataRow in dataTable.Rows)
//        {
//            leaveModel.leavetype = dataRow["LEAVE_TYPE"].ToString();
//            leaveModel.startdate = Convert.ToDateTime(dataRow["LEAVE_START"]);
//            leaveModel.enddate = Convert.ToDateTime(dataRow["LEAVE_END"]);
//            leaveModel.leaveduration = Convert.ToInt32(dataRow["LEAVE_DAYS"]);
//            leaveModel.leavedescription = dataRow["LEAVE_DESCRIPTION"].ToString();
//            leaveModel.leavestatus = dataRow["LEAVE_STATUS"].ToString();
//            leaveModel.leavebalance = Convert.ToInt32(dataRow["LEAVE_BALANCE"]);

//            leave.Add(leaveModel);
//        }
//    }
