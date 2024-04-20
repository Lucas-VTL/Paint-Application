using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using myShape;
using myWidthness;
using myStroke;
using myColor;

namespace myShiftEllipse
{
    public class myShiftEllipse : IShape
    {
        private Point startPoint;
        private Point endPoint;
        IWidthness widthness;
        IStroke strokeStyle;
        IColor colorValue;
        public string shapeName => "ShiftEllipse";
        public string shapeImage => "";

        public void addStartPoint(Point point) { startPoint = point; }
        public void addEndPoint(Point point) { endPoint = point; }
        public void addWidthness(IWidthness width)
        {
            widthness = width;
        }
        public void addStrokeStyle(IStroke stroke)
        {
            strokeStyle = stroke;
        }
        public void addColor(IColor color)
        {
            colorValue = color;
        }
        public void addPointList(List<Point> pointList) { }
        public object Clone()
        {
            return MemberwiseClone();
        }

        public UIElement convertShapeType()
        {
            var start = startPoint;
            var end = endPoint;

            double radius = Math.Min(Math.Abs(end.X - start.X), Math.Abs(end.Y - start.Y)) / 2;
            double centerX = start.X + (end.X - start.X) / 2;
            double centerY = start.Y + (end.Y - start.Y) / 2;

            Ellipse circle = new Ellipse()
            {
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                Width = radius * 2,
                Height = radius * 2
            };

            circle.SetValue(Canvas.LeftProperty, centerX - radius);
            circle.SetValue(Canvas.TopProperty, centerY - radius);

            return circle;
        }

    }
}
