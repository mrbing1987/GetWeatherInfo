using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetWeatherInfo.UserDefinedClass
{
    /// <summary>
    /// 字符串帮助类
    /// </summary>
    public class StringHelper
    {
        #region 公有方法
        /// <summary>
        /// 截取字符串，超过的部分用...代替
        /// </summary>
        /// <param name="sourceCharacter">截取前字符串</param>
        /// <param name="interceptionLength">从左到右所截取的长度(默认为15个字符)</param>
        /// <returns>截取后字符串</returns>
        public string GetSubString(string sourceCharacter, int interceptionLength = 15)
        {
            if (string.IsNullOrWhiteSpace(sourceCharacter) || interceptionLength < 0)
            {
                return string.Empty;
            }

            string dot = "...";

            var bytesCount = Encoding.GetEncoding("gb2312").GetByteCount(sourceCharacter);
            if (bytesCount > interceptionLength)
            {
                int readyLength = 0;
                int byteLength = 0;

                for (int loop = 0; loop < sourceCharacter.Length; loop++)
                {
                    byteLength = Encoding.GetEncoding("gb2312").GetByteCount(new char[] { sourceCharacter[loop] });
                    readyLength += byteLength;
                    if (readyLength == interceptionLength)
                    {
                        sourceCharacter = string.Format("{0}{1}", sourceCharacter.Substring(0, loop + 1), dot);
                        break;
                    }
                    else if (readyLength > interceptionLength)
                    {
                        sourceCharacter = string.Format("{0}{1}", sourceCharacter.Substring(0, loop), dot);
                        break;
                    }
                }
            }
            return sourceCharacter;
        }

        /// <summary>
        /// 按字节长度(按字节,一个汉字为2个字节)取得某字符串的一部分
        /// </summary>
        /// <param name="sourceString">源字符串</param>
        /// <param name="length">所取字符串字节长度</param>
        /// <param name="tailString">附加字符串(当字符串不够长时，尾部所添加的字符串，一般为"...")</param>
        /// <returns>某字符串的一部分</returns>
        public string GetSubString(string sourceString, int length, string tailString)
        {
            try
            {
                if (string.IsNullOrEmpty(sourceString) || (length < 0) || string.IsNullOrEmpty(tailString))
                {
                    return string.Empty;
                }

                return GetSubString(sourceString, 0, length, tailString);
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 按字节长度(按字节,一个汉字为2个字节)取得某字符串的一部分
        /// </summary>
        /// <param name="sourceString">源字符串</param>
        /// <param name="startIndex">索引位置，以0开始</param>
        /// <param name="length">所取字符串字节长度</param>
        /// <param name="tailString">附加字符串(当字符串不够长时，尾部所添加的字符串，一般为"...")</param>
        /// <returns>某字符串的一部分</returns>
        public string GetSubString(string sourceString, int startIndex, int length, string tailString)
        {
            try
            {
                if (string.IsNullOrEmpty(sourceString) || string.IsNullOrEmpty(tailString) || (startIndex < 0) || (length < 0))
                {
                    return string.Empty;
                }

                string myResult = sourceString;

                // 当是日文或韩文时(注:中文的范围:\u4e00 - \u9fa5, 日文在\u0800 - \u4e00, 韩文为\xAC00-\xD7A3)
                if (System.Text.RegularExpressions.Regex.IsMatch(sourceString, "[\u0800-\u4e00]+") ||
                    System.Text.RegularExpressions.Regex.IsMatch(sourceString, "[\xAC00-\xD7A3]+"))
                {
                    //当截取的起始位置超出字段串长度时
                    if (startIndex >= sourceString.Length)
                    {
                        return string.Empty;
                    }
                    else
                    {
                        return sourceString.Substring(startIndex, ((length + startIndex) > sourceString.Length) ?
                            (sourceString.Length - startIndex) :
                            length);
                    }
                }

                // 中文字符，如"中国人民abcd123"
                if (length <= 0)
                {
                    return string.Empty;
                }

                byte[] bytesSource = Encoding.Default.GetBytes(sourceString);

                // 当字符串长度大于起始位置
                if (bytesSource.Length > startIndex)
                {
                    int endIndex = bytesSource.Length;

                    // 当要截取的长度在字符串的有效长度范围内
                    if (bytesSource.Length > (startIndex + length))
                    {
                        endIndex = length + startIndex;
                    }
                    else
                    {
                        // 当不在有效范围内时,只取到字符串的结尾
                        length = bytesSource.Length - startIndex;
                        tailString = "";
                    }

                    int[] anResultFlag = new int[length];
                    int nFlag = 0;

                    // 字节大于127为双字节字符
                    for (int loop = startIndex; loop < endIndex; loop++)
                    {
                        if (bytesSource[loop] > 127)
                        {
                            nFlag++;
                            if (nFlag == 3)
                            {
                                nFlag = 1;
                            }
                        }
                        else
                        {
                            nFlag = 0;
                        }
                        anResultFlag[loop] = nFlag;
                    }

                    // 最后一个字节为双字节字符的一半
                    if ((bytesSource[endIndex - 1] > 127) && (anResultFlag[length - 1] == 1))
                    {
                        length = length + 1;
                    }

                    byte[] bsResult = new byte[length];
                    Array.Copy(bytesSource, startIndex, bsResult, 0, length);
                    myResult = Encoding.Default.GetString(bsResult);
                    myResult = myResult + tailString;

                    return myResult;
                }

                return string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }
        #endregion 公有方法
    }
}
