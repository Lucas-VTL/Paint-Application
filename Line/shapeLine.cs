using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using Shape;

namespace Line
{
    public class Line : IShape
    {
        private Point startPoint;
        private Point endPoint;

        public string shapeName => "Line";

        public string shapeImage => "images/shapeLine";

        public object Clone()
        {
            return MemberwiseClone();
        }

        public void addStartPoint(Point point)
        {
            startPoint = point;
        }

        public void addEndPoint(Point point)
        {
            endPoint = point;
        }

        public UIElement convertShape()
        {
            return new Line()
            {
                X1 = _start.X,
                Y1 = _start.Y,
                X2 = _end.X,
                Y2 = _end.Y,
                StrokeThickness = 1,
                Stroke = new SolidColorBrush(Colors.Red)
            }; ;
        }
    }
}
