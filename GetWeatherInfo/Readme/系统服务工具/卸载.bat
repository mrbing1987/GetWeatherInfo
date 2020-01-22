net stop WeatherInfoService
sc delete WeatherInfoService binPath= "%~dp0JDGetWeatherInfo.exe" start= auto
pause