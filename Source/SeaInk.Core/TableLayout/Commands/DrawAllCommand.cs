using System.Collections.Generic;
using System.Linq;
using FluentResults;
using Kysect.Centum.Sheets.Indices;
using SeaInk.Core.TableLayout.CommandsBase;
using SeaInk.Core.TableLayout.ComponentsBase;
using SeaInk.Core.TableLayout.Successes;

namespace SeaInk.Core.TableLayout.Commands
{
    public class DrawAllCommand : ILayoutCommand
    {
        private readonly List<LayoutComponent> _visitedComponents = new List<LayoutComponent>();

        public Result Execute(LayoutComponent target, ISheetIndex begin, ITableEditor? editor)
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