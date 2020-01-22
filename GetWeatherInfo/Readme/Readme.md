该天气预报 Web 服务（http://www.webxml.com.cn/WebServices/WeatherWebService.asmx），数据来源于中国气象局 http://www.cma.gov.cn/ ，数据每2.5小时左右自动更新一次，准确可靠。包括 340 多个中国主要城市和 60 多个国外主要城市三日内的天气预报数据。

方法：

1、引入Web服务。在VS中项目上右击→添加服务引用。

2、在弹出的添加服务引用窗口，录入web服务地址和引用后的命名空间。

错误处理方法：

添加服务引用->高级->添加web服务引用

定义w对象时 用cn.com.webxml.www.WeatherWebService w = new cn.com.webxml.www.WeatherWebService();

cn.com.webxml.www是引用名。

天气预报WEB服务接口说明 - 天气现象和图例

﻿http://www.webxml.com.cn/zh_cn/weather_icon.aspx

