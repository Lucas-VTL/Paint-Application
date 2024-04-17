using myStroke;
using System.Windows.Media;

namespace myShortDashStroke
{
    public class myShortDashStroke : IStroke
    {
        public string strokeName => "ShortDashStroke";
        public string strokeImage => "images/styleStroke2.png";
        public DoubleCollection strokeValue => new DoubleCollection() {10, 2};
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
