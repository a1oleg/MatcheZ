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

            ThuuzMatch oms = Repo.GetExist().First();

            var s = oms.Date.Split('/');
            string year = "20" + s[2];
            string month = ("0" + s[0]).Substring(-1, 2) ;
            string day = ("0" + s[1]).Substring(-1, 2);

            string ss = year + month + day;
                //PupMatch fm = new PupMatch(20121104, "ORL");
            BasRefMatch fm = new BasRefMatch(20121104, "ORL");


            
            ////211104 212333  1229     33329 – 34471 = – 1142  
            //for (int i = 265141; i <= 266114; i++)
            //{
            //    ThuuzMatch tm = new ThuuzMatch(i);
            //    if (tm.ThuuzId != 0)
            //        Repo.MergeThuuzMatch(tm);
            //}
                
            
            
         //   var fi = new FileInfo(@"C:\Users\a1ole\YandexDisk\foxmatchez.xlsx");
         //   //var fi = new FileInfo(@"C:\Users\a1ole\YandexDisk\thuuzmatchez.xlsx");

	        //using (var p = new ExcelPackage(fi))
         //   {

         //       var ws = p.Workbook.Worksheets[0];

         //       
         //       int r = 420;

         //       for (int i = 33748; i < 34472; i++) //
         //       {
                    
                    
         //           PupMatch match = new PupMatch(i, true);
                    
         //           ws.Cells[r,1].Value = match.Id;
         //           ws.Cells[r,2].Value = match.Team1;
         //           ws.Cells[r,3].Value = match.Team2;
         //           //ws.Cells[r, 4].Value = match.Rate;
         //           //ws.Cells[r, 5].Value = match.Date;

         //           Console.WriteLine(r);

         //           r++;
         //           p.Save();
         //       }

         //   }
            
        }
    }
}
