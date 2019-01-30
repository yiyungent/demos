using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRGroupChatDemo
{
    // 在GroupChatHub类中的方法就是提供给客户端调用的js方法
    // 特性HubName进行重命名
    [HubName("simpleHub")]
    public class SimpleHub : Hub
    {
        public void SendMsg(string msg)
        {
            #region 客户端=>服务端
            // 获取链接id
            string connectionId = Context.ConnectionId;
            // 获取cookie
            IDictionary<string, Cookie> cookies = Context.RequestCookies;
            #endregion

            #region 服务端=>客户端
            // 所有人
            Clients.All.msg("发送给客户端的消息");
            // 特定 connectionId
            Clients.Client("connectionId").msg("发送给客户端的消息");
            // 特定 group
            Clients.Group("groupName").msg("发送给客户端的消息");
            #endregion
        }
    }
}