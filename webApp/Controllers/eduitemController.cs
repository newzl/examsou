using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace webApp.Controllers
{
    public class eduitemController : Controller
    {
        // GET: eduitem
        public ActionResult search()
        {
            return View();
        }
        public ActionResult detail() {
            return View();
        }
    }
}