using myStroke;
using System.Windows.Media;

namespace myDashDotDotStroke
{
    public class myDashDotDotStroke : IStroke
    {
        public string strokeName => "DashDotDotStroke";
        public string strokeImage => "images/styleStroke4.png";
        public DoubleCollection strokeValue => new DoubleCollection() { 12, 3, 3, 3, 3, 3 };
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
