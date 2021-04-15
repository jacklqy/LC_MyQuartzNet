管理员身份打开cmd

//注册Windows服务
C:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil  G:\github\LC_MyQuartzNet\LC_QuartzNetWindowsService\bin\Debug\LC_QuartzNetWindowsService.exe

//卸载Windows服务
C:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil /u  G:\github\LC_MyQuartzNet\LC_QuartzNetWindowsService\bin\Debug\LC_QuartzNetWindowsService.exe


服务改名称后，需要先原样卸载，再重新编译安装,要不然卸载还蛮麻烦的

