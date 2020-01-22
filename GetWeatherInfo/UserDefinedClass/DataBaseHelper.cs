using EF.ORM;
using GetWeatherInfo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetWeatherInfo.UserDefinedClass
{
    static class DataBaseHelper
    {
        #region 字段
        /// <summary>
        /// 
        /// </summary>
        private static EFHelper _client = new EFHelper(new WeatherInfoEntities());
        #endregion 字段

        /// <summary>
        /// 
        /// </summary>
        /// <param name="weatherData"></param>
        public static bool InsertTable(string[] weatherData)
        {
            if (_client != null)
            {
                AESHelper _aes = new AESHelper();

                return _client.InsertData<WeatherTable>(new WeatherTable()
                {
                    InsertDate = DateTime.Now.ToString("yyyy-MM-dd"),
                    Province = weatherData[0],
                    City = weatherData[1],
                    Code = weatherData[2],
                    CodeImage = weatherData[3],
                    DateTime = weatherData[4],
                    Temperature_1 = weatherData[5],
                    WeatherInfo_1 = weatherData[6],
                    WindInfo_1 = weatherData[7],
                    Image_1_1 = weatherData[8],
                    Image_1_2 = weatherData[9],
                    WeatherDetail_1_1 = weatherData[10],
                    WeatherDetail_1_2 = weatherData[11],
                    Temperature_2 = weatherData[12],
                    WeatherInfo_2 = weatherData[13],
                    WindInfo_2 = weatherData[14],
                    Image_2_1 = weatherData[15],
                    Image_2_2 = weatherData[16],
                    Temperature_3 = weatherData[17],
                    WeatherInfo_3 = weatherData[18],
                    WindInfo_3 = weatherData[19],
                    Image_3_1 = weatherData[20],
                    Image_3_2 = weatherData[21],
                    //CityNotes = weatherData[22])
                });
            }
            return false;
        }
    }
}
