using myColor;
using System.Windows.Media;

namespace myCustomColor
{
    public class myCustomColor : IColor
    {
        public string colorName => "Custom Color";
        public SolidColorBrush colorValue => new SolidColorBrush(Color.FromRgb(colorRed, colorGreen, colorBlue));
        
        public byte colorRed;
        
        public byte colorGreen;
        
        public byte colorBlue;

        public void addColorRGB(byte r, byte g, byte b)
        {
            colorRed = r;
            colorGreen = g;
            colorBlue = b;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
