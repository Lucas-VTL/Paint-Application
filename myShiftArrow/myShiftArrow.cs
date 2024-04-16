using myShape;
using myWidthness;
using System.Windows;

namespace myShiftArrow
{
    public class myShiftArrow : IShape
    {
        private Point startPoint;
        private Point endPoint;
        IWidthness widthness;
        public string shapeName => "ShiftArrow";
        public string shapeImage => "";

        public void addStartPoint(Point point) { startPoint = point; }
        public void addEndPoint(Point point) { endPoint = point; }
        public void addWidthness(IWidthness width)
        {
            widthness = width;
        }
        public object Clone()
        {
            return MemberwiseClone();
        }

        public UIElement convertShapeType()
        {
            return null;
        }
    }
}
