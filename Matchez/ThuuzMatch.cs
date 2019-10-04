using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;

namespace Matchez
{
    class ThuuzMatch
    {
        public static HtmlWeb hw = new HtmlWeb();

        static readonly string ThuuzLink = "https://www.foxsports.com/nba/boxscore?id=";
        public int Rate { get; set; }

        private object Team1;
        private object Team2;

        public int Id { get; set; }
        public HtmlDocument ThuuzPage { get; set; }

        static readonly string ThuuzRatingXpath = "//*[@id=\"circle\"]/div/div";

        static readonly string ThuuzTeam1Xpath = "//*[@id=\"game-summary\"]/div[1]/table/tbody/tr[3]/td[1]/div/span";

        static readonly string ThuuzTeam2Xpath = "//*[@id=\"game-summary\"]/div[1]/table/tbody/tr[4]/td[1]/div/span";


        public ThuuzMatch(int _id)
        {
            Id = _id;
            string url = ThuuzLink + _id;

            ThuuzPage = hw.Load(url);
            
            Rate = Int32.Parse(ThuuzPage.DocumentNode.SelectSingleNode(ThuuzRatingXpath).InnerText);


            Team1 = ThuuzPage.DocumentNode.SelectSingleNode(ThuuzTeam1Xpath).InnerHtml;
            Team2 = ThuuzPage.DocumentNode.SelectSingleNode(ThuuzTeam2Xpath).InnerHtml;


        }
    }
}
