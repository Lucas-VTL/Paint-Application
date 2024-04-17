using System.Windows.Media;

namespace myStroke
{
    public interface IStroke : ICloneable
    {
        string strokeName { get; }
        string strokeImage { get; }
        DoubleCollection strokeValue { get; }
    }
}
