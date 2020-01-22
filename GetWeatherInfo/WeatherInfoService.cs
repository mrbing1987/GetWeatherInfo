using GetWeatherInfo.UserDefinedClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace GetWeatherInfo
{
    public partial class WeatherInfoService : ServiceBase
    {
        private System.Timers.Timer _timer = new System.Timers.Timer();

        private string[] _Province = null;

        private string[] _City = null;

        public WeatherInfoService()
        {
            InitializeComponent();
        }

        //protected override void OnStart(string[] args)
        public void OnStart()
        {
            _City = WeatherHelper.GetSupportCity("ALL");

            int _interval = 5000;
            _timer.Interval = _interval;
            _timer.AutoReset = true;
            _timer.Enabled = true;
            _timer.Elapsed += new System.Timers.ElapsedEventHandler(ActionRun);
        }

        protected override void OnStop()
        {
            _timer.AutoReset = false;
            _timer.Enabled = false;
            _timer.Stop();
        }

        private void ActionRun(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (_City!=null)
                {
                    foreach (var item in _City)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            string[] content = WeatherHelper.GetWeatherbyCityName(RegexHelper.Func(item));
                            DataBaseHelper.InsertTable(content);
                        }
                    }
                }
            }
            catch
            {
                return;
            }
        }
    }
}
