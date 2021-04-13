using System;
using SeaInk.Core.Models.Tables.Tables;

namespace SeaInk.Sample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            for (int i = 26; i < 64; ++i) 
                Console.WriteLine(TableIndex.ColumnStringFromInt(i));
            
            // Console.WriteLine(TableIndex.ColumnStringFromInt(26));
        }
    }
}