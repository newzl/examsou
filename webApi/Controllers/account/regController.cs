using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace webApi.Controllers
{
    public class regController : ApiController
    {
        // GET: api/reg

        // GET: api/reg/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/reg
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/reg/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/reg/5
        public void Delete(int id)
        {
        }
    }
}
