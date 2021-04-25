using System;
using System.Collections.Generic;
using SeaInk.Core.Entities.Tables;
using SeaInk.Core.Models.Tables;

namespace SeaInk.Sample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ITable table = new ExcelTable();
            table.Create(@"E:\Desktop\Test.xlsx");

            var sheet = new TableIndex("NEW", table.SheetCount + 1);
            
            table.CreateSheet(sheet);
            
            table.SetValuesForCellsAt(sheet, 
                new List<List<string>>
                {
                    new List<string>()
                    {
                        "Проверка", "Вставки"
                    },
                    new List<string>()
                    {
                        "Строковых", "Значений"
                    }
                });
            table.SetValuesForCellsAt(sheet.WithColumn(3), 
                new List<List<int>>
                {
                    new List<int>()
                    {
                        10, 20, 25
                    },
                    new List<int>()
                    {
                        30, 40, 45
                    }
                });
        
            table.Rename("test2.xlsx");
            table.RenameSheet(sheet, "NEW NEW");
            sheet.SheetName = "NEW NEW";

            table.MergeCellsAt(new TableIndexRange(sheet.SheetName, sheet.SheetId, (0, 3), (3, 4)));

            Console.WriteLine("Press ENTER to delete row");
            Console.ReadLine();
            table.DeleteRowAt(sheet.WithRow(1));

            Console.WriteLine("Press ENTER to delete column");
            Console.ReadLine();
            table.DeleteColumnAt(sheet.WithColumn(3));
            
            Console.WriteLine("Press ENTER to delete sheet");
            Console.ReadLine();
            table.DeleteSheet(sheet);
        }
    }
}