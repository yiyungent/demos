using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace SignalRApp
{
    public class MyHub : Hub
    {
        #region 加入组
        public void AddGroup(string groupName)
        {
            // 将此次连接的客户端（浏览器端）加入指定的组中
            Groups.Add(this.Context.ConnectionId, groupName);
        }
        #endregion

        #region 向指定的组发送消息
        public void SendGroupMsg(string groupName, string msg)
        {
            // 注意：当前客户端的连接ID在不在此组与能不能给此组发送消息无关（既可以给任意组发），客户端连接ID在此组，只能说明它接收给此组的消息
            // 向此组中（除了我之外的成员）发送消息（这个我是指此次连接上Hub的客户端）
            Clients.OthersInGroup(groupName).OnMessage(msg);
        }
        #endregion

        #region 重写父类OnConnected方法：客户端连接上的时候会调用此方法
        public override Task OnConnected()
        {
            return base.OnConnected();
        }
        #endregion

        #region 重写父类OnReconnected方法：客户端可能网络不稳定，造成重新连接，就会调用此方法
        // 注意：客户端（浏览器页面）在刷新的时候不属于重连接，刷新属于先断开连接，然后再连接
        public override Task OnReconnected()
        {
            return base.OnReconnected();
        }
        #endregion
    }
}