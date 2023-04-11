using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Appium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZMGDesktopTests.Support
{
    public static class GuiDriver
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
            var options = new AppiumOptions();
            options.AddAdditionalCapability("app", @"C:\Users\zvona\Desktop\Faks\3. godina\6. semestar\TKPP\ZMG Desktop\Software\ZMG\ZMGDesktop.exe");
            options.AddAdditionalCapability("deviceName", "WindowsPC");
            var wd = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"),
           options);
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
