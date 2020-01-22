using GetWeatherInfo.UserDefinedClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

// http://www.webxml.com.cn/WebServices/WeatherWebService.asmx

namespace GetWeatherInfo
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
            //ServiceBase[] ServicesToRun;
            //ServicesToRun = new ServiceBase[]
            //{
            //    new WeatherInfoService()
            //};
            //ServiceBase.Run(ServicesToRun);

            // 调试
            WeatherInfoService s1 = new WeatherInfoService();
            s1.OnStart();

            while (true) ;
        }
    }
}
