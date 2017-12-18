using System.Web.Mvc;

namespace webApp.Areas.root.Controllers
{
    public class subClassController : Controller
    {
        //
        // GET: /root/subClass/

        public ActionResult tree()
        {
            return View();
        }
        //  /root/subclass/save
        [HttpPost]
        public int save(Models.subject m)
        {
            BLL.subjectClassBLL.save(m);
            return 1;
        }
        [HttpPost]
        public int setvalit(bool valid, int id)
        {
            BLL.subjectClassBLL.setValid(valid, id);
            return 1;
        }
        [HttpPost]
        public int delete(int id)
        {
            BLL.subjectClassBLL.delete(id);
            return 1;
        }
        [HttpGet]
        public JsonResult getentity(int id)
        {
            return Json(BLL.subjectClassBLL.getEntity(id), JsonRequestBehavior.AllowGet);
        }
    }
}
