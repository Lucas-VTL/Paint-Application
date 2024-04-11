using myShape;
using System.Windows;

namespace myPentagon
{
    public class myPentagon : IShape
    {
        private Point startPoint;
        private Point endPoint;

        public string shapeName => "Pentagon";
        public string shapeImage => "images/shapePentagon.png";

        public void addStartPoint(Point point) { startPoint = point; }
        public void addEndPoint(Point point) { endPoint = point; }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public UIElement convertShapeType() { return null; }
    }
}
