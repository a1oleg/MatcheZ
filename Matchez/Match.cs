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

        static readonly string foxsportLink = "https://www.foxsports.com/nba/boxscore?id=";        

        static readonly string ThuuzRatingXpath = "//*[@id=\"circle\"]/div/div";

        static readonly string ThuuzTeam1Xpath = "//*[@id=\"game-summary\"]/div[1]/table/tbody/tr[3]/td[1]/div/span";

        static readonly string ThuuzTeam2Xpath = "//*[@id=\"game-summary\"]/div[1]/table/tbody/tr[4]/td[1]/div/span";

        //static readonly string LeadChangeXpath = "//*[@id=\"wisbb_bsPlayerStats\"]/div[4]/div[3]/span";

        //static readonly string TimesTiedXpath = "//*[@id=\"wisbb_bsPlayerStats\"]/div[4]/div[4]/span";

        //static readonly string AttendanceXpath = "//*[@id=\"wisbb_bsPlayerStats\"]/div[4]/div[1]/span";

        static readonly string tbodyXpath = "//*[@id=\"wisbb_bsPlayerStats\"]/div[2]/table/tbody";

        static readonly string GAMEINFOXpath = "//*[@id=\"wisbb_bsPlayerStats\"]/div[4]";



        public string Team1 { get; set; }
        public string Team2 { get; set; }
        public int Id { get; set; } 
        public int Rate { get; set; }       
        


        public Match(int _id)
        {
            Id = _id;
            string url = foxsportLink + _id;

            //ThuuzPage = hw.Load(url);                       
            //var x = ThuuzPage.DocumentNode.SelectSingleNode(tbodyXpath);
            //Rate = Int32.Parse(ThuuzPage.DocumentNode.SelectSingleNode(ThuuzRatingXpath).InnerText);

            string selector = "#wisbb_bsPlayerStats > div.wisbb_bsTeamCompare > table > tbody";

            var htmlAsTask = LoadAndWaitForSelector(url, selector);
            htmlAsTask.Wait();

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(htmlAsTask.Result);

            var htmlBody = htmlDoc.DocumentNode.SelectSingleNode(tbodyXpath);

            //Console.WriteLine(htmlAsTask.Result);
            //Console.ReadKey();

            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");

            using (webDriver = new ChromeDriver(driversDir, chromeOptions))
            {
                webDriver.Navigate().GoToUrl(url);
                IWebElement tbody = webDriver.FindElement(By.XPath(tbodyXpath));
                Team1 = tbody.GetAttribute("innerHTML");

                IWebElement GAMEINFO = webDriver.FindElement(By.XPath(GAMEINFOXpath));
                Team2 = GAMEINFO.GetAttribute("innerHTML");
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

        public static async Task<string> LoadAndWaitForSelector(String url, String selector)
        {
            var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true,
                ExecutablePath = @"c:\Program Files (x86)\Google\Chrome\Application\chrome.exe"
            });
            using (Page page = await browser.NewPageAsync())
            {
                await page.GoToAsync(url);
                await page.WaitForSelectorAsync(selector);
                //var x = page.QuerySelectorAsync(selector);
                return await page.GetContentAsync();
            }
        }
    }
}
