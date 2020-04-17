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
        static string BasRefLink = "https://www.basketball-reference.com/boxscores/";
        
        static string BoxScoreSelector = "#four_factors > thead > tr";
        static string FourFactors = "/html/body/div[2]/div[4]/div[5]/div[2]/div/div[3]/div/table/tbody";

        static string BasRefLinkpbp = "https://www.basketball-reference.com/boxscores/pbp/";
        static string LongestScoringDroughtSelector = "#st_0";

        

        static string TiesLeadChanges = "/html/body/div[2]/div[4]/div[5]/div[3]/div/div[1]/table/tbody";
        static string MostConsecutivePoints = "/html/body/div[2]/div[4]/div[5]/div[3]/div/div[2]/table/tbody";
        static string LongestScoringDrought = "/html/body/div[2]/div[4]/div[5]/div[3]/div/div[3]/table/tbody";



        public PupMatch(int date, string home)
        {
            string url = BasRefLink + date + '0' + home.ToUpper() + ".html";
            string urlpbp = BasRefLinkpbp + date + '0' + home.ToUpper() + ".html";

            var htmlAsTask = LoadAndWaitForSelector(url, BoxScoreSelector);
            var htmlAsTaskpbp = LoadAndWaitForSelector(urlpbp, LongestScoringDroughtSelector);
            htmlAsTask.Wait();

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(htmlAsTask.Result);
            var LeadC = doc.DocumentNode.SelectNodes(FourFactors)[0].InnerHtml;
            
            
            
            htmlAsTaskpbp.Wait();
            HtmlDocument docpbp = new HtmlDocument();
            docpbp.LoadHtml(htmlAsTaskpbp.Result);
            
            var TimesT = docpbp.DocumentNode.SelectNodes(TiesLeadChanges)[0].InnerHtml;
            var Tot1 = docpbp.DocumentNode.SelectNodes(MostConsecutivePoints)[0].InnerHtml;
            var Tot2 = docpbp.DocumentNode.SelectNodes(LongestScoringDrought)[0].InnerHtml;

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
