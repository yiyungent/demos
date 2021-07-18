using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQCalG_tk
{
    class Program
    {
        static void Main(string[] args)
        {
            string p_skey = "Fk3348E3NIkpp1Uw*qGGPpA-UhXtft0MfIs8kKSXpU0_";
            Console.WriteLine(Get_gtk(p_skey));
            Console.WriteLine(GetBkn(p_skey));
            Console.ReadKey();
        }

        /// <summary>
        /// 根据QQ空间Cookie["p_skey"] 计算出g_tk
        /// </summary>
        /// <param name="p_skey"></param>
        /// <returns></returns>
        public static long Get_gtk(string p_skey)
        {
            long hash = 5381;
            for (int i = 0; i < p_skey.Length; i++)
            {
                hash += (hash << 5) + p_skey[i];
            }
            return hash & 0x7fffffff;
        }

        public static long GetBkn(string skey)
        {
            var hash = 5381;
            for (int i = 0, len = skey.Length; i < len; ++i)
            {
                hash += (hash << 5) + (int)skey[i];
            }
            return hash & 2147483647;
        }
    }
}
