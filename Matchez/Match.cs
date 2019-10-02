using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PuppeteerSharp;

namespace Matchez
{
    public class Match
    {        
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

            var htmlAsTask = LoadAndWaitForSelector(url, "head");//nba-stat-table__overflow
            htmlAsTask.Wait();
            Console.WriteLine(htmlAsTask.Result);

            Console.ReadKey();
            

        }

        static void Plain(string url)
        {
            var htmlAsTask = LoadAndWaitForSelector(url, "nba-stat-table");
            htmlAsTask.Wait();
            Console.WriteLine(htmlAsTask.Result);

            Console.ReadKey();
        }

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
                return await page.GetContentAsync();
            }
        }



    }
}
