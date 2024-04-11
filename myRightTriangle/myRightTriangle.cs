using myShape;
using System.Windows;

namespace myRightTriangle
{
    public class myRightTriangle : IShape
    {
        private Point startPoint;
        private Point endPoint;

        public string shapeName => "RightTriangle";
        public string shapeImage => "images/shapeRightTriangle.png";

        public void addStartPoint(Point point) { startPoint = point; }
        public void addEndPoint(Point point) { endPoint = point; }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public UIElement convertShapeType() { return null; }
    }
}
