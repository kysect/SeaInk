using System.Collections.Generic;
using System.Linq;
using FluentResults;
using SeaInk.Application.TableLayout.CommandsBase;
using SeaInk.Application.TableLayout.ComponentsBase;
using SeaInk.Application.TableLayout.Errors;
using SeaInk.Application.TableLayout.Indices;
using SeaInk.Application.TableLayout.Successes;

namespace SeaInk.Application.TableLayout.Commands
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

        public Result Execute(LayoutComponent target, ITableIndex begin, ITableEditor? editor)
        {
            if (_ignored.Contains(target))
                return Result.Fail(new ComponentShouldBeIgnoredError(target));

            Result result = target.ExecuteCommand(_command, begin, editor);

            return result.IsSuccess ? result.WithSuccess(new SuccessComponent(target)) : result;
        }
    }
}