using myShape;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace myArrow
{
    public class myArrow : IShape
    {
        private Point startPoint;
        private Point endPoint;

        public string shapeName => "Arrow";
        public string shapeImage => "images/shapeArrow.png";

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

            // Calculate arrow properties
            var arrowLength = 20;
            var arrowWidth = 10;
            var tailLength = 10;

            var angle = Math.Atan2(end.Y - start.Y, end.X - start.X);

            // Calculate points for the arrow shape
            var arrowPoints = new Point[]
            {
                new Point(end.X - arrowLength * Math.Cos(angle), end.Y - arrowLength * Math.Sin(angle)),
                new Point(end.X - arrowWidth * Math.Cos(angle + Math.PI / 2), end.Y - arrowWidth * Math.Sin(angle + Math.PI / 2)),
                new Point(end.X + arrowWidth * Math.Cos(angle + Math.PI / 2), end.Y + arrowWidth * Math.Sin(angle + Math.PI / 2))
            };

            var tailPoint1 = new Point(start.X + tailLength * Math.Cos(angle + Math.PI / 2), start.Y + tailLength * Math.Sin(angle + Math.PI / 2));
            var tailPoint2 = new Point(start.X - tailLength * Math.Cos(angle + Math.PI / 2), start.Y - tailLength * Math.Sin(angle + Math.PI / 2));

            // Create a PathGeometry to contain the arrow shape
            var arrowGeometry = new PathGeometry();

            // Create a PathFigure to define the arrow shape
            var figure = new PathFigure
            {
                StartPoint = arrowPoints[0],
                IsClosed = true
            };

            // Add arrow points to the PathFigure
            figure.Segments.Add(new LineSegment(arrowPoints[1], true));
            figure.Segments.Add(new LineSegment(end, true));
            figure.Segments.Add(new LineSegment(arrowPoints[2], true));

            // Create a tail for the arrow
            figure.Segments.Add(new LineSegment(tailPoint1, true));
            figure.Segments.Add(new LineSegment(start, true));
            figure.Segments.Add(new LineSegment(tailPoint2, true));

            // Add the PathFigure to the PathGeometry
            arrowGeometry.Figures.Add(figure);

            // Create a Path element to represent the arrow shape
            var path = new Path
            {
                Fill = Brushes.LightGray,
                Stroke = Brushes.Black,
                StrokeThickness = 1,
                Data = arrowGeometry
            };

            return path;
        }
    }
}
