using HtmlAgilityPack;
using System;
using System.Threading.Tasks;
using PuppeteerSharp;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Matchez
{
    public class PupMatch
    {        
        static string foxsportLink = "https://www.foxsports.com/nba/boxscore?id=";

        static readonly string ThuuzLink = "https://www.thuuz.com/basketball/nba/game/";
        

        static string FoxSelector = "#wisbb_bsPlayerStats > div.wisbb_bsTeamCompare > table > tbody";

        static string tbodyXpath = "//*[@id=\"wisbb_bsPlayerStats\"]/div[2]/table/tbody";

        static string gameinfoXpath = "//*[@id=\"wisbb_bsPlayerStats\"]/div[4]";


        static string ThuuzSelector = "#circle > div";

        static readonly string RatingXpath = "/html/body/div[2]/div[3]/div[1]/table/tbody/tr[1]/td[1]/div[1]/div[1]";
        //static readonly string RatingXpath = "//*[@id=\"circle\"]/div/div";

        static readonly string DateXpath = "/html/body/div[2]/div[2]/table/tbody/tr/td[2]";

        static readonly string Team1Xpath = "/html/body/div[2]/div[3]/div[1]/table/tbody/tr[1]/td[2]/div";

        static readonly string Team2Xpath = "/html/body/div[2]/div[3]/div[1]/table/tbody/tr[2]/td[1]/div";


        public string Team1 { get; set; }
        public string Team2 { get; set; }
        public int Id { get; set; } 
        public int Rate { get; set; }

        public string Date { get; set; }

        public PupMatch(int _id, bool foxOrThuuz)
        {
            Id = _id;
            string url = foxOrThuuz ? foxsportLink + _id : ThuuzLink + _id;

            var htmlAsTask = LoadAndWaitForSelector(url, foxOrThuuz ? FoxSelector  : ThuuzSelector) ;
            htmlAsTask.Wait();

            HtmlDocument htmlDoc = new HtmlDocument();

            htmlDoc.LoadHtml(htmlAsTask.Result);

            if(foxOrThuuz)
            {
                Team1 = htmlDoc.DocumentNode.SelectSingleNode(tbodyXpath).InnerHtml;
                Team2 = htmlDoc.DocumentNode.SelectSingleNode(gameinfoXpath).InnerHtml;
            }
            else
            {
                Team1 = htmlDoc.DocumentNode.SelectSingleNode(Team1Xpath).InnerText;
                Team2 = htmlDoc.DocumentNode.SelectSingleNode(Team2Xpath).InnerText;

                Rate = Int32.Parse(htmlDoc.DocumentNode.SelectSingleNode(RatingXpath).InnerText);

                //\n\t\t\t\t\t\n\t\t\t\n\t\t\t\t\t\t\n\t\t\t\t\t\t\tFinal - 10/16/18\n\t\t\t\t\t\t\n\t\t\t\t\t\n\t\t\t\t
                Date = htmlDoc.DocumentNode.SelectSingleNode(DateXpath).InnerText.Split("Final - ")[1].Split(@"\")[0];
            }
        }






        public static async Task<string> LoadAndWaitForSelector(String url, String selector)
        {
            var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true,
                ExecutablePath = @"c:\Program Files (x86)\Google\Chrome\Application\chrome.exe"
            }) ;
            using (Page page = await browser.NewPageAsync())
            {
                await page.GoToAsync(url);
                await page.WaitForSelectorAsync(selector);
                //var x = page.QuerySelectorAsync(selector);
                var x = await page.GetContentAsync();
                browser.Dispose();
                return x;
            }
        }
    }
}
