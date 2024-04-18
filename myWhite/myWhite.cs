using myColor;
using System.Windows.Media;

namespace myWhite
{
    public class myWhite : IColor
    {
        public string colorName => "White";
        public SolidColorBrush colorValue => new SolidColorBrush(Color.FromRgb(255, 255, 255));
        public void addColorRGB(byte r, byte g, byte b) { }
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
