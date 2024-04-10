using System.Windows;
using Shape;

namespace Line
{
    public class Line : IShape
    {
        private Point startPoint;
        private Point endPoint;

        public string shapeName => "Line";

        public string shapeImage => "images/shapeLine";

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
