namespace SeaInk.Application.TableLayout.Indices
{
    public interface ITableIndex
    {
        int Column { get; }
        int Row { get; }

        void MoveHorizontally(int i = 1);
        void MoveVertically(int i = 1);

        ITableIndex Copy();
    }
}