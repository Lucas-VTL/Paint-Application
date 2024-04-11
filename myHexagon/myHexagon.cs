using myShape;
using System.Windows;

namespace myHexagon
{
    public class myHexagon : IShape
    {
        private Point startPoint;
        private Point endPoint;

        public string shapeName => "Hexagon";
        public string shapeImage => "images/shapeHexagon.png";

        public void addStartPoint(Point point) { startPoint = point; }
        public void addEndPoint(Point point) { endPoint = point; }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public UIElement convertShapeType() { return null; }
    }
}
