using System.Collections.Generic;
using System.Linq;
using FluentResults;
using SeaInk.Application.TableLayout.CommandsBase;
using SeaInk.Application.TableLayout.ComponentsBase;
using SeaInk.Application.TableLayout.Indices;
using SeaInk.Application.TableLayout.Successes;

namespace SeaInk.Application.TableLayout.Commands
{
    public class DrawAllCommand : ILayoutCommand
    {
        private readonly List<LayoutComponent> _visitedComponents = new List<LayoutComponent>();

        public Result Execute(LayoutComponent target, ITableIndex begin, ITableEditor? editor)
        {
            var command = new DrawComponentCommand();
            Result result = target.ExecuteCommand(new ComponentIgnoringCommand(command, _visitedComponents), begin, editor);

            while (result.IsSuccess)
            {
                var success = (SuccessComponent)result.Successes.Single();
                _visitedComponents.Add(success.Component);
                result = target.ExecuteCommand(new ComponentIgnoringCommand(command, _visitedComponents), begin, editor);
            }

            return Result.Ok();
        }
    }
}