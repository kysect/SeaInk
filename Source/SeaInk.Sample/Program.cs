using System;
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
            
            table.DeleteSheet(new TableIndex("", 2));
            
            // Console.WriteLine(table.Create("Name"));
        }
    }
}