using SeaInk.Core.TableLayout.Exceptions;

namespace SeaInk.Core.TableLayout.Models
{
    public class Scale
    {
        public Scale(int horizontal, int vertical)
        {
            if (horizontal <= 0)
                throw new InvalidScaleComponentException(nameof(horizontal), horizontal);

            if (vertical <= 0)
                throw new InvalidScaleComponentException(nameof(vertical), vertical);

            Horizontal = horizontal;
            Vertical = vertical;
        }

        public int Horizontal { get; }
        public int Vertical { get; }
    }
}