using System.Windows;
using Shape;

namespace Rectangle
{
    public class Rectangle : IShape
    {
        private Point startPoint;
        private Point endPoint;

        public string shapeName => "Rectangle";

        public string shapeImage => "images/shapeRectangle";

        public object Clone()
        {
            return MemberwiseClone();
        }

        public void addStartPoint(Point point)
        {
            startPoint = point;
        }

        public void addEndPoint(Point point)
        {
            endPoint = point;
        }

        public UIElement convertShape()
        {
            return null;
        }
    }
}
