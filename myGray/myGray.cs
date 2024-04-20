using myColor;
using System.Windows.Media;

namespace myGray
{
    public class myGray : IColor
    {
        public string colorName => "Gray";
        public SolidColorBrush colorValue => new SolidColorBrush(Color.FromRgb(128, 128, 128));
        public void addColorRGB(byte r, byte g, byte b) { }
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
