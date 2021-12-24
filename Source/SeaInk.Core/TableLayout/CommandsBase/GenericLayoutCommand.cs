using FluentResults;
using Kysect.Centum.Sheets.Indices;
using SeaInk.Core.TableLayout.ComponentsBase;
using SeaInk.Core.TableLayout.Errors;

namespace SeaInk.Core.TableLayout.CommandsBase
{
    public abstract class GenericLayoutCommand<T> : ILayoutCommand
    {
        public Result Execute(LayoutComponent target, ISheetIndex begin, ISheetEditor? editor)
        {
            return target switch
            {
                T typedComponent => Execute(typedComponent, begin, editor),
                not T => Result.Fail(new InvalidCommandHandlerError<T>(target)),
            };
        }

        protected abstract Result Execute(T target, ISheetIndex begin, ISheetEditor? editor);
    }
}