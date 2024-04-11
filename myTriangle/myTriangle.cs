using myShape;
using System.Windows;

namespace myTriangle
{
    public class myTriangle : IShape
    {
        private Point startPoint;
        private Point endPoint;

        public string shapeName => "Triangle";
        public string shapeImage => "images/shapeTriangle.png";

        public void addStartPoint(Point point) { startPoint = point; }
        public void addEndPoint(Point point) { endPoint = point; }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public UIElement convertShapeType() { return null; }
    }
}
