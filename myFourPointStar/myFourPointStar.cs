using System.Windows;
using myShape;

namespace myFourPointStar
{
    public class myFourPointStar : IShape
    {
        private Point startPoint;
        private Point endPoint;

        public string shapeName => "FourPointStar";
        public string shapeImage => "images/shape4Star.png";

        public void addStartPoint(Point point) { startPoint = point; }
        public void addEndPoint(Point point) { endPoint = point; }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public UIElement convertShapeType() { return null; }
    }
}
