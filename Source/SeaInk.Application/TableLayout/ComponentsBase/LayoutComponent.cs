using System;
using FluentResults;
using SeaInk.Application.TableLayout.CommandsBase;
using SeaInk.Application.TableLayout.Indices;
using SeaInk.Application.TableLayout.Models;

namespace SeaInk.Application.TableLayout.ComponentsBase
{
    public abstract class LayoutComponent : IEquatable<LayoutComponent>
    {
        public abstract Frame Frame { get; }
        public abstract bool Equals(LayoutComponent? other);
        public abstract override bool Equals(object? obj);
        public abstract override int GetHashCode();

        public virtual Result ExecuteCommand(ILayoutCommand command, ITableIndex begin, ITableEditor? editor)
            => command.Execute(this, begin, editor);
    }
}