using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;

namespace Matchez
{
    public class BasRefMatch
    {
        static string BasRefLink = "https://www.basketball-reference.com/boxscores/";
        static string BasRefLinkpbp = "https://www.basketball-reference.com/boxscores/pbp/";

        static string FourFactors = "/html/body/div[2]/div[4]/div[5]/div[2]/div/div[3]/div/table/tbody";
        
        static string TiesLeadChanges = "/html/body/div[2]/div[4]/div[5]/div[3]/div/div[1]/table/tbody";
        static string MostConsecutivePoints = "/html/body/div[2]/div[4]/div[5]/div[3]/div/div[2]/table/tbody";
        static string LongestScoringDrought = "/html/body/div[2]/div[4]/div[5]/div[3]/div/div[3]/table/tbody";

        
        public BasRefMatch(int date, string home)
        {
            string url = BasRefLink + date + '0' + home.ToUpper() + ".html";
            var web = new HtmlWeb();
            var doc = web.Load(url);

            var LeadC = doc.DocumentNode.SelectNodes(FourFactors);//[0].InnerHtml;
            var LeadC2 = doc.DocumentNode.SelectNodes("//*[@id=\"four_factors\"]");  //[0].InnerHtml;


            string urlpbp = BasRefLinkpbp + date + '0' + home.ToUpper() + ".html";

            var web2 = new HtmlWeb();
            var doc2 = web2.Load(urlpbp);
            var TimesTNN = doc2.DocumentNode.SelectNodes("//*[@id=\"st_0\"]")[0].InnerHtml;
            var TimesT = doc2.DocumentNode.SelectNodes(TiesLeadChanges)[0].InnerHtml;
            var Tot1 = doc2.DocumentNode.SelectNodes(MostConsecutivePoints)[0].InnerHtml;
            var Tot2 = doc2.DocumentNode.SelectNodes(LongestScoringDrought)[0].InnerHtml;
        }      
        

    }

}
