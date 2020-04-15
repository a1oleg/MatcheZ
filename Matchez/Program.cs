using HtmlAgilityPack;
using OfficeOpenXml;
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
            ThuuzMatch tm = new ThuuzMatch(211105);
            
            
         //   var fi = new FileInfo(@"C:\Users\a1ole\YandexDisk\foxmatchez.xlsx");
         //   //var fi = new FileInfo(@"C:\Users\a1ole\YandexDisk\thuuzmatchez.xlsx");

	        //using (var p = new ExcelPackage(fi))
         //   {

         //       var ws = p.Workbook.Worksheets[0];

         //       //211104 212333  1229     33329 – 34471 = – 1142  
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
