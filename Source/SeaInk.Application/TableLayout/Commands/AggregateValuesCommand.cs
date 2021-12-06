using System.Collections.Generic;
using System.Linq;
using FluentResults;
using Kysect.Centum.Sheets.Indices;
using SeaInk.Application.TableLayout.CommandsBase;
using SeaInk.Application.TableLayout.ComponentsBase;
using SeaInk.Application.TableLayout.Successes;
using SeaInk.Utility.Extensions;

namespace SeaInk.Application.TableLayout.Commands
{
    public class AggregateValuesCommand<T> : ILayoutCommand
    {
        private readonly List<T> _values = new List<T>();
        private readonly ITableDataProvider _provider;
        private readonly List<LayoutComponent> _visitedComponents = new List<LayoutComponent>();

        public AggregateValuesCommand(ITableDataProvider provider)
        {
            _provider = provider;
        }

        public IReadOnlyCollection<T> Values => _values.AsReadOnly();

        public Result Execute(LayoutComponent target, ISheetIndex begin, ITableEditor? editor)
        {
            var command = new GetValueCommand<T>(_provider);
            Result result = target.ExecuteCommand(new ComponentIgnoringCommand(command, _visitedComponents), begin, editor);

            while (result.IsSuccess)
            {
                _values.Add(command.Value.ThrowIfNull());
                var success = (SuccessComponent)result.Successes.Single();
                _visitedComponents.Add(success.Component);

                result = target.ExecuteCommand(new ComponentIgnoringCommand(command, _visitedComponents), begin, editor);
            }

            return Result.Ok();
        }
    }
}