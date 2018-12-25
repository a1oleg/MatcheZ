using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Matchez
{
    class Program
    {
        static void Main(string[] args)
        {
            HtmlWeb hw = new HtmlWeb();

            HtmlDocument Page = hw.Load("http://720pier.ru/viewforum.php?f=34");

            string Xpath = "html/body/div/div/div[5]/div/ul[2]/li";                        

            List<HtmlNode> nodes = Page.DocumentNode.SelectNodes(Xpath).ToList();

            

            List<Game> games = new List<Game>();
            for(int i = 1; i < nodes.Count(); i++)
            {
                HtmlNode a = nodes.ElementAt(i).SelectSingleNode(nodes.ElementAt(i).XPath + "/dl/dt/div/a");
                games.Add(new Game(a.OuterHtml));                

            }


        }
    }
}
