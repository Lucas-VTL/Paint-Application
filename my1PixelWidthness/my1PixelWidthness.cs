using myWidthness;
using System.Windows;

namespace my1PixelWidthness
{
    public class my1PixelWidthness : IWidthness
    {
        public string widthnessName => "1px";
        public string widthnessImage => "images/styleBaseLine.png";
        public Thickness widthnessValue => new Thickness(1);
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
