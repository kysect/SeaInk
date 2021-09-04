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

        /// <summary>
        /// Method draws a table from column's start index
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="start"></param>
        /// <returns>index for next column</returns>
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