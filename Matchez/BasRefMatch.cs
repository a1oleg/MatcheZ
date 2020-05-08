using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;

namespace Matchez
{
    public class BasRefMatch
    {
        static string BasRefLink = "https://www.basketball-reference.com/boxscores/";
        static string BasRefLinkpbp = "https://www.basketball-reference.com/boxscores/pbp/";
        public string url { get; set; }
        public int Ties { get; set; }
        public int Leadchanges { get; set; }
        public string Gametied { get; set; }
        public string awayled { get; set; }
        public string homeled { get; set; }
        public int awaymcp { get; set; }
        public int homemcp { get; set; }
        public string awaylsd { get; set; }
        public string homelsd { get; set; }
        
        public double Pace { get; set; }
        public double eFG { get; set; }
        public double TOV { get; set; }
        public double ORB { get; set; }
        public double FTFGA { get; set; }
        public double ORtg { get; set; }

        public double Pace2 { get; set; }
        public double eFG2 { get; set; }
        public double TOV2 { get; set; }
        public double ORB2 { get; set; }
        public double FTFGA2 { get; set; }
        public double ORtg2 { get; set; }

        
    


        public BasRefMatch(string date, string home)
        {
            url = BasRefLink + date + '0' + home.ToUpper() + ".html";
            string urlpbp = BasRefLinkpbp + date + '0' + home.ToUpper() + ".html";


            var web = new HtmlWeb();
            var doc0 = web.LoadFromWebAsync(url);

            var web2 = new HtmlWeb();
            var doc2 = web2.LoadFromWebAsync(urlpbp);

            doc0.Wait();
            var doc1 = doc0.Result;

            string[] x = null;
            try
            {
                x = doc1.Text.Split("Four Factors")[8].Split("Game")[0].Split("</td>");
            }
            catch
            {
                home = "CHO";
                url = BasRefLink + date + '0' + home.ToUpper() + ".html";
                urlpbp = BasRefLinkpbp + date + '0' + home.ToUpper() + ".html";


                 web = new HtmlWeb();
                 doc0 = web.LoadFromWebAsync(url);

                 web2 = new HtmlWeb();
                 doc2 = web2.LoadFromWebAsync(urlpbp);

                doc0.Wait();
                doc1 = doc0.Result;
                x = doc1.Text.Split("Four Factors")[8].Split("Game")[0].Split("</td>");

            }
            finally
            {
                Pace = Double.Parse(x[0].Split(">").Last().Replace('.', ','));
                eFG = Double.Parse('0' + x[1].Split(">").Last().Replace('.', ','));
                TOV = Double.Parse(x[2].Split(">").Last().Replace('.', ','));
                ORB = Double.Parse(x[3].Split(">").Last().Replace('.', ','));
                FTFGA = Double.Parse('0' + x[4].Split(">").Last().Replace('.', ','));
                ORtg = Double.Parse(x[5].Split(">").Last().Replace('.', ','));

                Pace2 = Double.Parse(x[6].Split(">").Last().Replace('.', ','));
                eFG2 = Double.Parse('0' + x[7].Split(">").Last().Replace('.', ','));
                TOV2 = Double.Parse(x[8].Split(">").Last().Replace('.', ','));
                ORB2 = Double.Parse(x[9].Split(">").Last().Replace('.', ','));
                FTFGA2 = Double.Parse('0' + x[10].Split(">").Last().Replace('.', ','));
                ORtg2 = Double.Parse(x[11].Split(">").Last().Replace('.', ','));


                doc2.Wait();
                var doc3 = doc2.Result;
                var y = doc3.Text.Split("placeholder").Last().Split("</td></tr>");//.Take(9);

                Ties = Int32.Parse(y[0].Split(">").Last());
                Leadchanges = Int32.Parse('0' + y[1].Split(">").Last());
                Gametied = y[2].Split(">").Last();
                awayled = y[3].Split(">").Last();
                homeled = y[4].Split(">").Last();
                awaymcp = Int32.Parse('0' + y[5].Split(">").Last());
                homemcp = Int32.Parse('0' + y[6].Split(">").Last());
                awaylsd = y[7].Split(">").Last();
                homelsd = y[8].Split(">").Last();

            }
            

        }


    }

}
