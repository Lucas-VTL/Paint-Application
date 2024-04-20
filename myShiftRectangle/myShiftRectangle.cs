using myShape;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;
using myWidthness;
using myStroke;
using myColor;

namespace myShiftRectangle
{
    public class myShiftRectangle : IShape
    {
        private Point startPoint;
        private Point endPoint;
        IWidthness widthness;
        IStroke strokeStyle;
        IColor colorValue;
        public string shapeName => "ShiftRectangle";
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

            double side = Math.Min(Math.Abs(end.X - start.X), Math.Abs(end.Y - start.Y));
            double startX = Math.Min(start.X, end.X);
            double startY = Math.Min(start.Y, end.Y);

            Rectangle square = new Rectangle()
            {
                Stroke = Brushes.Black,
                StrokeDashArray = strokeStyle.strokeValue,
                StrokeThickness = 2,
                Width = side,
                Height = side
            };

            square.SetValue(Canvas.LeftProperty, startX);
            square.SetValue(Canvas.TopProperty, startY);

            return square;
        }
    }
}
