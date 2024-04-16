using myStroke;
using System.Windows.Media;

namespace myBaseStroke
{
    public class myBaseStroke : IStroke
    {
        public string strokeName => "BaseStroke";
        public string strokeImage => "images/styleBaseLine.png";
        public DoubleCollection strokeValue => null;
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
