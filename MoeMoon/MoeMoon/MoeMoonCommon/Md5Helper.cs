using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace MoeMoonCommon
{
    public partial class Md5Helper
    {
        public static string EncryptString(string source)
        {
            MD5 md5 = MD5.Create();
            byte[] tempByte = Encoding.UTF8.GetBytes(source);
            byte[] encryptByte = md5.ComputeHash(tempByte);
            StringBuilder sb = new StringBuilder();
            foreach (byte b in encryptByte)
            {
                sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }
    }
}
