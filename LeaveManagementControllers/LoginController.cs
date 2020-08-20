using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using LeaveManagementModels;
using LeaveManagementManager;
using System.Net;
using Newtonsoft.Json;
using System.IO;

namespace Leave_Management.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult AboutUs()
        {
            return View();
        }

        
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]      
        public void RegisterEmployee(RegisterModel register)
        {
            register.created_datetime = DateTime.Now;
            var request = (HttpWebRequest)WebRequest.Create("http://localhost:50404/api/LoginWebApi/RegisterEmployee");
            request.Method = "Post";
            request.ContentType = "application/json";

            string result = JsonConvert.SerializeObject(register);

            var streamWriter = new StreamWriter(request.GetRequestStream());
            streamWriter.Write(result);
            streamWriter.Close();

            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

        }

        [HttpPost]
        public JsonResult LoginEmployee(LoginModel login)
        {
            LoginManager loginManager = new LoginManager();
            loginManager.ManageLogin(login);
            string message = loginManager.ManageLogin(login);
            Session["usertype"] = login.usertype;

            return Json(message, JsonRequestBehavior.AllowGet);
        }

        public void ForgetPassword(RegisterModel registerModel)
        {
            string email = registerModel.email;
            RegisterManager registerManager = new RegisterManager();
            registerManager.ForgetPassword(email);
        }

        public ActionResult Logout()
        {
            Session["usertype"] = null;
            return RedirectToAction("Login");
        }
    }
}
