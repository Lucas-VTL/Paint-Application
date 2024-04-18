using myColor;
using System.Windows.Media;

namespace myRed
{
    public class myRed : IColor
    {
        public string colorName => "Red";
        public SolidColorBrush colorValue => new SolidColorBrush(Color.FromRgb(255, 0, 0));
        public void addColorRGB(byte r, byte g, byte b) { }
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
