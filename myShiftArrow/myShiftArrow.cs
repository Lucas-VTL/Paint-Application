using myShape;
using System.Windows;

namespace myShiftArrow
{
    public class myShiftArrow : IShape
    {
        private Point startPoint;
        private Point endPoint;

        public string shapeName => "ShiftArrow";
        public string shapeImage => "";

        public void addStartPoint(Point point) { startPoint = point; }
        public void addEndPoint(Point point) { endPoint = point; }

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
