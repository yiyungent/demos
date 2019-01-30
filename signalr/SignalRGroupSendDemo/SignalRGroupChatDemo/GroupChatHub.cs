using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SignalRGroupChatDemo
{
    /// <summary>
    /// SignalR Hub 群聊类
    /// </summary>
    [HubName("groupChatHub")] // 标记名称供js调用
    public class GroupChatHub : Hub
    {
        /// <summary>
        /// 用户名
        /// </summary>
        private string UserName
        {
            // 注意：实测发现，对于每一个连接，一旦第一次访问了给了cookie["USERNAME"]，后面不会再次得到新的

                // 怀疑可能：因为websocket不会自动将客户端cookie发给服务端，那么只有在第一次建立会话,get访问时取到cookie，并且Context.RequestCookies也是在第一次赋值后就会一直保留值，才能导致后面ws通信，全部RequestCookies["USERNAME"]还是为第一次请求时cookie
            get
            {
                // 注意：实测字典中找不到指定的键，那么不会返回null，而是报错，所以其实下面判断null无用
                var userName = Context.RequestCookies["USERNAME"];
                return userName == null ? "" : HttpUtility.UrlDecode(userName.Value);
            }
        }

        /// <summary>
        /// 在线用户
        /// </summary>
        private static Dictionary<string, int> _onlineUser = new Dictionary<string, int>();

        /// <summary>
        /// 开始连接
        /// </summary>
        /// <returns></returns>
        public override Task OnConnected()
        {
            Connected();
            return base.OnConnected();
        }

        // 注意：刷新浏览器，属于先断开链接，再链接

        /// <summary>
        /// 重新链接
        /// </summary>
        /// <returns></returns>
        public override Task OnReconnected()
        {
            Connected();
            return base.OnReconnected();
        }

        private void Connected()
        {
            // 处理在线人员
            if (!_onlineUser.ContainsKey(UserName)) // 如果名称不存在，则是新用户
            {
                // 加入在线人员
                _onlineUser.Add(UserName, 1);

                // 向客户端发送在线人员(用户名集合)
                Clients.All.publishUser(_onlineUser.Select(i => i.Key));

                // 向客户端发送加入聊天消息
                Clients.All.publishMsg(FormatMsg("系统消息", UserName + "加入聊天"));
            }
            else
            {
                // 如果是已经存在的用户，则把在线链接的个数+1
                _onlineUser[UserName] = _onlineUser[UserName] + 1;
            }

            // 加入Hub Group，为了发送单独消息
            Groups.Add(Context.ConnectionId, "GROUP-" + UserName);
        }

        /// <summary>
        /// 断开链接
        /// </summary>
        /// <param name="stopCalled"></param>
        /// <returns></returns>
        public override Task OnDisconnected(bool stopCalled)
        {
            // 此人员链接数-1
            _onlineUser[UserName] = _onlineUser[UserName] - 1;

            // 判断此人是否断开了所有的链接
            if (_onlineUser[UserName] == 0)
            {
                // 移除此在线人员
                _onlineUser.Remove(UserName);

                // 向客户端发送在线人员
                Clients.All.publishUser(_onlineUser.Select(i => i.Key));

                // 向客户端发送退出聊天消息
                Clients.All.publishMsg(FormatMsg("系统消息", UserName + "退出聊天"));
            }

            // 移除Hub Group
            Groups.Remove(Context.ConnectionId, "GROUP-" + UserName);
            return base.OnDisconnected(stopCalled);
        }

        /// <summary>
        /// 发送消息，供客户端调用
        /// </summary>
        /// <param name="user">用户名，如果为0，则是发送给所有人</param>
        /// <param name="msg">消息</param>
        public void SendMsg(string user, string msg)
        {
            if (user == "0")
            {
                // 发送给所有用户消息
                Clients.All.publishMsg(FormatMsg(UserName, msg));
            }
            else
            {
                //// 发送给自己消息
                //Clients.Group("GROUP-" + UserName).publishMsg(FormatMsg(UserName, msg));

                //// 发送给选择的单个人员
                //Clients.Group("GROUP-" + user).publishMsg(FormatMsg(UserName, msg));

                // 发送给自己和选择的单个人员
                Clients.Groups(new List<string> { "GROUP-" + UserName, "GROUP-" + user }).publishMsg(FormatMsg(UserName, msg));
            }
        }

        /// <summary>
        /// 格式化发送的消息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private object FormatMsg(string name, string msg)
        {
            return new { Name = name, Msg = HttpUtility.HtmlEncode(msg), Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
        }
    }
}