using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliLib.Lottery
{
    /// <summary>
    /// post https://api.live.bilibili.com/xlive/lottery-interface/v3/guard/join
    /// 
    /// id=1816390&roomid=5050&type=guard&csrf_token=4d22bb43d01f827eace7d5ef3acc2d5e&csrf=4d22bb43d01f827eace7d5ef3acc2d5e&visit_id=
    /// </summary>
    public class Guard_Join_RequestModel
    {
        public int id { get; set; }

        public int roomid { get; set; }

        public string type { get; set; } = "guard";

        public string csrf_token { get; set; }

        public string csrf { get; set; }

        public string visit_id { get; set; } = "";
    }

    /// <summary>
    /// {
    ///  "code": 0,
    ///  "data": {
    ///    "id": 1816390,
    ///    "award_id": 1,
    ///    "award_type": 0,
    ///    "award_num": 1,
    ///    "award_image": "http://i0.hdslb.com/bfs/live/d57afb7c5596359970eb430655c6aef501a268ab.png",
    ///    "award_name": "辣条",
    ///    "award_text": "",
    ///    "award_ex_time": 1578326400
    ///  },
    ///  "message": "ok",
    ///  "msg": "ok"
    ///}
    /// </summary>
    public class Guard_Join_ResponseModel
    {
        public int code { get; set; }
        public DataModel data { get; set; }
        public string message { get; set; }
        public string msg { get; set; }

        public class DataModel
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
}
