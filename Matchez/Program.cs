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
            var fi = new FileInfo(@"C:\Users\a1ole\YandexDisk\foxmatchez.xlsx");

	        using (var p = new ExcelPackage(fi))
            {
                
                var ws = p.Workbook.Worksheets[0];

                //211104 - 211201-211699 212333  1229     33329   34472
                int r = 306;

                for (int i = 33634; i < 34472; i++) //
                {
                    FoxPupMatch match = new FoxPupMatch(i);

                    ws.Cells[r,1].Value = match.Id;
                    ws.Cells[r,2].Value = match.Team1;
                    ws.Cells[r,3].Value = match.Team2;

                    Console.WriteLine(r);

                    r++;
                    p.Save();
                }

                
                
                //p.SaveAs(new FileInfo(@"C:\Users\a1ole\Desktop\myworkbook.xlsx"));
            }
            
        }
    }
}
