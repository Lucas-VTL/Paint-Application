using myWidthness;
using System.Windows;

namespace my1PixelWidthness
{
    public class my1PixelWidthness : IWidthness
    {
        public string widthnessName => "1px";
        public string widthnessImage => "images/styleBaseLine.png";
        public double widthnessValue => 1.0;
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
