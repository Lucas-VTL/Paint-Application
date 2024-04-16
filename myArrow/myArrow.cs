using myShape;
using myWidthness;
using System.Windows;

namespace myArrow
{
    public class myArrow : IShape
    {
        private Point startPoint;
        private Point endPoint;
        private IWidthness widthness;

        public string shapeName => "Arrow";
        public string shapeImage => "images/shapeArrow.png";

        public void addStartPoint(Point point) { startPoint = point; }
        public void addEndPoint(Point point) { endPoint = point; }
        public void addWidthness (IWidthness width)
        {
            widthness = width;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public UIElement convertShapeType() {

            return null;
        }
    }
}
