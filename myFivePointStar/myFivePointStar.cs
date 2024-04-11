using System.Windows;
using myShape;

namespace myFivePointStar
{
    public class myFivePointStar : IShape
    {
        private Point startPoint;
        private Point endPoint;

        public string shapeName => "FivePointStar";
        public string shapeImage => "images/shape5Star.png";

        public void addStartPoint(Point point) { startPoint = point; }
        public void addEndPoint(Point point) { endPoint = point; }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public UIElement convertShapeType() { return null; }
    }
}
