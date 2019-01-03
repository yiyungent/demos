using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Danmaku
{
    [HubName("danmakuHub")]
    public class DanmakuHub : Hub
    {
        /// <summary>
        /// 发送弹幕 (注意：是直接将弹幕json对象作为参数传了过来，而不是经过JSON.stringify()后的json字符串)
        /// </summary>
        /// <param name="danmakuObj">弹幕json对象</param>
        public void SendDanmaku(Model.Danmaku danmakuObj)
        {
            #region 保存弹幕到本地, 并将弹幕发送给所有客户端
            string saveFilePath = @"C:\wwwroot\api.moeci.com\danmakuList.txt";
            if (!File.Exists(saveFilePath))
            {
                File.Create(saveFilePath);
            }
            System.Web.Script.Serialization.JavaScriptSerializer javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string danmakuJsonStr = string.Empty;
            if (danmakuObj.Stime == 0)
            {
                // Stime说明没有带stime，为send发出的弹幕
                var danmakuJsonObj = new { mode = danmakuObj.Mode, text = danmakuObj.Text, size = danmakuObj.Size, color = danmakuObj.Color };
                Clients.All.sendTipMsg("-----服务端发送消息啦------");
                //Clients.All.sendDanmaku(danmakuJsonObj);
                //Clients.All.sendTipMsg(danmakuJsonObj);

                // 给自己发带边框的，给其它人发不带边框的(这种是无记录模式，其实是通过发送时调用此方法的则为发送弹幕者，以前自己发的弹幕并不会标识为自己发的（带边框）)
                Clients.Client(Context.ConnectionId).sendDanmaku(new { mode = danmakuObj.Mode, text = danmakuObj.Text, size = danmakuObj.Size, color = danmakuObj.Color, border = true });
                Clients.Others.sendDanmaku(danmakuJsonObj);

                Clients.Client(Context.ConnectionId).sendTipMsg(new { mode = danmakuObj.Mode, text = danmakuObj.Text, size = danmakuObj.Size, color = danmakuObj.Color, border = true });
                Clients.Others.sendTipMsg(danmakuJsonObj);

                danmakuJsonStr = javaScriptSerializer.Serialize(danmakuJsonObj);
            }
            else
            {
                // 有Stime说明为 insert 发出的弹幕
                var danmakuJsonObj = new { mode = danmakuObj.Mode, text = danmakuObj.Text, stime = danmakuObj.Stime, size = danmakuObj.Size, color = danmakuObj.Color };
                Clients.All.sendTipMsg("-----服务端发送消息啦------");
                //Clients.All.sendDanmaku(danmakuJsonObj);
                //Clients.All.sendTipMsg(danmakuJsonObj);

                Clients.Client(Context.ConnectionId).sendDanmaku(new { mode = danmakuObj.Mode, text = danmakuObj.Text, stime = danmakuObj.Stime, size = danmakuObj.Size, color = danmakuObj.Color, border = true });
                Clients.Others.sendDanmaku(danmakuJsonObj);

                Clients.Client(Context.ConnectionId).sendTipMsg(new { mode = danmakuObj.Mode, text = danmakuObj.Text, stime = danmakuObj.Stime, size = danmakuObj.Size, color = danmakuObj.Color, border = true });
                Clients.Others.sendTipMsg(danmakuJsonObj);

                danmakuJsonStr = javaScriptSerializer.Serialize(danmakuJsonObj);
            }
            File.AppendAllText(saveFilePath, danmakuJsonStr + "\n\r", System.Text.Encoding.UTF8);
            #endregion
        }
    }
}