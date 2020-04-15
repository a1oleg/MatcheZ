using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using PuppeteerSharp;

using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Opera;

namespace Matchez
{
    
    public class ThuuzMatch
    {
        static string ThuuzLink = "https://www.thuuz.com/basketball/nba/game/";

        static string Thuuz2dgraphLink = "https://www.thuuz.com/2dgraph/";

        //static string tbodyXpath = "//*[@id=\"wisbb_bsPlayerStats\"]/div[2]/table/tbody";

        //static string gameinfoXpath = "//*[@id=\"wisbb_bsPlayerStats\"]/div[4]";


        static string ThuuzSelector = "#circle > div";

        static readonly string DateXpath = "/html/body/div[2]/div[2]/table/tbody/tr/td[2]";

        static readonly string Team1Xpath = "/html/body/div[2]/div[3]/div[1]/table/tbody/tr[1]/td[2]/div";

        static readonly string Team2Xpath = "/html/body/div[2]/div[3]/div[1]/table/tbody/tr[2]/td[1]/div";


        public string Team1 { get; set; }
        public string Team2 { get; set; }
        public int ThuuzId { get; set; }        
        public string Date { get; set; }

        public List<int> var_gec { get; set; }

        public int avind { get; set; }
        public List<int> var_gec_away { get; set; }
        public List<int> var_gec_home { get; set; }



        public ThuuzMatch(int _id)
        {
            ThuuzId = _id;
            string url = ThuuzLink + _id;
            var url2 = Thuuz2dgraphLink + _id;
            string[] arrs = GetTextAgility(url2);
            
            var var_gecL = arrs[1].Split(',').Take(50).ToList();
            var_gec = Remover(var_gecL);
            
            avind = (int)var_gec.Average();

            var var_gec_awayL = arrs[2].Split(',').Take(50).ToList();
            var_gec_away = Remover(var_gec_awayL);
            var var_gec_homeL = arrs[3].Split(',').Take(50).ToList();
            var_gec_home = Remover(var_gec_homeL);

            var htmlAsTask = LoadAndWaitForSelector(url,  ThuuzSelector);
            htmlAsTask.Wait();

            HtmlDocument htmlDoc = new HtmlDocument();

            htmlDoc.LoadHtml(htmlAsTask.Result);

            Team1 = htmlDoc.DocumentNode.SelectSingleNode(Team1Xpath).InnerText;
            Team2 = htmlDoc.DocumentNode.SelectSingleNode(Team2Xpath).InnerText;

            Date = htmlDoc.DocumentNode.SelectSingleNode(DateXpath).InnerText.Split("Final - ")[1].Split(@"\")[0];

        }

        public static List<int> Remover(List<string> li)
        {
            List<int> wn = new List<int>();// { 12, 25, 38 };
            int y = 0;
            foreach(string x in li.Where(x => Int32.TryParse(x, out y)))
            {
                wn.Add(y);
            }
            return wn;
        }

        public static string[] GetTextAgility(String url)
        {
            var web = new HtmlWeb();
            var doc = web.Load(url);
            return doc.Text.Split("_data = \"");
        }

        public static async Task<string[]> GetContentAsync(String url)
        {
            

            var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true,
                ExecutablePath = @"c:\Program Files (x86)\Google\Chrome\Application\chrome.exe"
            });

            using (var page = await browser.NewPageAsync())
            {

                await page.GoToAsync(url);
                var x = await page.GetContentAsync();
                var y = x.Split("_data = \"");                                

                browser.Dispose();
                return y;
            }
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
                //var x = page.QuerySelectorAsync(selector);
                var x = await page.GetContentAsync();
                browser.Dispose();
                return x;
            }
        }

    }
}
