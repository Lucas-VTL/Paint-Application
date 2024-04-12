using myShape;
using System.Windows;

namespace myArrow
{
    public class myArrow : IShape
    {
        private Point startPoint;
        private Point endPoint;

        public string shapeName => "Arrow";
        public string shapeImage => "images/shapeArrow.png";

        public void addStartPoint(Point point) { startPoint = point; }
        public void addEndPoint(Point point) { endPoint = point; }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public UIElement convertShapeType() {

            return null;
        }
    }
}
