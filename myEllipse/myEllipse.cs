using System.Windows;
using myShape;

namespace myEllipse
{
    public class myEllipse : IShape
    {
        private Point startPoint;
        private Point endPoint;

        public string shapeName => "Ellipse";
        public string shapeImage => "images/shapeEllipse.png";

        public void addStartPoint(Point point) { startPoint = point; }
        public void addEndPoint(Point point) { endPoint = point; }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public UIElement convertShapeType() { return null; }
    }
}
