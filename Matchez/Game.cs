using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Matchez
{
    public class Game
    {
        public string Ref720 { get; set; }

        public HtmlWeb hw = new HtmlWeb();

        public string ThuuzPrefix { get; set; }
        public string ThuuzId { get; set; } //211104 211201 211699


        public HtmlDocument ThuuzPage { get; set; }//http://www.thuuz.com/basketball/nba/game

        static readonly string ThuuzRatingXpath = "//*[@id=\"circle\"]/div/div";
        public int rating;
        
        public Team Team1 { get; set; }
        public Team Team2 { get; set; }

        static readonly string ThuuzTeam1Xpath = "/html/body/div[2]/div[3]/div[1]/table/tbody/tr[1]/td[2]/div";
        static readonly string ThuuzTeam2Xpath = "/html/body/div[2]/div[3]/div[1]/table/tbody/tr[2]/td[1]/div";

        public Game(string outerHtml)
        {
            var x = outerHtml.Split('/', StringSplitOptions.RemoveEmptyEntries).ToList();
            string y = x.ElementAt(1).Split(' ').First();
            Ref720 = y.Remove(y.Count() - 1);
            var teams = x.ElementAt(4).Split('[').First().Trim().Split('@');
        }
       
        public void GetThuuzPage() => hw.Load(ThuuzPrefix + ThuuzId);

        public bool GetThuuzRating() => Int32.TryParse(ThuuzPage.DocumentNode.SelectSingleNode(ThuuzRatingXpath).InnerText, out rating);
        

        public void GetThuuzTeams()
        {
            Team1 = new Team(ThuuzPage.DocumentNode.SelectSingleNode(ThuuzTeam1Xpath).InnerText);
            Team2 = new Team(ThuuzPage.DocumentNode.SelectSingleNode(ThuuzTeam2Xpath).InnerText);


        }


    }
}
