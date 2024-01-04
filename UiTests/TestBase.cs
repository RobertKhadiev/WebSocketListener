using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace WebSocketListener.UiTests
{
    public class TestBase
    {
        public IWebDriver driver { get; set; }
        [SetUp]
        public void BaseSetup()
        {
            Main.SetDriver(driver);
            InitLocalDriver();
        }

        private void InitLocalDriver()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddAdditionalOption("useAutomationExtension", false);
            options.AddArgument("==window-size=1037, 1080");
            options.AddArgument("incognito");
            driver = new ChromeDriver(service: ChromeDriverService.CreateDefaultService(), options: options, commandTimeout: TimeSpan.FromMinutes(5));
            driver.Manage().Window.Maximize();
            Main.driver = driver;
        }
    }
}
