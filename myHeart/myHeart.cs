using System.Windows;
using myShape;

namespace myHeart
{
    public class myHeart : IShape
    {
        private Point startPoint;
        private Point endPoint;

        public string shapeName => "Heart";
        public string shapeImage => "images/shapeHeart.png";

        public void addStartPoint(Point point) { startPoint = point; }
        public void addEndPoint(Point point) { endPoint = point; }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public UIElement convertShapeType() { return null; }
    }
}
