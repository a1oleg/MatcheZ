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

        public List<int> gec { get; set; }

        public int avgec { get; set; }
        public List<int> gec_away { get; set; }
        public List<int> gec_home { get; set; }



        public ThuuzMatch(int _id)
        {
            ThuuzId = _id;
            string urlTD = ThuuzLink + _id;
            string urlGec = Thuuz2dgraphLink + _id;

            GetGec(urlGec);
            GetTeamsDates(urlTD);
        }        

        public void GetGec(String url)
        {
            var web = new HtmlWeb();
            var doc = web.Load(url);
            var arrs = doc.Text.Split("_data = \"");

            var var_gecL = arrs[1].Split(',').Take(50).ToList();
            this.gec = Remover(var_gecL);
            if(gec.Count() > 0)
                this.avgec = (int)gec.Average();
            var var_gec_awayL = arrs[2].Split(',').Take(50).ToList();
            this.gec_away = Remover(var_gec_awayL);
            var var_gec_homeL = arrs[3].Split(',').Take(50).ToList();
            this.gec_home = Remover(var_gec_homeL);
        }

        public void GetTeamsDates(String url)
        {
            var web = new HtmlWeb();
            var doc = web.Load(url);

            //"Upcoming - 7:00 PM (EDT) 11/1/12
            try
            {
                Date = doc.Text.Split("Final - ")[1].Substring(0, 8);
            }
            catch
            {
                try
                {
                    Date = doc.Text.Split("DT) ")[1].Substring(0, 8);
                }
                catch
                {
                    Date = doc.Text.Split("ST) ")[1].Substring(0, 8);
                }
                
            }


            Team1 = doc.Text.Split("Thuuz - ")[1].Split(" at ")[0];
            Team2 = doc.Text.Split("Thuuz - ")[1].Split(" at ")[1].Split(" (")[0];
        }



        public static List<int> Remover(List<string> li)
        {
            List<int> wn = new List<int>();// { 12, 25, 38 };
            int y = 0;
            foreach (string x in li.Where(x => Int32.TryParse(x, out y)))
            {
                wn.Add(y);
            }
            return wn;
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
