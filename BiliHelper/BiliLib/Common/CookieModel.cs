using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliLib.Common
{
    /// <summary>
    /// "cookies": [
    ///  {
    ///    "name": "LIVE_BUVID",
    ///    "value": "AUTO5615777116953777"
    ///  },
    ///  {
    ///    "name": "Hm_lvt_8a6e55dbd2870f0f5bc9194cddf32a02",
    ///    "value": "1577711696"
    ///  },
    ///  {
    ///    "name": "_uuid",
    ///    "value": "A047CF6A-9C22-6D17-1C6D-040EEFF8AE3B96314infoc"
    ///  },
    ///  {
    ///    "name": "buvid3",
    ///    "value": "1484E9EE-1DF6-499C-9E35-52B94F533AEC155836infoc"
    ///  },
    ///  {
    ///    "name": "sid",
    ///    "value": "l6ypnxkz"
    ///  },
    ///  {
    ///    "name": "DedeUserID",
    ///    "value": "25057459"
    ///  },
    ///  {
    ///    "name": "DedeUserID__ckMd5",
    ///    "value": "045950e7d663368e"
    ///  },
    ///  {
    ///    "name": "SESSDATA",
    ///    "value": "eeae388b,1580317709,d22807c1"
    ///  },
    ///  {
    ///    "name": "bili_jct",
    ///    "value": "4d22bb43d01f827eace7d5ef3acc2d5e"
    ///  },
    ///  {
    ///    "name": "bp_t_offset_25057459",
    ///    "value": "338733547970763897"
    ///  },
    ///  {
    ///    "name": "im_notify_type_25057459",
    ///    "value": "0"
    ///  },
    ///  {
    ///    "name": "Hm_lpvt_8a6e55dbd2870f0f5bc9194cddf32a02",
    ///    "value": "1577725722"
    ///  }
    ///]
    /// </summary>
    public class CookieModel
    {
        public string LIVE_BUVID { get; set; }

        public string _uuid { get; set; }

        public string buvid3 { get; set; }

        public string sid { get; set; }

        public string DedeUserID { get; set; }

        public string DedeUserID__ckMd5 { get; set; }

        public string SESSDATA { get; set; }

        public string bili_jct { get; set; }

        public CookieModel(string cookieStr)
        {
            // LIVE_BUVID=AUTO5615777116953777; Hm_lvt_8a6e55dbd2870f0f5bc9194cddf32a02=1577711696; _uuid=A047CF6A-9C22-6D17-1C6D-040EEFF8AE3B96314infoc; buvid3=1484E9EE-1DF6-499C-9E35-52B94F533AEC155836infoc; sid=l6ypnxkz; DedeUserID=25057459; DedeUserID__ckMd5=045950e7d663368e; SESSDATA=eeae388b%2C1580317709%2Cd22807c1; bili_jct=4d22bb43d01f827eace7d5ef3acc2d5e; bp_t_offset_25057459=338733547970763897; im_notify_type_25057459=0; Hm_lpvt_8a6e55dbd2870f0f5bc9194cddf32a02=1577725722
            this._cookieStr = cookieStr;
            string[] cookies = cookieStr.Split(new string[] { "; " }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var item in cookies)
            {
                string[] keyAndVal = item.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                string key = keyAndVal[0];
                string val = keyAndVal[1];
                if (key == "LIVE_BUVID")
                {
                    this.LIVE_BUVID = val;
                }
                else if (key == "_uuid")
                {
                    this._uuid = val;
                }
                else if (key == "buvid3")
                {
                    this.buvid3 = val;
                }
                else if (key == "sid")
                {
                    this.sid = val;
                }
                else if (key == "DedeUserID")
                {
                    this.DedeUserID = val;
                }
                else if (key == "DedeUserID__ckMd5")
                {
                    this.DedeUserID__ckMd5 = val;
                }
                else if (key == "SESSDATA")
                {
                    this.SESSDATA = val;
                }
                else if (key == "bili_jct")
                {
                    this.bili_jct = val;
                }
            }
        }

        private string _cookieStr;

        public override string ToString()
        {
            return this._cookieStr;
        }
    }
}
