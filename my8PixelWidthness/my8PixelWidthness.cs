using myWidthness;
using System.Windows;

namespace my8PixelWidthness
{
    public class my8PixelWidthness : IWidthness
    {
        public string widthnessName => "8px";
        public string widthnessImage => "images/styleWidth3.png";
        public Thickness widthnessValue => new Thickness(8);
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
