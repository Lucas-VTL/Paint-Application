using myShape;
using System.Windows;

namespace myRhombus
{
    public class myRhombus : IShape
    {
        private Point startPoint;
        private Point endPoint;

        public string shapeName => "Rhombus";
        public string shapeImage => "images/shapeRhombus.png";

        public void addStartPoint(Point point) { startPoint = point; }
        public void addEndPoint(Point point) { endPoint = point; }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public UIElement convertShapeType() { return null; }
    }
}
