﻿using Kysect.CentumFramework.Sheets.Entities;
using Kysect.CentumFramework.Sheets.Models.Indices;

namespace SeaInk.Core.TableGeneration.ColumnConfigurations
{
    public abstract class ColumnConfigurationBase
    {
        public SheetIndex Draw(Sheet sheet, SheetIndex start)
        {
            SheetIndex index = DrawHeader(sheet, start);
            index = DrawBody(sheet, index);

            return start with
            {
                Column = index.Column
            };
        }
        
        protected abstract SheetIndex DrawHeader(Sheet sheet, SheetIndex start);
        protected abstract SheetIndex DrawBody(Sheet sheet, SheetIndex start);
    }
}