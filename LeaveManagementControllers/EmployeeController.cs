using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LeaveManagementModels;
using LeaveManagementManager;
using System.Data;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace Leave_Management.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Dashboard
        [Filters.AuthorizeUser]
        public ActionResult Dashboard(string username)
        {
            if (!string.IsNullOrEmpty(username))
            {
                ViewBag.username = username;
                Session["username"] = username;
            }

            return View();
        }

        [Filters.AuthorizeUser]
        public ActionResult ApplyForLeave()
        {
            ViewBag.username = Session["username"];
            return View();
        }

        [Filters.AuthorizeUser]
        public ActionResult EmployeeProfile()
        {
            ViewBag.username = Session["username"];
            return View();
        }

        [HttpGet]
        public JsonResult GetMasterData()
        {
            //MasterData masterData = new MasterData();
            EmployeeManager employeeManager = new EmployeeManager();
            List<MasterData> masterlist = employeeManager.GetMasterData();
            return Json(masterlist, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult EmployeeDetails(string username)
        {
            string url = "http://localhost:50404/api/EmployeeWebApi/EmployeeDetails?username=" + username;
            var request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "Get";
            request.ContentType = "application/json";

            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            RegisterModel register = JsonConvert.DeserializeObject<RegisterModel>(responseString);

            //Session["emp_id"] = register.emp_id;
            //Session["name"] = register.name;
            //Session["gender"] = register.gender;
            //Session["email"] = register.email;
            //Session["mobile"] = register.mobile;
            //Session["dateofbirth"] = register.dateofbirth;
            //Session["address"] = register.address;
            //Session["city"] = register.city;
            //Session["state"] = register.state;

            Session["register"] = register;
            
            return Json(register, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void ApplyForLeaveDetails(LeaveModel leaveModel)
        {
            var leave = (RegisterModel)Session["register"];
            leaveModel.empid = leave.emp_id;
            leaveModel.name = leave.name;
            leaveModel.created_datetime = DateTime.Now;
            leaveModel.leavestatus = "Applied";

            var request = (HttpWebRequest)WebRequest.Create("http://localhost:50404/api/EmployeeWebApi/ApplyForLeaveDetails");
            request.Method = "Post";
            request.ContentType = "application/json";

            string result = JsonConvert.SerializeObject(leaveModel);

            var streamWriter = new StreamWriter(request.GetRequestStream());
            streamWriter.Write(result);
            streamWriter.Close();

            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            
        }

        [HttpGet]
        public JsonResult GetLeaveDetails()
        {
            LeaveModel leaveModel = new LeaveModel();
            var leave = (RegisterModel)Session["register"];
            leaveModel.empid = leave.emp_id;
            Session["empid1"] = leaveModel.empid;
            Session["empid2"] = leaveModel.empid;

            var request = (HttpWebRequest)WebRequest.Create("http://localhost:50404/api/EmployeeWebApi/GetLeaveDetails?empid=" + leaveModel.empid);
            request.Method = "Get";
            request.ContentType = "application/json";
            
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            List<LeaveModel> leavelist = JsonConvert.DeserializeObject<List<LeaveModel>>(responseString);

            //List<LeaveModel> leavelist = employeeManager.GetLeaveDetails(leaveModel.empid);
            
            return Json(leavelist, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetProfileDetails()
        {
            ProfileModel profileModel = new ProfileModel();
            var profile = (RegisterModel)Session["register"];

            profileModel.emp_id = profile.emp_id;
            profileModel.name = profile.name;
            profileModel.gender = profile.gender;
            profileModel.mobile = profile.mobile;
            profileModel.email = profile.email;
            profileModel.dateofbirth = profile.dateofbirth;
            profileModel.address = profile.address;
            profileModel.city = profile.city;
            profileModel.state = profile.state;
            
            return Json(profileModel, JsonRequestBehavior.AllowGet);
        }

        public void UpdateProfileDetails(ProfileModel profileModel)
        {
            profileModel.emp_id = (int)Session["empid1"];
            profileModel.updated_datetime = DateTime.Now;
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
