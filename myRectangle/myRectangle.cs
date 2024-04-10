using myShape;
using System.Windows;

namespace myRectangle
{
    public class myRectangle : IShape
    {
        private Point startPoint;
        private Point endPoint;

        public string shapeName => "Rectangle";
        public string shapeImage => "images/shapeRectangle.png";

        public void addStartPoint(Point point) { startPoint = point; }
        public void addEndPoint(Point point) { endPoint = point; }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public UIElement convertShapeType() { return null; }
    }
}
