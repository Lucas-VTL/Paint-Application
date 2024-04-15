using myShape;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;

namespace myShiftLine
{
    public class myShiftLine : IShape
    {
        private Point startPoint;
        private Point endPoint;

        public string shapeName => "ShiftLine";
        public string shapeImage => "";

        public void addStartPoint(Point point) { startPoint = point; }
        public void addEndPoint(Point point) { endPoint = point; }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public UIElement convertShapeType()
        {
            return new Line()
            {
                X1 = startPoint.X,
                Y1 = startPoint.Y,
                X2 = endPoint.X,
                Y2 = endPoint.Y,
                Fill = Brushes.AliceBlue,
                Stroke = Brushes.Black,
                StrokeThickness = 2,
            };
        }
    }
}
