using System.Windows.Media;

namespace myColor
{
    public interface IColor : ICloneable 
    {
        string colorName { get; }
        byte colorRed { get; }
        byte colorGreen { get; }
        byte colorBlue { get; }
    }
}
