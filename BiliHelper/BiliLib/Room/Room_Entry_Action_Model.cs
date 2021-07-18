using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliLib.Room
{
    /// <summary>
    /// 
    /// 不一定要 get https://live.bilibili.com/1540114?live_lottery_type=1&broadcast_type=0&visit_id=3g5vqoumg0cg
    /// 而可以 post https://api.live.bilibili.com/room/v1/Room/room_entry_action
    /// 来表达进入直播间
    /// 
    /// 响应json结果
    /// {
    ///  "code": 0,
    ///  "msg": "ok",
    ///  "message": "ok",
    ///  "data": []
    ///}
    /// </summary>
    public class Room_Entry_Action_ResponseModel
    {
        public int code { get; set; }
        public string msg { get; set; }
        public string message { get; set; }
        public object[] data { get; set; }
    }

    /// <summary>
    /// post 表单数据
    /// 
    /// room_id=21663729&platform=pc&csrf_token=4d22bb43d01f827eace7d5ef3acc2d5e&csrf=4d22bb43d01f827eace7d5ef3acc2d5e&visit_id=
    /// </summary>
    public class Room_Entry_Action_RequestModel
    {
        public int room_id { get; set; }

        /// <summary>
        /// pc
        /// </summary>
        public string platform { get; set; } = "pc";

        /// <summary>
        /// 发现 csrf_token 和 csrf 值一致，又和 cookie 中的 bili_jct 值相同
        /// </summary>
        public string csrf_token { get; set; }

        public string csrf { get; set; }

        /// <summary>
        /// 为空字符串
        /// </summary>
        public string visit_id { get; set; }
    }

}
