using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;

namespace DanmuDemoOne
{
    [HubName("danmuHub")]
    public class DanmuHub : Hub
    {
        #region 方法1：接收json字符串，再将json字符串 转化为 对象
        //public void SendDanmu(string jsonDanmu)
        //{
        //    // 难以置信：尝试一下，居然这样就成功了
        //    // 如果类中的属性名和json字符串不匹配，那么还行吗
        //    // 实测发现，如果类中属性与 json中属性名对不上，那么对不上的就没有值，也就是说是按名字匹配的
        //    object obj = JsonConvert.DeserializeObject(jsonDanmu, typeof(Models.Danmu));
        //    Models.Danmu danmu = (Models.Danmu)obj;
        //    Clients.All.tipMsg(JsonConvert.SerializeObject(danmu));
        //}
        #endregion

        #region 方法2：接收json弹幕对象
        // (PS:但发现供客户端调用的服务端方法不能重载，否则一个方法都无法调用)
        // 实测发现，此方法，必须要客户端传过来的是json对象，而不能是json字符串，否则无法调用此方法，
        // 如果传过来的是json字符串，则使用方法1
        public void SendDanmu(Models.Danmu danmu)
        {
            // 返回json字符串
            //Clients.All.tipMsg(JsonConvert.SerializeObject(danmu));

            // 可以直接返回给客户端对象，不过注意,结果为{Mode: 1, Text: "测试发送", Stime: 8701, Size: 40, Color: 255}  因为是.Net中的类中属性，所以变首字母变大写了，而json是区分大小写的
            //Clients.All.tipMsg(danmu);

            // 匿名类来重建 对象 ，实现名字小写
            var danmuJsonObj = new { mode = danmu.Mode, text = danmu.Text, stime = danmu.Stime, size = danmu.Size, color = danmu.Color };
            Clients.All.tipMsg("------------服务端开始发消息啦--------");
            Clients.All.tipMsg(danmuJsonObj);
            Clients.All.publicDanmu(danmuJsonObj);
        }
        #endregion

    }
}