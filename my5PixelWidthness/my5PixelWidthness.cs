using myWidthness;
using System.Windows;

namespace my5PixelWidthness
{
    public class my5PixelWidthness : IWidthness
    {
        public string widthnessName => "5px";
        public string widthnessImage => "images/styleWidth2.png";
        public double widthnessValue => 5.0;
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
