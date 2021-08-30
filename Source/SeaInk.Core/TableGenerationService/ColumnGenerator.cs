using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Kysect.CentumFramework.Sheets.Entities;
using Kysect.CentumFramework.Sheets.Models.CellStyle;
using Kysect.CentumFramework.Sheets.Models.CellStyle.Enums;
using Kysect.CentumFramework.Sheets.Models.Indices;
using SeaInk.Core.Entities;
using SeaInk.Core.Models;

namespace SeaInk.Core.TableGenerationService
{
    public class ColumnGenerator
    {
        public ColumnConfiguration ColumnConfiguration { get; }
        
        public Sheet Sheet { get; }

        public ColumnGenerator(ColumnConfiguration columnConfiguration, Sheet sheet)
        {
            ColumnConfiguration = columnConfiguration;
            Sheet = sheet;
        }
        
        public void CreateColumn()
        {
            Sheet.SetRangeStyle(ColumnConfiguration.Range, new DefaultCellStyle
            {
                BorderStyle = new BorderStyle(new LineConfiguration(Color.Black, LineStyle.Bold))
            });
            
            Sheet[ColumnConfiguration.Range] = new List<IReadOnlyList<object>>
            {
                new List<object> { ColumnConfiguration.Title }
            };
            
            var dataIndex = new SheetIndexRange(new SheetIndex(ColumnConfiguration.Range.From.Column, new RowIndex(2)));
            Sheet[dataIndex] = new List<IReadOnlyList<object>>
            {
                new List<object> { ColumnConfiguration.DeadLine }
            };
        }

        public void UpdateColumn(IReadOnlyList<string> students)
        {
            switch (ColumnConfiguration.Type)
            {
                case ColumnType.Milestones:
                    Sheet.SetRangeStyle(ColumnConfiguration.Range, new DefaultCellStyle
                    {
                        BorderStyle = new BorderStyle(new LineConfiguration(Color.Black, LineStyle.Bold))
                    });
                    break;
                
                case ColumnType.Results:
                    Sheet.SetRangeStyle(ColumnConfiguration.Range, new DefaultCellStyle
                    {
                        BorderStyle = new BorderStyle(new LineConfiguration(Color.Black, LineStyle.Bold))
                    });
                    break;
                
                case ColumnType.Ordinaries:
                    Sheet.SetRangeStyle(ColumnConfiguration.Range, new DefaultCellStyle
                    {
                        BorderStyle = new BorderStyle(new LineConfiguration(Color.Black, LineStyle.Light))
                    });
                    break;
                
                case ColumnType.Students:
                    Sheet.SetRangeStyle(ColumnConfiguration.Range, new DefaultCellStyle
                    {
                        BorderStyle = new BorderStyle(new LineConfiguration(Color.Black, LineStyle.Light))
                    });

                    Sheet[ColumnConfiguration.Range] = students.Select(s => new List<object> {s}).ToList();
                    //Лямбда ибо в Гошенной библиотеке по другому не работает -_-
                    break;
            }
            
            
            
        }
    }
}