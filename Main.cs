using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace WebSocketListener
{
    public class Main
    {
        private static Main _manager;
        public static IWebDriver driver;
        private static ChromeOptions _options = new();
        /// тут менеджеры

        /// <summary>
        /// Конструктор, создающий пекрвый экземпляр класса Main
        /// </summary>
        public static Main Manager
        {
            get
            {
                if (_manager == null)
                {
                    _manager = new Main();
                }
                if (driver == null)
                {
                    driver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), _options);
                }
                return _manager;
            }
            set { _manager = value; }
        }
        public static void SetDriver(IWebDriver _driver) => driver = _driver;
    }
}
