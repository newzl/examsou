using System.Web.Mvc;
using Utility;

namespace webApp.Controllers
{
    [employeeAuthorize]
    public class learnpublicController : Controller
    {
        //
        // GET: /learnpublic
        /// <summary>
        /// 保存收藏 /learnpublic/savecollect
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public int saveCollect(Models.learnCollect m)
        {
            return BLL.learn.learnCollectBLL.save(m);
        }
        /// <summary>
        /// 保存笔记 /learnpublic/savenotes
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public int saveNotes(Models.learnNotes m)
        {
            BLL.learn.learnNotesBLL.save(m);
            return 1;
        }
        /// <summary>
        /// 保存错题反馈 /learnpublic/saveerrorback
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public int saveErrorBack(Models.learnErrorBack m)
        {
            m.uid = employeeLogin.eid;
            BLL.learn.learnErrorBackBLL.save(m);
            return 1;
        }
    }
}
