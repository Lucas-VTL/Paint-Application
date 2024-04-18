using myColor;
using System.Windows.Media;

namespace myGreen
{
    public class myGreen : IColor
    {
        public string colorName => "Green";
        public SolidColorBrush colorValue => new SolidColorBrush(Color.FromRgb(0, 128, 0));
        public void addColorRGB(byte r, byte g, byte b) { }
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
