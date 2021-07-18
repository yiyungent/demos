using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliLib.Lottery
{
    /// <summary>
    /// 进入抽奖直播间 get https://live.bilibili.com/1540114?live_lottery_type=1&broadcast_type=0&visit_id=3g5vqoumg0cg
    /// 后 post https://api.live.bilibili.com/xlive/lottery-interface/v5/smalltv/join
    /// 响应json结果
    /// 
    /// 抽奖结果：获得了什么奖品（辣条）
    /// 
    /// {
    ///  "code": 0,
    ///  "data": {
    ///    "id": 626069,
    ///    "award_id": 1,
    ///    "award_type": 0,
    ///    "award_num": 5,
    ///    "award_image": "http://i0.hdslb.com/bfs/live/da6656add2b14a93ed9eb55de55d0fd19f0fc7f6.png",
    ///    "award_name": "辣条",
    ///    "award_text": "",
    ///    "award_ex_time": 1580313600
    ///  },
    ///  "message": "",
    ///  "msg": ""
    ///}
    /// </summary>
    public class SmallTv_Join_ResponseModel
    {
        public int code { get; set; }
        public DataModel data { get; set; }
        public string message { get; set; }
        public string msg { get; set; }

        public sealed class DataModel
        {
            public int id { get; set; }
            public int award_id { get; set; }
            public int award_type { get; set; }
            public int award_num { get; set; }
            public string award_image { get; set; }
            public string award_name { get; set; }
            public string award_text { get; set; }
            public int award_ex_time { get; set; }
        }
    }

    /// <summary>
    /// post 的表单数据
    /// id=626069&roomid=1540114&type=small_tv&csrf_token=4d22bb43d01f827eace7d5ef3acc2d5e&csrf=4d22bb43d01f827eace7d5ef3acc2d5e&visit_id=
    /// 
    /// 
    /// PS: 这里的id发现其实就是 gift 数组中每个元素的 raffleId
    /// </summary>
    public class SmallTv_Join_RequestModel
    {
        /// <summary>
        /// 请求的id和响应的id 相同
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 一定是此礼物发送的直播间
        /// </summary>
        public int roomid { get; set; }

        /// <summary>
        /// small_tv
        /// GIFT_30405
        /// </summary>
        public string type { get; set; } = "small_tv";

        public string csrf_token { get; set; }

        public string csrf { get; set; }

        /// <summary>
        /// 几次测试这个都为空字符串
        /// </summary>
        public string visit_id { get; set; }
    }
}
