using BiliLib.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliLib.Live
{
    public class User_RecommendModel
    {
        public int code { get; set; }
        public string msg { get; set; }
        public string message { get; set; }
        public DatumModel[] data { get; set; }

        public sealed class DatumModel
        {
            public int area { get; set; }
            public string areaName { get; set; }
            public string face { get; set; }
            public int is_bn { get; set; }
            public int is_tv { get; set; }
            public string link { get; set; }
            public int online { get; set; }
            public int roomid { get; set; }
            public int short_id { get; set; }
            public int stream_id { get; set; }
            public string system_cover { get; set; }
            public string title { get; set; }
            public int uid { get; set; }
            public string uname { get; set; }
            public string user_cover { get; set; }
        }

        //public User_RecommendModel(int page)
        //{

        //}

        public static User_RecommendModel Get(int page)
        {
            User_RecommendModel rtnModel = new User_RecommendModel();
            string url = $"https://api.live.bilibili.com/room/v1/room/get_user_recommend?page={page}";
            string[] headers = {
                "User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:68.0) Gecko/20100101 Firefox/68.0",
                "Referer: https://live.bilibili.com/all?visit_id=b697kwgsxi80",
                "Origin: https://live.bilibili.com"
            };
            string jsonData = HttpAide.HttpGet(url: url, headers: headers);
            try
            {
                User_RecommendModel model = JsonConvert.DeserializeObject<User_RecommendModel>(jsonData);
                if (model.code == 0)
                {
                    rtnModel.code = model.code;
                    rtnModel.msg = model.msg;
                    rtnModel.message = model.message;
                    rtnModel.data = model.data;
                }
            }
            catch (Exception ex)
            {
                rtnModel.code = -1;
            }

            return rtnModel;
        }
    }
}
