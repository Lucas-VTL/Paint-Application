using myStroke;
using System.Windows.Media;

namespace myLongDashStroke
{
    public class myLongDashStroke : IStroke
    {
        public string strokeName => "LongDashStroke";
        public string strokeImage => "images/styleStroke1.png";
        public DoubleCollection strokeValue => new DoubleCollection() { 20, 5 };
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
