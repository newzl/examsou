using Models;
using Newtonsoft.Json;
using System;
using System.Web.Mvc;
using Utility;

namespace webApp.Areas.admin.Controllers
{
    [adminAuthorize]
    public class exammsgController : Controller
    {
        //
        // GET: /admin/exammsg/

        public ActionResult index()
        {
            return View();
        }
    }
}
