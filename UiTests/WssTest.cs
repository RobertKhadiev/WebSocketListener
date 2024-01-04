using WebSocketListener.SeleniumExtensions;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
using System;

namespace WebSocketListener.UiTests
{
    public class WssTest : TestBase
    {
        public const string Url = "https://socketsbay.com/test-websockets";
        [Test]
        public async Task SocketsBayTest()
        {
            driver.Navigate().GoToUrl(Url);
            driver.WaitForFullLoad();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(200));

            var wssClient = new WssClient();
            await wssClient.StartListen();
            // отправляем сообщение
            driver.FindElement(By.XPath("//button[@id='btnConnect']")).Click();
            driver.FindElement(By.XPath("//input[@id='txtToSend']")).SendKeys("TestMessage!");
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//button[@type='submit']")));
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
            // дожидаемся получения сообщения
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//pre[text()[contains(., 'RECEIVE')]]")));

            var result = wssClient.ReturnLogger();
            Assert.IsTrue(result.Any(x => x.Contains("TestMessage!")));
        }
    }
}
