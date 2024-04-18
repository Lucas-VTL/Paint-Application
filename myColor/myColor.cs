using System.Windows.Media;

namespace myColor
{
    public interface IColor : ICloneable 
    {
        string colorName { get; }
        SolidColorBrush colorValue { get; }
        void addColorRGB(byte r, byte g, byte b);
    }
}
