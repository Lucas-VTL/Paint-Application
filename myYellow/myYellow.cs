using myColor;
using System.Windows.Media;

namespace myYellow
{
    public class myYellow : IColor
    {
        public string colorName => "Yellow";
        public SolidColorBrush colorValue => new SolidColorBrush(Color.FromRgb(255, 255, 0));
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
