using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace webApi.Controllers.profile
{
    public class coreController : ApiController
    {
        // GET: api/core
        [HttpGet]
        public HttpResponseMessage Get(string eid)
        {
            return new HttpResponseMessage
            {
                Content = new StringContent(
                    BLL.account.employeeBLL.profileCore(eid),
                    Encoding.GetEncoding("UTF-8"), "application/json")
            };
        }
    }
}
