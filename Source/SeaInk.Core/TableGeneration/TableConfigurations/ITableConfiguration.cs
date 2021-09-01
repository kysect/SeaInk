using Kysect.CentumFramework.Sheets.Entities;
using Kysect.CentumFramework.Sheets.Models.Indices;

namespace SeaInk.Core.TableGeneration.TableConfigurations
{
    public interface ITableConfiguration
    {
        void Draw(Sheet sheet, SheetIndex start);
    }
}