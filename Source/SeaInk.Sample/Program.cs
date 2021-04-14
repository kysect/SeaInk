using System;
using SeaInk.Core.Models.Tables;

namespace SeaInk.Sample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var range = new TableIndexRange("A", 0, (0, 0), (5, 5));

            foreach (TableIndex index in range)
            {
                Console.WriteLine(index);
            }
        }
    }
}