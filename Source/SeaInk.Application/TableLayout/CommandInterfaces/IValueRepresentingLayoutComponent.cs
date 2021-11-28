namespace SeaInk.Application.TableLayout.CommandInterfaces
{
    public interface IValueRepresentingLayoutComponent<out T>
    {
        T Value { get; }
    }
}