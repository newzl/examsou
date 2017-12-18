using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace webApp.Controllers
{
    public class defaultController : Controller
    {
        // GET: default
        public ActionResult index()
        {
            return View();
        }
    }
}