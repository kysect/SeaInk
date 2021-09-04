using Kysect.CentumFramework.Sheets.Entities;
using Kysect.CentumFramework.Sheets.Models.Indices;

namespace SeaInk.Core.TableGeneration.TableConfigurations
{
    public interface ITableConfiguration
    {
        SheetIndex Draw(Sheet sheet, SheetIndex start);
    }
}