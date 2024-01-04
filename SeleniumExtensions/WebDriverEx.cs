using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace WebSocketListener.SeleniumExtensions
{
    public static class WebDriverEx
    {
        public static void WaitForFullLoad(this IWebDriver driver, int timeOutInSeconds = 120, int poolingInterval = 3)
        {
            string js = "return document.readyState";
            WebDriverWait waitForFullLoad = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOutInSeconds))
            {
                PollingInterval = TimeSpan.FromSeconds(poolingInterval)
            };
            try
            {
                waitForFullLoad.Until(d => ((IJavaScriptExecutor)d).ExecuteScript(js).Equals("complete"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static IWebElement GetElementAndScrollTo(this IWebDriver driver, By by)
        {
            var js = (IJavaScriptExecutor)driver;
            try
            {
                var element = driver.FindElement(by);
                if (element.Location.Y > 200)
                {
                    js.ExecuteScript($"window.scrollTo({0}, {element.Location.Y - 400})");
                }
                return element;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
