namespace SeaInk.Application.TableLayout.Indices
{
    public interface ITableIndex
    {
        int Column { get; }
        int Row { get; }

        void MoveHorizontally(int value = 1);
        void MoveVertically(int value = 1);

        ITableIndex Copy();
    }
}