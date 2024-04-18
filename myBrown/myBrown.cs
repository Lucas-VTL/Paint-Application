using myColor;
using System.Windows.Media;

namespace myBrown
{
    public class myBrown : IColor
    {
        public string colorName => "Brown";
        public SolidColorBrush colorValue => new SolidColorBrush(Color.FromRgb(165, 42, 42));
        public void addColorRGB(byte r, byte g, byte b) { }
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
