using Models;
using System.Collections.Generic;
using System.Web.Http;

namespace webApi.Controllers
{
    public class loginController : ApiController
    {
        // POST: api/login
        [HttpGet]
        public IHttpActionResult Get(string account, string pwd)
        {
            employeeLoginInfo employee = BLL.account.employeeBLL.login(account, Common.Encrypt.md5(pwd));
            if (employee != null)
            {
                apiLogin m = new apiLogin()
                {
                    eid = employee.eid,
                    name = employee.name
                    //state = employee.state
                };
                return Json<apiLogin>(m);
            }
            else
            {
                apiLogin m = new apiLogin()
                {
                    state = 0
                };
                return Json<apiLogin>(m);
            }
        }
    }
    public class apiLogin
    {
        public string eid { get; set; }
        public string name { get; set; }
        public int state { get; set; }
    }
}
