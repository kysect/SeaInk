using FluentResults;
using Kysect.Centum.Sheets.Indices;
using SeaInk.Application.TableLayout.CommandInterfaces;
using SeaInk.Application.TableLayout.CommandsBase;
using SeaInk.Utility.Extensions;

namespace SeaInk.Application.TableLayout.Commands
{
    public class SetValueCommand<T> : GenericLayoutCommand<IValueSettingLayoutComponent<T>>
    {
        private readonly T _value;

        public SetValueCommand(T value)
        {
            _value = value.ThrowIfNull(nameof(value));
        }

        protected override Result Execute(IValueSettingLayoutComponent<T> target, ISheetIndex begin, ITableEditor? editor)
        {
            target.SetValue(_value, begin, editor.ThrowIfNull(nameof(editor)));
            return Result.Ok();
        }
    }
}