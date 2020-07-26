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
    public class LoginWebApiController : ApiController
    {
        [Route("api/LoginWebApi/RegisterEmployee")]
        [HttpPost]
        public void RegisterEmployee(RegisterModel register)
        {
            RegisterManager registerManager = new RegisterManager();
            registerManager.ManageRegister(register);
        }
    }
}
