using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PuppeteerSharp;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Matchez
{
    public class Match
    {
        private static IWebDriver webDriver;
        private static TimeSpan defaultWait = TimeSpan.FromSeconds(50);

        private static String driversDir = @"C:\Users\a1ole\Desktop\chromedriver_win32";

        public static HtmlWeb hw = new HtmlWeb();
        public HtmlDocument ThuuzPage { get; set; }
        public HtmlDocument NbaStatPage { get; set; }

        static readonly string ThuuzLink = "http://www.thuuz.com/basketball/nba/game/";

        static readonly string NbaStatLink = "https://stats.nba.com/game/";

        static readonly string ThuuzRatingXpath = "//*[@id=\"circle\"]/div/div";
        //static readonly string ThuuzTeam1Xpath = "/html/body/div[2]/div[3]/div[1]/table/tbody/tr[1]/td[2]/div";
        static readonly string ThuuzTeam1Xpath = "//*[@id=\"game-summary\"]/div[1]/table/tbody/tr[3]/td[1]/div/span";

        //static readonly string ThuuzTeam2Xpath = "/html/body/div[2]/div[3]/div[1]/table/tbody/tr[2]/td[1]/div";
        static readonly string ThuuzTeam2Xpath = "//*[@id=\"game-summary\"]/div[1]/table/tbody/tr[4]/td[1]/div/span";

        static readonly string gameSummaryXpath = "//*[@id=\"game-summary\"]/div[3]/table/tbody/";
        public string Team1 { get; set; }
        public string Team2 { get; set; }
        public int Id { get; set; } 
        public int Rate { get; set; }       
        


        public Match(int _id)
        {
            //Id = _id;
            //ThuuzPage = hw.Load(ThuuzLink + _id);
            
            //Rate = Int32.Parse(ThuuzPage.DocumentNode.SelectSingleNode(ThuuzRatingXpath).InnerText);

            string url = ( NbaStatLink + "00" + (_id + 21588897));

            //var chromeOptions = new ChromeOptions();
            //chromeOptions.AddArguments("headless");

            using (webDriver = new ChromeDriver(driversDir/*, chromeOptions*/))
            {
                webDriver.Navigate().GoToUrl(url);
                IWebElement table = FindElement(By.XPath("html/body/main/div[2]/div/div/div[4]/div/div[2]/div/nba-stat-table[1]/div[2]/div[1]/table/tfoot"));///div[4]
                var innerHtml = table.GetAttribute("innerHTML");
            }

        }

        #region (!) I didn't even use this, but it can be useful (!)
        public static IWebElement FindElement(By by)
        {
            try
            {
                WaitForAjax();
                var wait = new WebDriverWait(webDriver, defaultWait);
                return wait.Until(driver => driver.FindElement(by));
            }
            catch
            {
                return null;
            }
        }

        public static void WaitForAjax()
        {
            var wait = new WebDriverWait(webDriver, defaultWait);
            wait.Until(d => (bool)(d as IJavaScriptExecutor).ExecuteScript("return jQuery.active == 0"));
        }
        #endregion
    }
}
