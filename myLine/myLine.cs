using myShape;
using System.Windows;

namespace myLine
{
    public class myLine : IShape
    {
        private Point startPoint;
        private Point endPoint;

        public string shapeName => "Line";
        public string shapeImage => "images/shapeLine.png";

        public void addStartPoint (Point point) {startPoint = point;}
        public void addEndPoint (Point point) {endPoint = point;}

        public object Clone()
        {
            return MemberwiseClone();
        }

        public UIElement convertShapeType() { return null; }
    }
}
