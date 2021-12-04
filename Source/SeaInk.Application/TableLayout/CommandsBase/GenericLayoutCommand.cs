using FluentResults;
using SeaInk.Application.TableLayout.ComponentsBase;
using SeaInk.Application.TableLayout.Errors;
using SeaInk.Application.TableLayout.Indices;

namespace SeaInk.Application.TableLayout.CommandsBase
{
    public abstract class GenericLayoutCommand<T> : ILayoutCommand
    {
        public Result Execute(LayoutComponent target, ITableIndex begin, ITableEditor? editor)
        {
            return target switch
            {
                T typedComponent => Execute(typedComponent, begin, editor),
                not T => Result.Fail(new InvalidCommandHandlerError<T>(target)),
            };
        }

        protected abstract Result Execute(T target, ITableIndex begin, ITableEditor? editor);
    }
}