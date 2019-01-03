using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.SignalR;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp
{
    public class DanmakuHub : Hub
    {
        // 客户端可以直接向服务端发送json对象，同时服务端也用类来接，不过类中属性或字段名 需与 接收到的 json对象中的一致
        public async Task SendDanmaku(Danmaku danmaku)
        {
            // 可以直接向客户端发送对象，将会被自动转换为json对象，不过首字母会自动转换为小写
            // { mode: 4, text: "测试发送", stime: 0, size: 40, color: 345678}
            await Clients.All.SendAsync("ReceiveDanmaku", danmaku);
        }
    }
}
