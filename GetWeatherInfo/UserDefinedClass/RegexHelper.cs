using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GetWeatherInfo.UserDefinedClass
{
    static class RegexHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string Func(string content)
        {
            if (content.Contains("(") && content.Contains(")"))
            {
                return Regex.Replace(content, @"\(.*\)", "");
            }
            return content;
        }
    }
}
