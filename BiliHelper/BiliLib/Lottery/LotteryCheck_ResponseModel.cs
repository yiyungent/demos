using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliLib.Lottery
{
    /// <summary>
    /// 进入抽奖直播间 get https://live.bilibili.com/1540114?live_lottery_type=1&broadcast_type=0&visit_id=3g5vqoumg0cg
    /// 后 get https://api.live.bilibili.com/xlive/lottery-interface/v1/lottery/Check?roomid=1540114
    /// 响应json结果
    /// 
    /// 此直播间有哪些礼物
    /// 
    /// {\"code\":0,\"message\":\"0\",\"ttl\":1,\"data\":{\"pk\":[],\"guard\":[{\"id\":1814006,\"sender\":{\"uid\":7245015,\"uname\":\"瞎改名改到没硬币了哇\",\"face\":\"http://i1.hdslb.com/bfs/face/b33843c25dbfee8c30fe85b8e8b8071652c40441.jpg\"},\"keyword\":\"guard\",\"privilege_type\":3,\"time\":718,\"status\":1,\"time_wait\":0,\"asset_icon\":\"https://i0.hdslb.com/bfs/vc/43f488e7c4dca5ba6fbdcb88f40052d56bf777d8.png\",\"asset_animation_pic\":\"https://i0.hdslb.com/bfs/vc/ff2a28492970850ce73df0cc144f1766b222d471.gif\",\"thank_text\":\"恭喜\\u003c%瞎改名改到没硬币了哇%\\u003e上任舰长\",\"weight\":0}],\"gift\":[{\"raffleId\":626069,\"type\":\"small_tv\",\"from_user\":{\"uid\":0,\"uname\":\"老年言\",\"face\":\"http://i1.hdslb.com/bfs/face/434133a7130f514c51130376726caa02aec5513b.jpg\"},\"time_wait\":-49,\"time\":11,\"max_time\":180,\"status\":1,\"sender_type\":0,\"asset_icon\":\"http://s1.hdslb.com/bfs/live/fae67831221cfc2d0576ff0201d3609b4671bcdb.png\",\"asset_animation_pic\":\"http://i0.hdslb.com/bfs/live/746a8db0702740ec63106581825667ae525bb11a.gif\",\"thank_text\":\"感谢\\u003c%老年言%\\u003e 赠送的小电视飞船\",\"weight\":0,\"gift_id\":25},{\"raffleId\":626070,\"type\":\"small_tv\",\"from_user\":{\"uid\":0,\"uname\":\"Owl树上猫\",\"face\":\"http://i2.hdslb.com/bfs/face/5a14adadb6507fbd2e5426b9087b185322d75524.jpg\"},\"time_wait\":30,\"time\":90,\"max_time\":180,\"status\":1,\"sender_type\":0,\"asset_icon\":\"http://s1.hdslb.com/bfs/live/fae67831221cfc2d0576ff0201d3609b4671bcdb.png\",\"asset_animation_pic\":\"http://i0.hdslb.com/bfs/live/746a8db0702740ec63106581825667ae525bb11a.gif\",\"thank_text\":\"感谢\\u003c%Owl树上猫%\\u003e 赠送的小电视飞船\",\"weight\":0,\"gift_id\":25}]}}
    /// </summary>
    public class LotteryCheck_ResponseModel
    {
        public int code { get; set; }
        public string message { get; set; }
        public int ttl { get; set; }
        public DataModel data { get; set; }


        public sealed class DataModel
        {
            public object[] pk { get; set; }
            public GuardModel[] guard { get; set; }
            public GiftModel[] gift { get; set; }
        }

        public sealed class GuardModel
        {
            public int id { get; set; }
            public SenderModel sender { get; set; }
            public string keyword { get; set; }
            public int privilege_type { get; set; }
            public int time { get; set; }
            public int status { get; set; }
            public int time_wait { get; set; }
            public string asset_icon { get; set; }
            public string asset_animation_pic { get; set; }
            public string thank_text { get; set; }
            public int weight { get; set; }
        }

        public sealed class SenderModel
        {
            public int uid { get; set; }
            public string uname { get; set; }
            public string face { get; set; }
        }

        public sealed class GiftModel
        {
            public int raffleId { get; set; }
            public string type { get; set; }
            public From_UserModel from_user { get; set; }
            public int time_wait { get; set; }
            public int time { get; set; }
            public int max_time { get; set; }
            public int status { get; set; }
            public int sender_type { get; set; }
            public string asset_icon { get; set; }
            public string asset_animation_pic { get; set; }
            public string thank_text { get; set; }
            public int weight { get; set; }
            public int gift_id { get; set; }
        }

        public sealed class From_UserModel
        {
            public int uid { get; set; }
            public string uname { get; set; }
            public string face { get; set; }
        }
    }

}
