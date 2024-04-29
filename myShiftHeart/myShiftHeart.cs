using myShape;
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;
using myWidthness;
using myStroke;
using myColor;
using System.Windows.Controls;

namespace myShiftHeart
{
    public class myShiftHeart : IShape
    {
        private Point startPoint;
        private Point endPoint;
        private IWidthness widthness;
        private IStroke strokeStyle;
        private IColor colorValue;
        private bool isFill;
        private bool isEdit;
        public string shapeName => "ShiftHeart";
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
        public void addFontSize(int fontSize) { }
        public void addFontFamily(string fontFamily) { }
        public TextBox getTextBox() { return null; }
        public void setTextString(string text) { }
        public void setFocus(bool focus) { }
        public void setBold(bool bold) { }
        public void setItalic(bool italic) { }
        public void setBackground(byte r, byte g, byte b) { }
        public Point getStartPoint()
        {
            return startPoint;
        }
        public Point getEndPoint()
        {
            return endPoint;
        }
        public Point getCenterPoint()
        {
            return new Point((startPoint.X + endPoint.X) / 2, (startPoint.Y + endPoint.Y) / 2);
        }
        public void setShapeFill(bool isShapeFill)
        {
            isFill = isShapeFill;
        }
        public void setEdit(bool edit)
        {
            isEdit = edit;
        }
        public object Clone()
        {
            return MemberwiseClone();
        }

        public UIElement convertShapeType()
        {
            Point center;

            var left = Math.Min(startPoint.X, endPoint.X);
            var right = Math.Max(startPoint.X, endPoint.X);

            var top = Math.Min(startPoint.Y, endPoint.Y);
            var bottom = Math.Max(startPoint.Y, endPoint.Y);

            var width = right - left;
            var height = bottom - top;

            string status = "";

            if (startPoint.X < endPoint.X && startPoint.Y < endPoint.Y)
            {
                status = "normal";

                if (width > height)
                {
                    width = height;
                    endPoint = new Point(startPoint.X + height, startPoint.Y + height);
                }
                else
                {
                    height = width;
                    endPoint = new Point(startPoint.X + width, startPoint.Y + width);
                }

                center = new Point((startPoint.X + endPoint.X) / 2, (startPoint.Y + endPoint.Y) / 2);
            }
            else if (startPoint.X < endPoint.X && startPoint.Y > endPoint.Y)
            {
                status = "upside";

                if (width > height)
                {
                    width = height;
                    endPoint = new Point(startPoint.X + height, startPoint.Y - height);
                }
                else
                {
                    height = width;
                    endPoint = new Point(startPoint.X + width, startPoint.Y - width);
                }

                center = new Point((startPoint.X + endPoint.X) / 2, (startPoint.Y + endPoint.Y) / 2);
            }
            else if (startPoint.X > endPoint.X && startPoint.Y < endPoint.Y)
            {
                status = "reverse";

                if (width > height)
                {
                    width = height;
                    endPoint = new Point(startPoint.X - height, startPoint.Y + height);
                }
                else
                {
                    height = width;
                    endPoint = new Point(startPoint.X - width, startPoint.Y + width);
                }

                center = new Point((startPoint.X + endPoint.X) / 2, (startPoint.Y + endPoint.Y) / 2);
            }
            else if (startPoint.X > endPoint.X && startPoint.Y > endPoint.Y)
            {
                status = "upside-reverse";

                if (width > height)
                {
                    width = height;
                    endPoint = new Point(startPoint.X - height, startPoint.Y - height);
                }
                else
                {
                    height = width;
                    endPoint = new Point(startPoint.X - width, startPoint.Y - width);
                }

                center = new Point((startPoint.X + endPoint.X) / 2, (startPoint.Y + endPoint.Y) / 2);
            }

            Path element;

            if (isFill)
            {
                element = new Path
                {
                    StrokeThickness = widthness.widthnessValue,
                    StrokeDashArray = strokeStyle.strokeValue,
                    Stroke = colorValue.colorValue,
                    Fill = colorValue.colorValue,
                    Data = CreateHeartGeometry(center, width, height, status)
                };
            } else
            {
                element = new Path
                {
                    StrokeThickness = widthness.widthnessValue,
                    StrokeDashArray = strokeStyle.strokeValue,
                    Stroke = colorValue.colorValue,
                    Data = CreateHeartGeometry(center, width, height, status)
                };
            }

            if (isEdit)
            {
                Canvas canvas = new Canvas();

                Rectangle rectangle = new Rectangle()
                {
                    Stroke = Brushes.Black,
                    StrokeThickness = 1,
                    StrokeDashArray = new DoubleCollection() { 10, 2 },
                    Width = width,
                    Height = height,
                };

                Canvas.SetLeft(rectangle, left);
                Canvas.SetTop(rectangle, top);

                Button LeftTopButton = new Button();
                LeftTopButton.Width = 10;
                LeftTopButton.Height = 10;
                LeftTopButton.Background = Brushes.White;
                Canvas.SetLeft(LeftTopButton, left - 5);
                Canvas.SetTop(LeftTopButton, top - 5);

                Button RightTopButton = new Button();
                RightTopButton.Width = 10;
                RightTopButton.Height = 10;
                RightTopButton.Background = Brushes.White;
                Canvas.SetLeft(RightTopButton, right - 5);
                Canvas.SetTop(RightTopButton, top - 5);

                Button LeftBottomButton = new Button();
                LeftBottomButton.Width = 10;
                LeftBottomButton.Height = 10;
                LeftBottomButton.Background = Brushes.White;
                Canvas.SetLeft(LeftBottomButton, left - 5);
                Canvas.SetTop(LeftBottomButton, bottom - 5);

                Button RightBottomButton = new Button();
                RightBottomButton.Width = 10;
                RightBottomButton.Height = 10;
                RightBottomButton.Background = Brushes.White;
                Canvas.SetLeft(RightBottomButton, right - 5);
                Canvas.SetTop(RightBottomButton, bottom - 5);

                Button LeftCenterButton = new Button();
                LeftCenterButton.Width = 10;
                LeftCenterButton.Height = 10;
                LeftCenterButton.Background = Brushes.White;
                Canvas.SetLeft(LeftCenterButton, left - 5);
                Canvas.SetTop(LeftCenterButton, top + (height / 2) - 5);

                Button RightCenterButton = new Button();
                RightCenterButton.Width = 10;
                RightCenterButton.Height = 10;
                RightCenterButton.Background = Brushes.White;
                Canvas.SetLeft(RightCenterButton, right - 5);
                Canvas.SetTop(RightCenterButton, top + (height / 2) - 5);

                Button TopCenterButton = new Button();
                TopCenterButton.Width = 10;
                TopCenterButton.Height = 10;
                TopCenterButton.Background = Brushes.White;
                Canvas.SetLeft(TopCenterButton, left + (width / 2) - 5);
                Canvas.SetTop(TopCenterButton, top - 5);

                Button BottomCenterButton = new Button();
                BottomCenterButton.Width = 10;
                BottomCenterButton.Height = 10;
                BottomCenterButton.Background = Brushes.White;
                Canvas.SetLeft(BottomCenterButton, left + (width / 2) - 5);
                Canvas.SetTop(BottomCenterButton, bottom - 5);

                canvas.Children.Add(rectangle);
                canvas.Children.Add(element);

                canvas.Children.Add(LeftTopButton);
                canvas.Children.Add(RightTopButton);
                canvas.Children.Add(LeftBottomButton);
                canvas.Children.Add(RightBottomButton);

                canvas.Children.Add(LeftCenterButton);
                canvas.Children.Add(RightCenterButton);
                canvas.Children.Add(TopCenterButton);
                canvas.Children.Add(BottomCenterButton);

                return canvas;
            }

            return element;
        }

        private Geometry CreateHeartGeometry(Point center, double width, double height, string status)
        {
            var geometry = new PathGeometry();
            var figure = new PathFigure();

            if (status == "normal")
            {
                figure.StartPoint = new Point(center.X, center.Y - height / 4);

                figure.Segments.Add(new BezierSegment()
                {
                    Point1 = new Point(center.X, center.Y - height / 4),
                    Point2 = new Point(((center.X + (startPoint.X + width / 4)) / 2) + width / 8, (((center.Y - height / 4) + startPoint.Y) / 2) - height / 8),
                    Point3 = new Point(startPoint.X + width / 4, startPoint.Y),
                    IsStroked = true
                });
                figure.Segments.Add(new BezierSegment()
                {
                    Point1 = new Point(startPoint.X + width / 4, startPoint.Y),
                    Point2 = new Point((((startPoint.X + width / 4) + startPoint.X) / 2) - width / 8, ((startPoint.Y + (center.Y - height / 4)) / 2) - height / 8),
                    Point3 = new Point(startPoint.X, center.Y - height / 4),
                    IsStroked = true
                });
                figure.Segments.Add(new BezierSegment()
                {
                    Point1 = new Point(startPoint.X, center.Y - height / 4),
                    Point2 = new Point(((startPoint.X + (startPoint.X + width / 4)) / 2) - width / 8, (((center.Y - height / 4) + (center.Y + height / 4)) / 2)),
                    Point3 = new Point(startPoint.X + width / 4, center.Y + height / 4),
                    IsStroked = true
                });
                figure.Segments.Add(new LineSegment(new Point(center.X, endPoint.Y), true));
                figure.Segments.Add(new LineSegment(new Point(endPoint.X - width / 4, center.Y + height / 4), true));
                figure.Segments.Add(new BezierSegment()
                {
                    Point1 = new Point(endPoint.X - width / 4, center.Y + height / 4),
                    Point2 = new Point((((endPoint.X - width / 4) + endPoint.X) / 2) + width / 8, (((center.Y + height / 4) + (center.Y - height / 4)) / 2)),
                    Point3 = new Point(endPoint.X, center.Y - height / 4),
                    IsStroked = true
                });
                figure.Segments.Add(new BezierSegment()
                {
                    Point1 = new Point(endPoint.X, center.Y - height / 4),
                    Point2 = new Point(((endPoint.X + (endPoint.X - width / 4)) / 2) + width / 8, (((center.Y - height / 4) + startPoint.Y) / 2) - height / 8),
                    Point3 = new Point(endPoint.X - width / 4, startPoint.Y),
                    IsStroked = true
                });
                figure.Segments.Add(new BezierSegment()
                {
                    Point1 = new Point(endPoint.X - width / 4, startPoint.Y),
                    Point2 = new Point((((endPoint.X - width / 4) + center.X) / 2) - width / 8, ((startPoint.Y + (center.Y - height / 4)) / 2) - height / 8),
                    Point3 = new Point(center.X, center.Y - height / 4),
                    IsStroked = true
                });
            }
            else if (status == "upside")
            {
                figure.StartPoint = new Point(center.X, center.Y - height / 4);

                figure.Segments.Add(new BezierSegment()
                {
                    Point1 = new Point(center.X, center.Y - height / 4),
                    Point2 = new Point(((center.X + (startPoint.X + width / 4)) / 2) + width / 8, (((center.Y - height / 4) + endPoint.Y) / 2) - height / 8),
                    Point3 = new Point(startPoint.X + width / 4, endPoint.Y),
                    IsStroked = true
                });
                figure.Segments.Add(new BezierSegment()
                {
                    Point1 = new Point(startPoint.X + width / 4, endPoint.Y),
                    Point2 = new Point((((startPoint.X + width / 4) + startPoint.X) / 2) - width / 8, ((endPoint.Y + (center.Y - height / 4)) / 2) - height / 8),
                    Point3 = new Point(startPoint.X, center.Y - height / 4),
                    IsStroked = true
                });
                figure.Segments.Add(new BezierSegment()
                {
                    Point1 = new Point(startPoint.X, center.Y - height / 4),
                    Point2 = new Point(((startPoint.X + (startPoint.X + width / 4)) / 2) - width / 8, (((center.Y - height / 4) + (center.Y + height / 4)) / 2)),
                    Point3 = new Point(startPoint.X + width / 4, center.Y + height / 4),
                    IsStroked = true
                });
                figure.Segments.Add(new LineSegment(new Point(center.X, startPoint.Y), true));
                figure.Segments.Add(new LineSegment(new Point(endPoint.X - width / 4, center.Y + height / 4), true));
                figure.Segments.Add(new BezierSegment()
                {
                    Point1 = new Point(endPoint.X - width / 4, center.Y + height / 4),
                    Point2 = new Point((((endPoint.X - width / 4) + endPoint.X) / 2) + width / 8, (((center.Y + height / 4) + (center.Y - height / 4)) / 2)),
                    Point3 = new Point(endPoint.X, center.Y - height / 4),
                    IsStroked = true
                });
                figure.Segments.Add(new BezierSegment()
                {
                    Point1 = new Point(endPoint.X, center.Y - height / 4),
                    Point2 = new Point(((endPoint.X + (endPoint.X - width / 4)) / 2) + width / 8, (((center.Y - height / 4) + endPoint.Y) / 2) - height / 8),
                    Point3 = new Point(endPoint.X - width / 4, endPoint.Y),
                    IsStroked = true
                });
                figure.Segments.Add(new BezierSegment()
                {
                    Point1 = new Point(endPoint.X - width / 4, endPoint.Y),
                    Point2 = new Point((((endPoint.X - width / 4) + center.X) / 2) - width / 8, ((endPoint.Y + (center.Y - height / 4)) / 2) - height / 8),
                    Point3 = new Point(center.X, center.Y - height / 4),
                    IsStroked = true
                });
            }
            else if (status == "reverse")
            {
                figure.StartPoint = new Point(center.X, center.Y - height / 4);

                figure.Segments.Add(new BezierSegment()
                {
                    Point1 = new Point(center.X, center.Y - height / 4),
                    Point2 = new Point(((center.X + (endPoint.X + width / 4)) / 2) + width / 8, (((center.Y - height / 4) + startPoint.Y) / 2) - height / 8),
                    Point3 = new Point(endPoint.X + width / 4, startPoint.Y),
                    IsStroked = true
                });
                figure.Segments.Add(new BezierSegment()
                {
                    Point1 = new Point(endPoint.X + width / 4, startPoint.Y),
                    Point2 = new Point((((endPoint.X + width / 4) + endPoint.X) / 2) - width / 8, ((startPoint.Y + (center.Y - height / 4)) / 2) - height / 8),
                    Point3 = new Point(endPoint.X, center.Y - height / 4),
                    IsStroked = true
                });
                figure.Segments.Add(new BezierSegment()
                {
                    Point1 = new Point(endPoint.X, center.Y - height / 4),
                    Point2 = new Point(((endPoint.X + (endPoint.X + width / 4)) / 2) - width / 8, (((center.Y - height / 4) + (center.Y + height / 4)) / 2)),
                    Point3 = new Point(endPoint.X + width / 4, center.Y + height / 4),
                    IsStroked = true
                });
                figure.Segments.Add(new LineSegment(new Point(center.X, endPoint.Y), true));
                figure.Segments.Add(new LineSegment(new Point(startPoint.X - width / 4, center.Y + height / 4), true));
                figure.Segments.Add(new BezierSegment()
                {
                    Point1 = new Point(startPoint.X - width / 4, center.Y + height / 4),
                    Point2 = new Point((((startPoint.X - width / 4) + startPoint.X) / 2) + width / 8, (((center.Y + height / 4) + (center.Y - height / 4)) / 2)),
                    Point3 = new Point(startPoint.X, center.Y - height / 4),
                    IsStroked = true
                });
                figure.Segments.Add(new BezierSegment()
                {
                    Point1 = new Point(startPoint.X, center.Y - height / 4),
                    Point2 = new Point(((startPoint.X + (startPoint.X - width / 4)) / 2) + width / 8, (((center.Y - height / 4) + startPoint.Y) / 2) - height / 8),
                    Point3 = new Point(startPoint.X - width / 4, startPoint.Y),
                    IsStroked = true
                });
                figure.Segments.Add(new BezierSegment()
                {
                    Point1 = new Point(startPoint.X - width / 4, startPoint.Y),
                    Point2 = new Point((((startPoint.X - width / 4) + center.X) / 2) - width / 8, ((startPoint.Y + (center.Y - height / 4)) / 2) - height / 8),
                    Point3 = new Point(center.X, center.Y - height / 4),
                    IsStroked = true
                });
            }
            else if (status == "upside-reverse")
            {
                figure.StartPoint = new Point(center.X, center.Y - height / 4);

                figure.Segments.Add(new BezierSegment()
                {
                    Point1 = new Point(center.X, center.Y - height / 4),
                    Point2 = new Point(((center.X + (endPoint.X + width / 4)) / 2) + width / 8, (((center.Y - height / 4) + endPoint.Y) / 2) - height / 8),
                    Point3 = new Point(endPoint.X + width / 4, endPoint.Y),
                    IsStroked = true
                });
                figure.Segments.Add(new BezierSegment()
                {
                    Point1 = new Point(endPoint.X + width / 4, endPoint.Y),
                    Point2 = new Point((((endPoint.X + width / 4) + endPoint.X) / 2) - width / 8, ((endPoint.Y + (center.Y - height / 4)) / 2) - height / 8),
                    Point3 = new Point(endPoint.X, center.Y - height / 4),
                    IsStroked = true
                });
                figure.Segments.Add(new BezierSegment()
                {
                    Point1 = new Point(endPoint.X, center.Y - height / 4),
                    Point2 = new Point(((endPoint.X + (endPoint.X + width / 4)) / 2) - width / 8, (((center.Y - height / 4) + (center.Y + height / 4)) / 2)),
                    Point3 = new Point(endPoint.X + width / 4, center.Y + height / 4),
                    IsStroked = true
                });
                figure.Segments.Add(new LineSegment(new Point(center.X, startPoint.Y), true));
                figure.Segments.Add(new LineSegment(new Point(startPoint.X - width / 4, center.Y + height / 4), true));
                figure.Segments.Add(new BezierSegment()
                {
                    Point1 = new Point(startPoint.X - width / 4, center.Y + height / 4),
                    Point2 = new Point((((startPoint.X - width / 4) + startPoint.X) / 2) + width / 8, (((center.Y + height / 4) + (center.Y - height / 4)) / 2)),
                    Point3 = new Point(startPoint.X, center.Y - height / 4),
                    IsStroked = true
                });
                figure.Segments.Add(new BezierSegment()
                {
                    Point1 = new Point(startPoint.X, center.Y - height / 4),
                    Point2 = new Point(((startPoint.X + (startPoint.X - width / 4)) / 2) + width / 8, (((center.Y - height / 4) + endPoint.Y) / 2) - height / 8),
                    Point3 = new Point(startPoint.X - width / 4, endPoint.Y),
                    IsStroked = true
                });
                figure.Segments.Add(new BezierSegment()
                {
                    Point1 = new Point(startPoint.X - width / 4, endPoint.Y),
                    Point2 = new Point((((startPoint.X - width / 4) + center.X) / 2) - width / 8, ((endPoint.Y + (center.Y - height / 4)) / 2) - height / 8),
                    Point3 = new Point(center.X, center.Y - height / 4),
                    IsStroked = true
                });
            }

            geometry.Figures.Add(figure);
            return geometry;
        }
    }
}
