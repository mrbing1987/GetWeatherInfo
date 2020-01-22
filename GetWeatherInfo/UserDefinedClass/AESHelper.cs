using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GetWeatherInfo.UserDefinedClass
{
    /// <summary>
    /// AES加密/解密类
    /// </summary>
    public class AESHelper
    {
        #region 字段
        /// <summary>
        /// 字符串帮助类实例
        /// </summary>
        private StringHelper _stringHelper = new StringHelper();

        /// <summary>
        /// keys
        /// </summary>
        private byte[] _keys = { 0x41, 0x72, 0x65, 0x79, 0x6F, 0x75, 0x6D, 0x79, 0x53, 0x6E, 0x6F, 0x77, 0x6D, 0x61, 0x6E, 0x3F };
        #endregion 字段

        #region 公有方法
        /// <summary>
        /// 对称加密算法AES RijndaelManaged加密(RijndaelManaged（AES）算法是块式加密算法)，(CBC)有向量（IV）
        /// </summary>
        /// <param name="encryptString">待加密字符串</param>
        /// <param name="encryptKey">加密密钥，须半角字符</param>
        /// <returns>加密结果字符串</returns>
        public string AESEncryptCBC(string encryptString, string encryptKey)
        {
            try
            {
                if (string.IsNullOrEmpty(encryptString) || string.IsNullOrEmpty(encryptKey))
                {
                    return string.Empty;
                }

                encryptKey = _stringHelper.GetSubString(encryptKey, 32, "");
                encryptKey = encryptKey.PadRight(32, ' ');

                RijndaelManaged rijndaelProvider = new RijndaelManaged()
                {
                    Key = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 32)),
                    IV = _keys
                };

                ICryptoTransform rijndaelEncrypt = rijndaelProvider.CreateEncryptor();

                byte[] inputData = Encoding.UTF8.GetBytes(encryptString);
                byte[] encryptedData = rijndaelEncrypt.TransformFinalBlock(inputData, 0, inputData.Length);

                return Convert.ToBase64String(encryptedData);
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary> 
        ///对称加密算法AES RijndaelManaged解密字符串，(CBC)有向量（IV）
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串,失败返回空</returns>
        public string AESDecryptCBC(string decryptString, string decryptKey)
        {
            try
            {
                if (string.IsNullOrEmpty(decryptString) || string.IsNullOrEmpty(decryptKey))
                {
                    return string.Empty;
                }

                decryptKey = _stringHelper.GetSubString(decryptKey, 32, "");
                decryptKey = decryptKey.PadRight(32, ' ');

                RijndaelManaged rijndaelProvider = new RijndaelManaged()
                {
                    Key = Encoding.UTF8.GetBytes(decryptKey),
                    IV = _keys
                };

                ICryptoTransform rijndaelDecrypt = rijndaelProvider.CreateDecryptor();

                byte[] inputData = Convert.FromBase64String(decryptString);
                byte[] decryptedData = rijndaelDecrypt.TransformFinalBlock(inputData, 0, inputData.Length);

                return Encoding.UTF8.GetString(decryptedData);
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// AES加密字符串，(ECB)无向量（IV）
        /// </summary>
        /// <param name="encryptString">被加密的明文</param>  
        /// <param name="encryptKey">密钥</param>
        /// <returns>密文字符串</returns>
        public string AESEncryptECB(string encryptString, string encryptKey)
        {
            try
            {
                if (string.IsNullOrEmpty(encryptString) || string.IsNullOrEmpty(encryptKey))
                {
                    return string.Empty;
                }

                encryptKey = _stringHelper.GetSubString(encryptKey, 32, "");
                encryptKey = encryptKey.PadRight(32, ' ');

                RijndaelManaged rijndaelProvider = new RijndaelManaged()
                {
                    Key = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 32)),
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                };

                ICryptoTransform rijndaelEncrypt = rijndaelProvider.CreateEncryptor();
                byte[] inputData = Encoding.UTF8.GetBytes(encryptString);
                byte[] encryptedData = rijndaelEncrypt.TransformFinalBlock(inputData, 0, inputData.Length);
                return Convert.ToBase64String(encryptedData);
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// AES解密字符串，(ECB)无向量（IV）
        /// </summary>
        /// <param name="decryptString">被加密的明文</param>
        /// <param name="decryptKey">密钥</param>
        /// <returns>明文字符串</returns>
        public string AESDecryptECB(string decryptString, string decryptKey)
        {
            try
            {
                if (string.IsNullOrEmpty(decryptString) || string.IsNullOrEmpty(decryptKey))
                {
                    return string.Empty;
                }

                decryptKey = _stringHelper.GetSubString(decryptKey, 32, "");
                decryptKey = decryptKey.PadRight(32, ' ');

                RijndaelManaged rijndaelProvider = new RijndaelManaged()
                {
                    Key = Encoding.UTF8.GetBytes(decryptKey.Substring(0, 32)),
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                };

                ICryptoTransform rijndaelDecrypt = rijndaelProvider.CreateDecryptor();
                byte[] inputData = Convert.FromBase64String(decryptString);
                byte[] decryptedData = rijndaelDecrypt.TransformFinalBlock(inputData, 0, inputData.Length);
                return Encoding.UTF8.GetString(decryptedData);
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// AES加密文件流
        /// </summary>
        /// <param name="fs">文件流</param>
        /// <param name="decryptKey">密钥</param>
        /// <returns>密文文件流</returns>
        public CryptoStream AES_EncryptStrream(FileStream fs, string decryptKey)
        {
            try
            {
                if ((fs == null) || string.IsNullOrEmpty(decryptKey))
                {
                    return null;
                }

                decryptKey = _stringHelper.GetSubString(decryptKey, 32, "");
                decryptKey = decryptKey.PadRight(32, ' ');

                RijndaelManaged rijndaelProvider = new RijndaelManaged()
                {
                    Key = Encoding.UTF8.GetBytes(decryptKey),
                    IV = _keys
                };

                CryptoStream cytptostreamEncr = new CryptoStream(fs, rijndaelProvider.CreateEncryptor(), CryptoStreamMode.Write);
                return cytptostreamEncr;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// AES解密文件流
        /// </summary>
        /// <param name="fs">文件流</param>
        /// <param name="decryptKey">密钥</param>
        /// <returns>明文文件流</returns>
        public CryptoStream AES_DecryptStream(FileStream fs, string decryptKey)
        {
            try
            {
                if ((fs == null) || string.IsNullOrEmpty(decryptKey))
                {
                    return null;
                }

                decryptKey = _stringHelper.GetSubString(decryptKey, 32, "");
                decryptKey = decryptKey.PadRight(32, ' ');

                RijndaelManaged rijndaelProvider = new RijndaelManaged()
                {
                    Key = Encoding.UTF8.GetBytes(decryptKey),
                    IV = _keys
                };

                CryptoStream cytptostreamDecr = new CryptoStream(fs, rijndaelProvider.CreateDecryptor(), CryptoStreamMode.Read);
                return cytptostreamDecr;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 对指定文件加密
        /// </summary>
        /// <param name="sourceFile">待加密源文件路径</param>
        /// <param name="outputFile">密文文件路径</param>
        /// <param name="decryptKey">密钥</param>
        /// <returns></returns>
        public bool AES_EncryptFile(string sourceFile, string outputFile, string decryptKey)
        {
            try
            {
                if (string.IsNullOrEmpty(sourceFile) || string.IsNullOrEmpty(outputFile) || string.IsNullOrEmpty(decryptKey) || !File.Exists(sourceFile))
                {
                    return false;
                }

                using (FileStream fr = new FileStream(sourceFile, FileMode.Open))
                {
                    using (FileStream fren = new FileStream(outputFile, FileMode.Create))
                    {
                        using (CryptoStream Enfr = AES_EncryptStrream(fren, decryptKey))
                        {
                            byte[] bytearrayinput = new byte[fr.Length];

                            fr.Read(bytearrayinput, 0, bytearrayinput.Length);
                            Enfr.Write(bytearrayinput, 0, bytearrayinput.Length);
                        }
                    }
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 对指定的文件解密
        /// </summary>
        /// <param name="sourceFile">待加密源文件路径</param>
        /// <param name="outputFile">密文文件路径</param>
        /// <param name="decryptKey">密钥</param>
        /// <returns></returns>
        public bool AES_DecryptFile(string sourceFile, string outputFile, string decryptKey)
        {
            try
            {
                if (string.IsNullOrEmpty(sourceFile) || string.IsNullOrEmpty(outputFile) || string.IsNullOrEmpty(decryptKey) || !File.Exists(sourceFile))
                {
                    return false;
                }

                using (FileStream fr = new FileStream(sourceFile, FileMode.Open))
                {
                    using (FileStream frde = new FileStream(outputFile, FileMode.Create))
                    {
                        using (CryptoStream Defr = AES_DecryptStream(fr, decryptKey))
                        {
                            byte[] bytearrayoutput = new byte[1024];

                            int m_count = 0;

                            do
                            {
                                m_count = Defr.Read(bytearrayoutput, 0, bytearrayoutput.Length);
                                frde.Write(bytearrayoutput, 0, m_count);

                                if (m_count < bytearrayoutput.Length)
                                {
                                    break;
                                }
                            } while (true);
                        }
                    }
                }
            }
            catch
            {
                return false;
            }

            return true;
        }
        #endregion 公有方法
    }
}
