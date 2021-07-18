using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BiliLib.Common
{
    public class Sign
    {
        public static string ACCESS_TOKEN = "";

        /// <summary>
        /// 计算sign
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        public static Dictionary<string, string> Api(Dictionary<string, string> payload)
        {
            // iOS 6680
            string appkey = "27eb53fc9058f8c3";
            string appsecret = "c2ed53a74eeefe3cf99fbd01d8c9c375";
            // Android
            // $appkey = '1d8b6e7d45233436';
            // $appsecret = '560c52ccd288fed045859ed18bffd973';
            // 云视听 TV
            // $appkey = '4409e2ce8ffd12b8';
            // $appsecret = '59b43e04ad6965f34319062b478f83dd';

            Dictionary<string, string> defaultDic = new Dictionary<string, string>
            {
                { "access_key", ACCESS_TOKEN },
                { "actionKey", "appkey" },
                { "build", "8230" },
                { "device", "phone" },
                { "mobi_app", "iphone" },
                { "platform", "ios" },
                { "ts", DateTimeHelper.NowTimeStamp10().ToString() }
            };

            // payload 并上默认API
            foreach (var item in defaultDic)
            {
                payload.Add(item.Key, item.Value);
            }

            // 先将参数以其参数名的字典序升序进行排序
            IDictionary<string, string> sortedParams = new SortedDictionary<string, string>(payload);
            IEnumerator<KeyValuePair<string, string>> iterator = sortedParams.GetEnumerator();

            // 遍历排序后的字典，将所有参数按"key=value"格式拼接在一起
            StringBuilder basestring = new StringBuilder();
            while (iterator.MoveNext())
            {
                string key = iterator.Current.Key;
                string value = iterator.Current.Value;
                if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
                {
                    basestring.Append(key).Append("=").Append(value).Append("&");
                }
            }
            // 移除最后末尾多出的&
            basestring = basestring.Remove(basestring.Length - 1, 1);
            basestring.Append(appsecret);

            // 使用MD5对待签名串求签
            MD5 md5 = MD5.Create();
            byte[] bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(basestring.ToString()));

            // 将MD5输出的二进制结果转换为小写的十六进制
            // 应当是 32 字符十六进制数
            // TODO: 未测试
            StringBuilder sign = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                string hex = bytes[i].ToString("x");
                if (hex.Length == 1)
                {
                    sign.Append("0");
                }
                sign.Append(hex);
            }

            payload.Add("sign", sign.ToString());

            return payload;
        }
    }
}
