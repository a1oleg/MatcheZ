using HtmlAgilityPack;
using OfficeOpenXml;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Matchez
{
    class Program
    {
        static void Main(string[] args)
        {

            Repo.StartLocale();

            var oms = Repo.GetExist("Orlando Magic");
            //Orlando Magic
            foreach (ThuuzMatch tm in oms)
            {
                var s = tm.Date.Split('/');
                string year = "20" + s[2];
                string month = s[0].Length == 1 ? ("0" + s[0]) : s[0];
                string day = s[1].Length == 1 ? ("0" + s[1]) : s[1];

                string ss = year + month + day;

                BasRefMatch fm = new BasRefMatch(ss, tm.T2s);

                Repo.MergeBasRefMatch(fm, tm.Id);
            }         


        }
    }
}
