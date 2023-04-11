using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Appium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ZMGDesktopTests.Support
{
    public static class GuiDriverv3
    {
        private static WindowsDriver<WindowsElement> driver;
        public static WindowsDriver<WindowsElement> GetOrCreateDriver()
        {
            if (driver == null)
            {
                driver = CreateDriverInstance();
            }
            return driver;
        }
        public static WindowsDriver<WindowsElement> GetDriver()
        {
            return driver;
        }
        private static WindowsDriver<WindowsElement> CreateDriverInstance()
        {
            IntPtr myAppTopLevelWindowsHandle = new IntPtr();
            foreach (Process clsProcess in Process.GetProcesses())
            {
                if (clsProcess.ProcessName.Contains("ZMGDesktop"))
                {
                    myAppTopLevelWindowsHandle = clsProcess.MainWindowHandle;
                    break;
                }
            }
            var appTopLevelWindowsHandleHex = myAppTopLevelWindowsHandle.ToString("x");
            var options = new AppiumOptions();
            options.AddAdditionalCapability("app", @"C:\Users\Sebastian\source\repos\tkipp-sbicak-sarbutina-pbuzic-zbelina\Software\ZMGv2\ZMGDesktop.exe");
            options.AddAdditionalCapability("deviceName", "WindowsPC");
            if (appTopLevelWindowsHandleHex == "0")
            {
                //Ukoliko je heksadecimalna vrijdenost nula, znaci da nije pokrenut proces sa zadanim imenom te se pokrece sa diska
                options.AddAdditionalCapability("app", @"C:\Users\okwin\source\repos\tkipp-sbicak-sarbutina-pbuzic-zbelina\Software\ZMGv2\ZMGDesktop.exe");
            }
            else
            {
                //Aplikacije je pokrenuta
                options.AddAdditionalCapability("appTopLevelWindow", appTopLevelWindowsHandleHex);
            }
            var wd = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), options);
            wd.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            return wd;
        }
        public static void Dispose()
        {
            if (driver != null)
            {
                foreach (var wh in driver.WindowHandles)
                {
                    driver.SwitchTo().Window(wh);
                    driver.CloseApp();
                }
                driver.Quit();
                driver.Dispose();
                driver = null;
            }
        }

    }
}
