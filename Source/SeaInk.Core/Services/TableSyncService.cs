using System;
using System.Collections.Generic;
using System.IO;
using ClosedXML.Excel;

namespace SeaInk.Core.Services
{
    public class TableSyncService
    {
        //Для тестиварония
        public static void GenerateTableExample()
        {
            List<string> a = new List<string>();
            a.Add("Иванов Иван");
            a.Add("Шевченко Валерий");
            a.Add("Полухин Максим");
            a.Add("Круглов Георгий");
            a.Add("Повышев Владислав");
            a.Add("Сазиков Михаил");
            a.Add("Вититнёв Кирилл");
            GenerateTable("Prog", "M3113", a);
        }
        
        //Позже можно будет давать методу на вводу просто класс Группа
        public static void GenerateTable(string subject, string groupName, List<string> names)
        {
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add(subject + groupName);
            
            worksheet.Cell("A" + 1).Value = "ФИО";
            worksheet.Cell("A" + 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            worksheet.Cell("A" + 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            worksheet.Cell("A" + 1).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
            worksheet.Cell("A" + 1).Style.Border.LeftBorder = XLBorderStyleValues.Thick;
            worksheet.Cell("A" + 1).Style.Border.RightBorder = XLBorderStyleValues.Thick;
            worksheet.Cell("A" + 1).Style.Border.TopBorder = XLBorderStyleValues.Thick;

            var k = 2;
            var max = 0;
            foreach (var i in names)
            {
                worksheet.Cell("A" + k).Value = i;
                if (i.Length > max)
                {
                    max = i.Length;
                }

                worksheet.Cell("A" + k).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Cell("A" + k).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                worksheet.Cell("A" + k).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                k++;
            }
            worksheet.Column("A").Width = max + 1;
            
            //Потом можно будет сделать выплёвывание этой таблички на сервер или куда там будет удобней
            var path = @"E:\ITMO prog\Projects\Sea-ink\Docs\" + subject + groupName + ".xlsx";
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            string s = path;
            workbook.SaveAs(path);
            }

        public void SyncGroup(string subject, string groupName)
        {
            throw new NotImplementedException();
        }

        public void SendPoints(string subject, string groupName)
        {
            throw new NotImplementedException();
        }
    }
}