using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Threading.Tasks;
using Utility;

namespace webApp.Hubs
{
    [HubName("learnService")]
    public class learnHub : Hub
    {
        //Context.ConnectionId:客服端id

        #region 重写Hub连接事件
        /// 连接
        /// </summary>
        /// <returns></returns>
        //public override System.Threading.Tasks.Task OnConnected()
        //{
        //    Groups.Add(Context.ConnectionId, Context.User.Identity.Name);
        //    return base.OnConnected();
        //}
        /// <summary>
        /// 重新连接
        /// </summary>
        /// <returns></returns>
        public override Task OnReconnected()
        {
            string eid = Context.RequestCookies[globalValue.COOKIE_EMPLOYEE].Value;
            Clients.Group(eid).learnStop();//  learnStop：通知eid组中的其他用户停止学习
            Groups.Add(Context.ConnectionId, eid);// 添加本客服端id到eid组中
            return base.OnReconnected();
        }
        /// <summary>
        /// 断开连接
        /// </summary>
        /// <param name="stopCalled"></param>
        /// <returns></returns>
        public override Task OnDisconnected(bool stopCalled)
        {
            Groups.Remove(Context.ConnectionId, Context.RequestCookies[globalValue.COOKIE_EMPLOYEE].Value);
            return base.OnDisconnected(stopCalled);
        } 
        #endregion

        /// <summary>
        /// 开始eid组的学习
        /// </summary>
        /// <param name="eid"></param>
        public void learnStart()
        {
            string eid = Context.RequestCookies[globalValue.COOKIE_EMPLOYEE].Value;
            Clients.Group(eid).learnStop();//  learnStop：通知eid组中的其他用户停止学习
            Groups.Add(Context.ConnectionId, eid);// 添加本客服端id到eid组中
        }
        /// <summary>
        /// 将eid组的客服端id用户删除
        /// </summary>
        /// <param name="eid"></param>
        public void learnRemove()
        {
            Groups.Remove(Context.ConnectionId, Context.RequestCookies[globalValue.COOKIE_EMPLOYEE].Value);
        }
    }
}