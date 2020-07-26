using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using LeaveManagementModels;
using LeaveManagementManager;


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

        [Route("Register")]
        public ActionResult Register()
        {
            return View();
        }

        //[HttpPost] 
        //[Route("RegisterEmp")]
        //public void RegisterEmp(string name)
        // {
        //     RegisterManager registerManager = new RegisterManager();
        //     registerManager.ManageRegister(name);
        // }

        [HttpPost]      
        public void RegisterEmployee(RegisterModel register)
        {
            RegisterManager registerManager = new RegisterManager();
            registerManager.ManageRegister(register);
        }

        [HttpPost]
        public JsonResult LoginEmployee(LoginModel login)
        {
            LoginManager loginManager = new LoginManager();
            loginManager.ManageLogin(login);
            string message = loginManager.ManageLogin(login);

            //if(message == "Login success")
            //{
            //    RedirectToAction("Dashboard","Dashboard");
            //}
            //else
            //{
            //    ViewBag.ErrorMsg = "Wrong username or password";
            //    ViewBag{ "ErrorMsg"}
            //    //return View("Login");
            //}
            return Json(message, JsonRequestBehavior.AllowGet);


        }

        public void ForgetPassword(RegisterModel registerModel)
        {
            string email = registerModel.email;
            RegisterManager registerManager = new RegisterManager();
            registerManager.ForgetPassword(email);
        }
    }
}
