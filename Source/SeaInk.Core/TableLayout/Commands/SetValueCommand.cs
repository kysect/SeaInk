using FluentResults;
using Kysect.Centum.Sheets.Indices;
using SeaInk.Core.TableLayout.CommandInterfaces;
using SeaInk.Core.TableLayout.CommandsBase;
using SeaInk.Utility.Extensions;

namespace SeaInk.Core.TableLayout.Commands
{
    public class SetValueCommand<T> : GenericLayoutCommand<IValueSettingLayoutComponent<T>>
    {
        private readonly T _value;

        public SetValueCommand(T value)
        {
            _value = value.ThrowIfNull();
        }

        protected override Result Execute(IValueSettingLayoutComponent<T> target, ISheetIndex begin, ISheetEditor? editor)
        {
            target.SetValue(_value, begin, editor.ThrowIfNull());
            return Result.Ok();
        }
    }
}