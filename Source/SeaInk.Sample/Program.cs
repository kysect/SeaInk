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
            var info = new TableInfo("test");
            table.Create(info, new List<string> {"Test", "a"});

            var sheetIndex = new SheetIndex("NEW", table.SheetCount + 1);

            try
            {
                table.CreateSheet(sheetIndex);
                table.SetValuesForCellsAt(new TableIndex(sheetIndex), 
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
            
                table.SetValuesForCellsAt(new TableIndex(sheetIndex, 3), 
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
                table.RenameSheet(sheetIndex, "NEW NEW");
                table.FormatCellsAt(new TableIndexRange(new TableIndex(sheetIndex, 3, 1)), new DefaultCellStyle
                {
                    BackgroundColor = Color.Aqua
                });
                table.FormatCellAt(new TableIndex(sheetIndex, 3), new DefaultCellStyle
                {
                    BackgroundColor = Color.Red
                });
                table.MergeCellsAt(new TableIndexRange(sheetIndex.SheetName, sheetIndex.SheetId, (0, 3), (0, 4)));

                Console.WriteLine("Press ENTER to delete row");
                Console.ReadLine();
                table.DeleteRowAt(new TableIndex(sheetIndex, 0, 1));
                
                Console.WriteLine("Press ENTER to delete column");
                Console.ReadLine();
                table.DeleteColumnAt(new TableIndex(sheetIndex, 3));
                
                Console.WriteLine("Press ENTER to delete sheet");
                Console.ReadLine();
                table.DeleteSheet(sheetIndex);
                
                Console.WriteLine("Press ENTER to delete file");
                Console.ReadLine();
                table.Delete();
            }
            catch (Exception)
            {
                table.DeleteSheet(sheetIndex);
                table.Delete();
                throw;
            }
        }
    }
}