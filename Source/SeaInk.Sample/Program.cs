using System.Collections.Generic;
using SeaInk.Core.Entities.Tables;
using SeaInk.Core.Models.Tables;

namespace SeaInk.Sample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var table = new GoogleTable();
            table.Load("1Z7JaEecXh5K6NPwrRuZC0OfM3TMCzHt-_POD2812J_k");
            
            table.SetValuesForCellsAt(new TableIndex("Test", 1, 1, 1), new List<List<string>>
            {
                new ()
                {
                    "почему", "List<List<", "object>> н"
                },
                new ()
                {
                    "апкастится", "в", "IList<IList<", "object>>?"
                }
            });
            
            // Console.WriteLine(table.Create("Name"));
        }
    }
}