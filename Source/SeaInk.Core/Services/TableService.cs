using SeaInk.Core.Entities.Tables;

namespace SeaInk.Core.Services
{
    public class TableService<TTable> where TTable : ITable
    {
        private TTable _table;
        
        public TableService(TTable table)
        {
            _table = table;
        }
    }
}