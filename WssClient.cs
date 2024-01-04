using OpenQA.Selenium;
using OpenQA.Selenium.DevTools;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebSocketListener
{
    public class WssClient // DevTools
    {
        public static IWebDriver driver = Main.driver;
        /// <summary>
        /// сюда записываем все сообщения вебсокетов
        /// </summary>
        public static List<string> logger = new();

        public async Task StartListen()
        {
            IDevTools devTools = driver as IDevTools;
            var session = devTools.GetDevToolsSession();

            // Возможно потребуется использовать другую версию DevTools, зависит от версии хрома
            var networkAdapter = session.GetVersionSpecificDomains<OpenQA.Selenium.DevTools.V120.DevToolsSessionDomains>().Network;
            var websocketCommandSettings = new OpenQA.Selenium.DevTools.V120.Network.EnableCommandSettings();
            networkAdapter.Enable(websocketCommandSettings);
            networkAdapter.WebSocketFrameReceived += NetworkAdapter_WebSocketFrameReceived;
            networkAdapter.WebSocketFrameSent += NetworkAdapter_WebSocketMessagessReceived;
        }

        public List<string> ReturnLogger()
        {
            return logger;
        }
        /// <summary>
        /// получаем полученные сообщения от WebSocketов
        /// </summary>
        private void NetworkAdapter_WebSocketFrameReceived(object sender, OpenQA.Selenium.DevTools.V120.Network.WebSocketFrameReceivedEventArgs e)
        {
            logger.Add(e.Response.PayloadData);
        }
        /// <summary>
        /// получаем отправленные сообщения к WebSocketам
        /// </summary>
        private void NetworkAdapter_WebSocketMessagessReceived(object sender, OpenQA.Selenium.DevTools.V120.Network.WebSocketFrameSentEventArgs e)
        {
            logger.Add(e.Response.PayloadData);
        }
    }
}
