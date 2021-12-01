using System.Collections.Generic;
using SeaInk.Application.TableLayout.CommandInterfaces;
using SeaInk.Application.TableLayout.CommandsBase;
using SeaInk.Application.TableLayout.Indices;
using SeaInk.Utility.Extensions;

namespace SeaInk.Application.TableLayout.Commands
{
    public class AggregateValuesCommand<T> : GenericLayoutCommand<IValueGettingLayoutComponent<T>>
    {
        private readonly List<T> _values = new List<T>();
        private readonly ITableDataProvider _provider;

        public AggregateValuesCommand(ITableDataProvider provider)
        {
            _provider = provider.ThrowIfNull(nameof(provider));
        }

        public IReadOnlyCollection<T> Values => _values.AsReadOnly();

        protected override bool TryExecute(IValueGettingLayoutComponent<T> target, ITableIndex begin, ITableEditor? editor)
        {
            _values.Add(target.GetValue(begin, _provider));
            return false;
        }
    }
}