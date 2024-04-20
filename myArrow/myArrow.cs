using myShape;
using myStroke;
using myWidthness;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media;
using System.Windows.Shapes;

namespace myArrow
{
    public class myArrow : IShape
    {
        private Point startPoint;
        private Point endPoint;
        private IWidthness widthness;
        private IStroke strokeStyle;

        public string shapeName => "Arrow";
        public string shapeImage => "images/shapeArrow.png";

        public void addStartPoint(Point point) { startPoint = point; }
        public void addEndPoint(Point point) { endPoint = point; }
        public void addWidthness (IWidthness width)
        {
            widthness = width;
        }
        public void addStrokeStyle(IStroke stroke) 
        {
            strokeStyle = stroke;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public UIElement convertShapeType()
        {
            // find the arrow shaft unit vector
            double vx = endPoint.X - startPoint.X;
            double vy = endPoint.Y - startPoint.Y;
            double dist = (double)Math.Sqrt(vx * vx + vy * vy);
            vx /= dist;
            vy /= dist;

            double wingLength = Math.Max(7, widthness.widthnessValue * 2);

            double ax = wingLength * (-vy - vx);
            double ay = wingLength * (vx - vy);

            Point wing1 = new Point(endPoint.X + ax, endPoint.Y + ay);
            Point wing2 = new Point(endPoint.X - ay, endPoint.Y + ax);

            // calculate the bounding box of the arrow
            double minX = Math.Min(startPoint.X, Math.Min(endPoint.X, Math.Min(wing1.X, wing2.X)));
            double minY = Math.Min(startPoint.Y, Math.Min(endPoint.Y, Math.Min(wing1.Y, wing2.Y)));
            double maxX = Math.Max(startPoint.X, Math.Max(endPoint.X, Math.Max(wing1.X, wing2.X)));
            double maxY = Math.Max(startPoint.Y, Math.Max(endPoint.Y, Math.Max(wing1.Y, wing2.Y)));

            double width = maxX - minX;
            double height = maxY - minY;

            // create the arrow shaft
            Line line = new Line
            {
                X1 = startPoint.X - minX,
                Y1 = startPoint.Y - minY,
                X2 = endPoint.X - minX,
                Y2 = endPoint.Y - minY,
            };

            // create the arrowhead
            PathFigure arrowHeadPath = new PathFigure();
            arrowHeadPath.StartPoint = new Point(wing1.X - minX, wing1.Y - minY);
            arrowHeadPath.Segments.Add(new LineSegment(new Point(endPoint.X - minX, endPoint.Y - minY), true));
            arrowHeadPath.Segments.Add(new LineSegment(new Point(wing2.X - minX, wing2.Y - minY), true));

            PathGeometry arrowGeometry = new PathGeometry();
            arrowGeometry.Figures.Add(arrowHeadPath);

            Path arrowHead = new Path
            {
                Data = arrowGeometry,
            };

            // combine arrow line and arrowhead into a single container
            Canvas container = new Canvas
            {
                Width = width,
                Height = height
            };

            container.Children.Add(line);
            container.Children.Add(arrowHead);

            if (endPoint.X >= startPoint.X)
            {
                container.SetValue(Canvas.LeftProperty, startPoint.X);
            }
            else
            {
                container.SetValue(Canvas.LeftProperty, startPoint.X - width);
            }

            if (endPoint.Y >= startPoint.Y)
            {
                container.SetValue(Canvas.TopProperty, startPoint.Y);
            }
            else
            {
                container.SetValue(Canvas.TopProperty, startPoint.Y - height);
            }

            return container;
        }
    }
}
