using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace webApp.Controllers
{
    public class courseController : Controller
    {
        // GET: course
        public ActionResult index()
        {
            return View();
        }
    }
}