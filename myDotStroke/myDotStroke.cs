using myStroke;
using System.Windows.Media;

namespace myDotStroke
{
    public class myDotStroke : IStroke
    {
        public string strokeName => "DotStroke";
        public string strokeImage => "images/styleStroke3.png";
        public DoubleCollection strokeValue => new DoubleCollection() { 1 };
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
