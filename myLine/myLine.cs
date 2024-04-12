using myShape;
using System.Configuration;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

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

        public UIElement convertShapeType() {
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
