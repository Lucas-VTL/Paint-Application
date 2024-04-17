using myShape;
using myStroke;
using myWidthness;
using System.Windows;

namespace myShiftArrow
{
    public class myShiftArrow : IShape
    {
        private Point startPoint;
        private Point endPoint;
        IWidthness widthness;
        IStroke strokeStyle;
        public string shapeName => "ShiftArrow";
        public string shapeImage => "";

        public void addStartPoint(Point point) { startPoint = point; }
        public void addEndPoint(Point point) { endPoint = point; }
        public void addWidthness(IWidthness width)
        {
            widthness = width;
        }
        public void addStrokeStyle(IStroke stroke)
        {
            strokeStyle = stroke;
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
