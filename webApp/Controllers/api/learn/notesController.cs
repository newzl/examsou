using System.Web.Http;

namespace webApp.Controllers.api.learn
{
    /// <summary>
    /// 试题笔记
    /// </summary>
    public class notesController : ApiController
    {
        ///
        // GET: api/notes
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}
        // 保存笔记
        // POST: api/notes
        public IHttpActionResult Post(Models.learnNotes m)
        {
            new BLL.learn.learnNotesBLL().save(m);
            return Ok(1);
        }
    }
}