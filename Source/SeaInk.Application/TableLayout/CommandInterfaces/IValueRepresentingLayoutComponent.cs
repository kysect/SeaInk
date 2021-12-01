namespace SeaInk.Application.TableLayout.CommandInterfaces
{
    public interface IValueRepresentingLayoutComponent<T>
    {
        T Value { get; }
    }
}