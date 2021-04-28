using System;
using Google.Apis.Sheets.v4.Data;

namespace SeaInk.Core.TableIntegrations.Models
{
    public class SheetIndex
    {
        public string Name { get; set; }
        public int Id { get; set; }

        public SheetIndex(string name, int id)
        {
            Name = name;
            Id = id;
        }

        public bool Equals(SheetIndex other)
            => Name == other.Name && Id == other.Id;

        public override bool Equals(object obj)
            => obj is SheetIndex sheet && Equals(sheet);

        public override int GetHashCode()
            => HashCode.Combine(Id);
    }

    public static class GoogleTableIndexExtension
    {
        public static SheetProperties ToGoogleSheetProperties(this SheetIndex index)
            => new SheetProperties
            {
                Title = index.Name,
                SheetId = index.Id,
                Index = index.Id
            };
    }
}