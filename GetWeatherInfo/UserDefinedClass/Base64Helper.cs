using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetWeatherInfo.UserDefinedClass
{
    /// <summary>
    /// Base64帮助类
    /// </summary>
    static class Base64Helper
    {
        /// <summary>
        /// Base64编码
        /// </summary>
        /// <param name="code_type">编码类型</param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string EncodeBase64(string code, string code_type = "utf-8")
        {
            string encode = string.Empty;
            byte[] bytes = Encoding.GetEncoding(code_type).GetBytes(code);
            try
            {
                encode = Convert.ToBase64String(bytes);
            }
            catch
            {
                encode = code;
            }
            return encode;
        }

        /// <summary>
        /// Base64解码
        /// </summary>
        /// <param name="code_type">编码类型</param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string DecodeBase64(string code, string code_type = "utf-8")
        {
            string decode = string.Empty;
            byte[] bytes = Convert.FromBase64String(code);
            try
            {
                decode = Encoding.GetEncoding(code_type).GetString(bytes);
            }
            catch
            {
                decode = code;
            }
            return decode;
        }
    }
}
