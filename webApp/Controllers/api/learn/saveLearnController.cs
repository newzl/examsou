using System.Web.Http;

namespace webApp.Controllers.api.learn
{
    public class saveLearnController : ApiController
    {
        // POST: api/savelearn
        public IHttpActionResult Post(Models.saveLearn m)
        {
            new BLL.learn.learnsxBLL().exeSaveLearn(m);
            return Ok(1);
        }
    }
}
