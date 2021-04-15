using System;
using System.Collections.Generic;
using System.Drawing;
using SeaInk.Core.Entities.Tables;
using SeaInk.Core.Models.Tables;
using SeaInk.Core.Models.Tables.Styles;

namespace SeaInk.Sample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ITable table = new GoogleTable();
            table.Load("1Z7JaEecXh5K6NPwrRuZC0OfM3TMCzHt-_POD2812J_k");

            var sheet = new TableIndex("NEW", table.SheetCount + 1);

            try
            {
                table.CreateSheet(sheet);
                table.SetValuesForCellsAt(sheet, 
                    new List<List<string>>
                    {
                        new ()
                        {
                            "Проверка", "Вставки"
                        },
                        new ()
                        {
                            "Строковых", "Значений"
                        }
                    });
            
                table.SetValuesForCellsAt(sheet.WithColumn(3), 
                    new List<List<int>>
                    {
                        new ()
                        {
                            10, 20
                        },
                        new ()
                        {
                            30, 40
                        }
                    });
            
                table.Rename("rename test");
                table.RenameSheet(sheet, "NEW NEW");
                table.FormatCellsAt(new TableIndexRange(sheet, sheet.WithColumn(1).WithRow(1)), new DefaultCellStyle
                {
                    BackgroundColor = Color.Aqua
                });
                table.FormatCellAt(sheet.WithColumn(3), new DefaultCellStyle
                {
                    BackgroundColor = Color.Red
                });
                table.MergeCellsAt(new TableIndexRange(sheet.SheetName, sheet.SheetId, (0, 3), (0, 4)));

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
            catch (Exception)
            {
                table.DeleteSheet(sheet);
                throw;
            }
        }
    }
}