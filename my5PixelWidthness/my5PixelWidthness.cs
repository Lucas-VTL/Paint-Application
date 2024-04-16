using myWidthness;
using System.Windows;

namespace my5PixelWidthness
{
    public class my5PixelWidthness : IWidthness
    {
        public string widthnessName => "5px";
        public string widthnessImage => "images/styleWidth2.png";
        public Thickness widthnessValue => new Thickness(5);
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
