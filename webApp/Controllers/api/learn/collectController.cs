using System.Web.Http;

namespace webApp.Controllers.api.learn
{
    /// <summary>
    /// 收藏试题API
    /// </summary>
    public class collectController : ApiController
    {
        // GET: api/collect
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}
        // 保存收藏
        // POST: api/collect
        public IHttpActionResult Post(Models.learnCollect m)
        {
            return Ok(new BLL.learn.learnCollectBLL().save(m));//返回保存的ID，cid=0时取消收藏，返回影响行数
        }
    }
}