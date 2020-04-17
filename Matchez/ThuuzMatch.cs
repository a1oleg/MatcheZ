using System;
using System.Linq;
using System.Collections.Generic;
using HtmlAgilityPack;

namespace Matchez
{

    public class ThuuzMatch
    {
        static string ThuuzLink = "https://www.thuuz.com/basketball/nba/game/";

        static string Thuuz2dgraphLink = "https://www.thuuz.com/2dgraph/";

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

            if(this.ThuuzId != 0)
                GetTeamsDates(urlTD);
        }        

        public void GetGec(String url)
        {
            var web = new HtmlWeb();
            var doc = web.Load(url);
            var arrs = doc.Text.Split("_data = \"");
            try
            {
                var var_gecL = arrs[1].Split(',').Take(50).ToList();
                this.gec = Remover(var_gecL);
                if (gec.Count() > 0)
                    this.avgec = (int)gec.Average();
                var var_gec_awayL = arrs[2].Split(',').Take(50).ToList();
                this.gec_away = Remover(var_gec_awayL);
                var var_gec_homeL = arrs[3].Split(',').Take(50).ToList();
                this.gec_home = Remover(var_gec_homeL);

            }
            catch
            {
                this.ThuuzId = 0;
            }
            
        }

        public void GetTeamsDates(String url)
        {
            var web = new HtmlWeb();
            var doc = web.Load(url);

           
            try
            {
                Date = doc.Text.Split("Final - ")[1].Substring(0, 8);
            }
            catch
            {
                try //"Upcoming - 7:00 PM (EDT) 11/1/12
                {
                    Date = doc.Text.Split("DT) ")[1].Substring(0, 8);
                }
                catch
                { //"Upcoming - 7:00 PM (EST) 11/1/12
                    Date = doc.Text.Split("ST) ")[1].Substring(0, 8);
                }                
            }
            finally
            {
                Date = Date.Trim();
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
        
    }
}
