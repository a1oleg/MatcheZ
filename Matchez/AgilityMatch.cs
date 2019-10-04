using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;

namespace Matchez
{
    class AgilityMatch
    {
        public static HtmlWeb hw = new HtmlWeb();

        static readonly string ThuuzLink = "https://www.thuuz.com/basketball/nba/game/";
        public int Rate { get; set; }
        public string Date { get; set; }

        public string Team1 { get; set; }
        public string Team2 { get; set; }

        public int Id { get; set; }
        public HtmlDocument ThuuzPage { get; set; }

        static readonly string RatingXpath = "/html/body/div[2]/div[3]/div[1]/table/tbody/tr[1]/td[1]/div[1]/div[1]";
        //static readonly string RatingXpath = "//*[@id=\"circle\"]/div/div";

        static readonly string DateXpath = "/html/body/div[2]/div[2]/table/tbody/tr/td[2]";

        static readonly string Team1Xpath = "/html/body/div[2]/div[3]/div[1]/table/tbody/tr[1]/td[2]/div";

        static readonly string Team2Xpath = "/html/body/div[2]/div[3]/div[1]/table/tbody/tr[2]/td[1]/div";


        public AgilityMatch(int _id)
        {
            Id = _id;
            string url = ThuuzLink + _id;

            ThuuzPage = hw.Load(url);
            
            Rate = Int32.Parse(ThuuzPage.DocumentNode.SelectSingleNode(RatingXpath).InnerText);

            Date = ThuuzPage.DocumentNode.SelectSingleNode(DateXpath).InnerText;


            Team1 = ThuuzPage.DocumentNode.SelectSingleNode(Team1Xpath).InnerText;
            Team2 = ThuuzPage.DocumentNode.SelectSingleNode(Team2Xpath).InnerText;


        }
    }
}
