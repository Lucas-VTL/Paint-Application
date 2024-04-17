using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using myShape;
using myStroke;
using myWidthness;

namespace myHeart
{
    public class myHeart : IShape
    {
        private Point startPoint;
        private Point endPoint;
        IWidthness widthness;
        IStroke strokeStyle;
        public string shapeName => "Heart";
        public string shapeImage => "images/shapeHeart.png";

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
        public object Clone()
        {
            return MemberwiseClone();
        }

        public UIElement convertShapeType()
        {
            // Calculate control points for ArcSegments
            var start = startPoint;
            var end = endPoint;

            var width = Math.Abs(start.X - end.X);
            var height = Math.Abs(start.Y - end.Y);
            //var diameter = Math.Min(width, height);

            //if (_isShiftPressed)
            //{
            //    width = diameter;
            //    height = diameter;
            //}

            Point center = new Point((startPoint.X + endPoint.X) / 2, (startPoint.Y + endPoint.Y) / 2);
            double minX = center.X - width / 2 - width / 30 ;
            double maxX = center.X + width / 2 + width / 30 ;
            double minY = center.Y - height / 2 - height / 6.25 ;
            double maxY = center.Y + height / 2 ;

            // Create ArcSegments
            var arc1 = new ArcSegment(new Point(center.X, center.Y - height / 2), new Size(width/4, width/4), 0, false, SweepDirection.Clockwise, true);
            var arc2 = new ArcSegment(new Point(center.X + width/2, center.Y - height / 4), new Size(width/4, height/4), 0, false, SweepDirection.Clockwise, true);

            // Create LineSegments
            var line1 = new LineSegment(new Point(center.X - width / 2, center.Y - height / 4), true);
            var line2 = new LineSegment(new Point(center.X, center.Y + height / 4), true);

            // Create PathGeometry
            var pathGeometry = new PathGeometry();
            var pathFigure = new PathFigure();
            pathFigure.StartPoint = new Point(center.X, center.Y + height/4);
            
            pathFigure.Segments.Add(line1);
            pathFigure.Segments.Add(arc1);
            pathFigure.Segments.Add(arc2);
            pathFigure.Segments.Add(line2);
            pathGeometry.Figures.Add(pathFigure);

            // Create Path
            var path = new Path
            {
                Stroke = Brushes.Red,
                StrokeThickness = 2,
                Data = pathGeometry
            };

            //double boundingWidth = maxX - minX;
            //double boundingHeight = maxY - minY;

            //Grid containerGrid = new Grid();
            //containerGrid.Width = boundingWidth;
            //containerGrid.Height = boundingHeight;
            //containerGrid.Children.Add(path);

            //// Set the position of the containerGrid
            //Canvas.SetLeft(containerGrid, minX);
            //Canvas.SetTop(containerGrid, minY);

            return path;
        }
    }
}
