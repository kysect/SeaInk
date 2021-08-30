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
        private readonly ColumnConfiguration _solumnConfiguration;

        private readonly Sheet _sheet;

        public ColumnGenerator(ColumnConfiguration columnConfiguration, Sheet sheet)
        {
            _solumnConfiguration = columnConfiguration;
            _sheet = sheet;
        }
        
        public void CreateColumn()
        {
            _sheet.SetRangeStyle(_solumnConfiguration.Range, new DefaultCellStyle
            {
                BorderStyle = new BorderStyle(new LineConfiguration(Color.Black, LineStyle.Bold))
            });
            
            _sheet[_solumnConfiguration.Range] = new List<IReadOnlyList<object>>
            {
                new List<object> { _solumnConfiguration.Title }
            };
            
            var dataIndex = new SheetIndexRange(new SheetIndex(_solumnConfiguration.Range.From.Column, new RowIndex(2)));
            _sheet[dataIndex] = new List<IReadOnlyList<object>>
            {
                new List<object> { _solumnConfiguration.DeadLine }
            };
        }

        public void UpdateColumn(IReadOnlyList<string> students)
        {
            switch (_solumnConfiguration.Type)
            {
                case ColumnType.Milestones:
                    _sheet.SetRangeStyle(_solumnConfiguration.Range, new DefaultCellStyle
                    {
                        BorderStyle = new BorderStyle(new LineConfiguration(Color.Black, LineStyle.Bold))
                    });
                    break;
                
                case ColumnType.Results:
                    _sheet.SetRangeStyle(_solumnConfiguration.Range, new DefaultCellStyle
                    {
                        BorderStyle = new BorderStyle(new LineConfiguration(Color.Black, LineStyle.Bold))
                    });
                    break;
                
                case ColumnType.Ordinaries:
                    _sheet.SetRangeStyle(_solumnConfiguration.Range, new DefaultCellStyle
                    {
                        BorderStyle = new BorderStyle(new LineConfiguration(Color.Black, LineStyle.Light))
                    });
                    break;
                
                case ColumnType.Students:
                    _sheet.SetRangeStyle(_solumnConfiguration.Range, new DefaultCellStyle
                    {
                        BorderStyle = new BorderStyle(new LineConfiguration(Color.Black, LineStyle.Light))
                    });

                    _sheet[_solumnConfiguration.Range] = students.Select(s => new List<object> {s}).ToList();
                    //Лямбда ибо в Гошенной библиотеке по другому не работает -_-
                    break;
            }
            
            
            
        }
    }
}