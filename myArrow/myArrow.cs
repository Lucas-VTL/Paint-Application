﻿using myColor;
using myShape;
using myStroke;
using myWidthness;
using System.Windows;
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
        private IColor colorValue;

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
        public void addColor(IColor color)
        {
            colorValue = color;
        }
        public void addPointList(List<Point> pointList) { }
        public object Clone()
        {
            return MemberwiseClone();
        }

        public UIElement convertShapeType() {
            Point start = startPoint;
            Point end = endPoint;
            Point center = new Point((startPoint.X + endPoint.X) / 2, (startPoint.Y + endPoint.Y) / 2);

            var left = Math.Min(startPoint.X, endPoint.X);
            var right = Math.Max(startPoint.X, endPoint.X);

            var top = Math.Min(startPoint.Y, endPoint.Y);
            var bottom = Math.Max(startPoint.Y, endPoint.Y);

            var width = right - left;
            var height = bottom - top;

            var element = new Path
            {
                StrokeThickness = widthness.widthnessValue,
                StrokeDashArray = strokeStyle.strokeValue,
                Stroke = colorValue.colorValue,
                Data = CreateArrowGeometry(center, start, end, width, height)
            };

            return element;
        }

        private Geometry CreateArrowGeometry(Point center, Point start, Point end, double width, double height)
        {
            var geometry = new PathGeometry();

            var figure = new PathFigure
            {
                StartPoint = new Point(center.X, startPoint.Y),
                IsClosed = true
            };

            figure.Segments.Add(new LineSegment(new Point(endPoint.X, center.Y - height / 6), true));
            figure.Segments.Add(new LineSegment(new Point(center.X + width / 6, center.Y - height / 6), true));
            figure.Segments.Add(new LineSegment(new Point(center.X + width / 6, endPoint.Y), true));
            figure.Segments.Add(new LineSegment(new Point(center.X - width / 6, endPoint.Y), true));
            figure.Segments.Add(new LineSegment(new Point(center.X - width / 6, center.Y - height / 6), true));
            figure.Segments.Add(new LineSegment(new Point(startPoint.X, center.Y - height / 6), true));

            geometry.Figures.Add(figure);
            return geometry;
        }
    }
}
