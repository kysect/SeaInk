namespace SeaInk.Core.TableLayout.CommandInterfaces
{
    public interface IValueRepresentingLayoutComponent<T>
    {
        T Value { get; }
    }
}