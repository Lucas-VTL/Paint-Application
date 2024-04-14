using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using myShape;

namespace myCircle
{
    public class myCircle : IShape
    {
        private Point startPoint;
        private Point endPoint;

        public string shapeName => "Circle";
        public string shapeImage => "";

        public void addStartPoint(Point point) { startPoint = point; }
        public void addEndPoint(Point point) { endPoint = point; }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public UIElement convertShapeType()
        {

            var start = startPoint;
            var end = endPoint;

            var left = Math.Min(end.X, start.X);
            var right = Math.Max(end.X, start.X);

            var top = Math.Min(end.Y, start.Y);
            var bottom = Math.Max(end.Y, start.Y);

            var width = right - left;
            var height = bottom - top;

            if (width > height)
            {
                width = height;
            } else
            {
                height = width;
            }

            var element = new Ellipse
            {
                Fill = Brushes.AliceBlue,
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Width = width,
                Height = height
            };

            Canvas.SetLeft(element, left);
            Canvas.SetTop(element, top);
            return element;
        }
    }
}
