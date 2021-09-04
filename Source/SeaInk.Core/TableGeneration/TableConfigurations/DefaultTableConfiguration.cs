using System.Collections.Generic;
using Kysect.CentumFramework.Sheets.Entities;
using Kysect.CentumFramework.Sheets.Models.Indices;
using SeaInk.Core.TableGeneration.ColumnConfigurations;

namespace SeaInk.Core.TableGeneration.TableConfigurations
{
    public class DefaultTableConfiguration : ITableConfiguration
    {
        private readonly IReadOnlyList<ListColumnConfiguration> _columnConfigurations;

        public DefaultTableConfiguration(IReadOnlyList<ListColumnConfiguration> columnConfigurations)
        {
            _columnConfigurations = columnConfigurations;
        }
        
        /// <param name="sheet">sheet for draw a table</param>
        /// <param name="start">start index for draw a table</param>
        /// <returns>index for next column after a table</returns>
        public SheetIndex Draw(Sheet sheet, SheetIndex start)
        {
            foreach(var column in _columnConfigurations)
            {
                start = column.Draw(sheet, start);
            }

            return start;
        }
    } 
}