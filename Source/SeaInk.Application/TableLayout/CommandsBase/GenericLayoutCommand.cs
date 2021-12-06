using FluentResults;
using Kysect.Centum.Sheets.Indices;
using SeaInk.Application.TableLayout.ComponentsBase;
using SeaInk.Application.TableLayout.Errors;

namespace SeaInk.Application.TableLayout.CommandsBase
{
    public abstract class GenericLayoutCommand<T> : ILayoutCommand
    {
        public Result Execute(LayoutComponent target, ISheetIndex begin, ITableEditor? editor)
        {
            return target switch
            {
                T typedComponent => Execute(typedComponent, begin, editor),
                not T => Result.Fail(new InvalidCommandHandlerError<T>(target)),
            };
        }

        protected abstract Result Execute(T target, ISheetIndex begin, ITableEditor? editor);
    }
}