using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 加密辅助类
    /// </summary>
    public static class DanceEncryptHelper
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="txt">文本</param>
        /// <returns>MD5加密后的字符串</returns>
        public static string MD5(string txt)
        {
            using var md5Hash = System.Security.Cryptography.MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(txt));

            StringBuilder builder = new();
            for (int i = 0; i < data.Length; i++)
            {
                builder.Append(data[i].ToString("x2"));
            }

            return builder.ToString();
        }
    }
}
