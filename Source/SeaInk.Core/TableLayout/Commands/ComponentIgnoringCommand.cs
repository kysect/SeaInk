using System.Collections.Generic;
using System.Linq;
using FluentResults;
using Kysect.Centum.Sheets.Indices;
using SeaInk.Core.TableLayout.CommandsBase;
using SeaInk.Core.TableLayout.ComponentsBase;
using SeaInk.Core.TableLayout.Errors;
using SeaInk.Core.TableLayout.Successes;

namespace SeaInk.Core.TableLayout.Commands
{
    public class ComponentIgnoringCommand : ILayoutCommand
    {
        private readonly ILayoutCommand _command;
        private readonly IReadOnlyCollection<LayoutComponent> _ignored;

        public ComponentIgnoringCommand(ILayoutCommand command, IReadOnlyCollection<LayoutComponent> ignored)
        {
            _command = command;
            _ignored = ignored.ToList();
        }

        public Result Execute(LayoutComponent target, ISheetIndex begin, ITableEditor? editor)
        {
            if (_ignored.Contains(target))
                return Result.Fail(new ComponentShouldBeIgnoredError(target));

            Result result = target.ExecuteCommand(_command, begin, editor);

            return result.IsSuccess ? result.WithSuccess(new SuccessComponent(target)) : result;
        }
    }
}