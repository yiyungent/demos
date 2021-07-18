using BiliLib.Common;
using BiliLib.Common;
using BiliLib.Lottery;
using BiliLib.Room;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BiliLib
{
    public class BiliClient
    {
        public CookieModel _cookie;

        public IList<string> _requestHeaders_PCWeb = new List<string>
        {
            "Content-Type: application/x-www-form-urlencoded" ,
             "User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:68.0) Gecko/20100101 Firefox/68.0"
        };

        public BiliClient(CookieModel cookie)
        {
            this._cookie = cookie;
            this._requestHeaders_PCWeb.Add($"Cookie: {_cookie.ToString()}");
        }

        public void ReceiveGift(int roomId, string link_url)
        {
            this._requestHeaders_PCWeb.Add($"Referer: {link_url}");
            this._requestHeaders_PCWeb.Add($"Origin: https://live.bilibili.com");

            EntryLiveRoom_PCWeb(roomId);
            LotteryCheck(roomId);
        }

        /// <summary>
        /// 先进入直播间
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        public bool EntryLiveRoom_PCWeb(int roomId)
        {
            bool isSuccess = false;
            string url = "https://api.live.bilibili.com/room/v1/Room/room_entry_action";
            string postData = "";
            Room_Entry_Action_RequestModel requestModel = new Room_Entry_Action_RequestModel
            {
                room_id = roomId,
                csrf = _cookie.bili_jct,
                csrf_token = _cookie.bili_jct,
            };

            string postDataStr = $"room_id={requestModel.room_id}&platform={requestModel.platform}&csrf_token={requestModel.csrf_token}&csrf={requestModel.csrf}&visit_id=";
            try
            {
                string resData = HttpAide.HttpPost(url: url, postDataStr: postDataStr, headers: _requestHeaders_PCWeb.ToArray());
                isSuccess = true;
            }
            catch (Exception ex)
            {
            }

            return isSuccess;
        }

        public void LotteryCheck(int roomId)
        {
            string url = $"https://api.live.bilibili.com/xlive/lottery-interface/v1/lottery/Check?roomid={roomId}";
            string jsonData = HttpAide.HttpGet(url: url, headers: _requestHeaders_PCWeb.ToArray());
            LotteryCheck_ResponseModel jsonModel = JsonConvert.DeserializeObject<LotteryCheck_ResponseModel>(jsonData);

            if (jsonModel != null && jsonModel.code == 0)
            {
                // 舰队
                var guards = jsonModel.data.guard;
                foreach (var guard in guards)
                {
                    new Thread(() =>
                    {
                        Join_Guard(guard.id, roomId);
                    })
                    { IsBackground = true }.Start();
                }

                // 礼物: 1.小电视飞船
                var gifts = jsonModel.data.gift;
                foreach (var gift in gifts)
                {
                    switch (gift.type)
                    {
                        case "small_tv":
                            // 小电视
                            int max_wait = gift.time - 10;
                            int try_wait = new Random().Next(gift.time_wait, max_wait);
                            new Thread(() =>
                            {
                                try
                                {
                                    Thread.Sleep(try_wait * 1000);
                                }
                                catch (Exception ex)
                                {
                                }
                                Join_SmallTv(gift.raffleId, roomId);
                            })
                            { IsBackground = true }.Start();
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public void Join_Guard(int guardId, int roomId)
        {
            string url = "https://api.live.bilibili.com/xlive/lottery-interface/v3/guard/join";
            Guard_Join_RequestModel requestModel = new Guard_Join_RequestModel
            {
                roomid = roomId,
                id = guardId,
                csrf = _cookie.bili_jct,
                csrf_token = _cookie.bili_jct,
            };
            string postDataStr = $"id={requestModel.id}&roomid={requestModel.roomid}&type=guard&csrf_token={requestModel.csrf_token}&csrf={requestModel.csrf}A&visit_id=";
            try
            {
                string jsonData = HttpAide.HttpPost(url: url, postDataStr: postDataStr, headers: _requestHeaders_PCWeb.ToArray());
                Guard_Join_ResponseModel jsonModel = JsonConvert.DeserializeObject<Guard_Join_ResponseModel>(jsonData);

                if (jsonModel.code == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    int day = (int)(DateTimeHelper.ToDateTime10(jsonModel.data.award_ex_time) - DateTime.Now).TotalDays + 1;
                    Console.WriteLine($"{DateTime.Now.ToString()}--在房间 {roomId} 获奖--来自 舰队: {jsonModel.data.award_name} × {jsonModel.data.award_num} 有效时间: {day}天");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"{DateTime.Now.ToString()}--在房间 {roomId} --来自 舰队: {jsonModel.message}");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void Join_SmallTv(int smallTvId, int roomId)
        {
            string url = "https://api.live.bilibili.com/xlive/lottery-interface/v3/guard/join";
            SmallTv_Join_RequestModel requestModel = new SmallTv_Join_RequestModel
            {
                roomid = roomId,
                id = smallTvId,
                csrf = _cookie.bili_jct,
                csrf_token = _cookie.bili_jct,
            };
            string postDataStr = $"id={requestModel.id}&roomid={requestModel.roomid}&type={requestModel.type}&csrf_token={requestModel.csrf_token}&csrf={requestModel.csrf}A&visit_id=";
            try
            {
                string jsonData = HttpAide.HttpPost(url: url, postDataStr: postDataStr, headers: _requestHeaders_PCWeb.ToArray());
                SmallTv_Join_ResponseModel jsonModel = JsonConvert.DeserializeObject<SmallTv_Join_ResponseModel>(jsonData);

                if (jsonModel.code == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    int day = (int)(DateTimeHelper.ToDateTime10(jsonModel.data.award_ex_time) - DateTime.Now).TotalDays + 1;
                    Console.WriteLine($"{DateTime.Now.ToString()}--在房间 {roomId} 获奖--来自 小电视飞船: {jsonModel.data.award_name} × {jsonModel.data.award_num} 有效时间: {day}天");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"{DateTime.Now.ToString()}--在房间 {roomId} --来自 小电视: {jsonModel.message}");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            catch (Exception ex)
            {
            }
        }


    }
}
