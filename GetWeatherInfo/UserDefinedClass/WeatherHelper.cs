using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetWeatherInfo.UserDefinedClass
{
    static class WeatherHelper
    {
        /// <summary>
        /// 通过城市名称获取城市天气信息
        /// </summary>
        /// <param name="cityName">城市名称</param>
        /// <returns>城市天气信息或NULL</returns>
        public static string[] GetWeatherbyCityName(string cityName)
        {
            try
            {
                if (!string.IsNullOrEmpty(cityName))
                {
                    cn.com.webxml.www.WeatherWebService weather = new cn.com.webxml.www.WeatherWebService();
                    return weather.getWeatherbyCityName(cityName);
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获取支持的国内外城市或地区信息
        /// </summary>
        /// <param name="provinceName">指定的洲或国内的省份，若为ALL或空则表示返回全部城市</param>
        /// <returns>支持的国内外城市或地区信息或NULL</returns>
        public static string[] GetSupportCity(string provinceName = "ALL")
        {
            try
            {
                if (!string.IsNullOrEmpty(provinceName))
                {
                    cn.com.webxml.www.WeatherWebService weather = new cn.com.webxml.www.WeatherWebService();
                    return weather.getSupportCity(provinceName);
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获取支持的洲、国内外省份和城市信息
        /// </summary>
        /// <returns>支持的洲、国内外省份和城市信息或NULL</returns>
        public static string[] GetSupportProvince()
        {
            try
            {
                cn.com.webxml.www.WeatherWebService weather = new cn.com.webxml.www.WeatherWebService();
                return weather.getSupportProvince();
            }
            catch
            {
                return null;
            }
        }
    }
}
