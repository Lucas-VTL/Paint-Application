using myWidthness;
using System.Windows;

namespace my3PixelWidthness
{
    public class my3PixelWidthness : IWidthness
    {
        public string widthnessName => "3px";
        public string widthnessImage => "images/styleWidth1.png";
        public double widthnessValue => 3.0;
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
