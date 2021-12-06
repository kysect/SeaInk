using FluentResults;
using Kysect.Centum.Sheets.Indices;
using SeaInk.Application.TableLayout.CommandInterfaces;
using SeaInk.Application.TableLayout.CommandsBase;
using SeaInk.Utility.Extensions;

namespace SeaInk.Application.TableLayout.Commands
{
    public class GetValueCommand<T> : GenericLayoutCommand<IValueGettingLayoutComponent<T>>
    {
        private readonly ITableDataProvider _provider;

        public GetValueCommand(ITableDataProvider provider)
        {
            _provider = provider.ThrowIfNull();
        }

        public T? Value { get; private set; }

        protected override Result Execute(IValueGettingLayoutComponent<T> target, ISheetIndex begin, ITableEditor? editor)
        {
            Value = target.GetValue(begin, _provider);
            return Result.Ok();
        }
    }
}