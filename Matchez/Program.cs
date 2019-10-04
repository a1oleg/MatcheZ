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
            //var fi = new FileInfo(@"C:\Users\a1ole\YandexDisk\foxmatchez.xlsx");
            var fi = new FileInfo(@"C:\Users\a1ole\YandexDisk\thuuzmatchez.xlsx");

	        using (var p = new ExcelPackage(fi))
            {

                //var ws = p.Workbook.Worksheets[0];
                var ws = p.Workbook.Worksheets.Add("MySheet");

                //211104 - *211201-211699* 212333  1229     *33329-33747*  419   34472
                int r = 1;

                for (int i = 211104; i < 211201; i++) //
                {
                    PupMatch match = new PupMatch(i, false);
                    
                    ws.Cells[r,1].Value = match.Id;
                    ws.Cells[r,2].Value = match.Team1;
                    ws.Cells[r,3].Value = match.Team2;
                    ws.Cells[r,4].Value = match.Rate;
                    ws.Cells[r,5].Value = match.Date;

                    Console.WriteLine(r +' ' + i);

                    r++;
                    p.Save();
                }

                
                
                //p.SaveAs(new FileInfo(@"C:\Users\a1ole\Desktop\myworkbook.xlsx"));
            }
            
        }
    }
}
