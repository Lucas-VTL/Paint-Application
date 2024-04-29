using myColor;
using myShape;
using myStroke;
using myWidthness;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace myLine
{
    public class myLine : IShape
    {
        private Point startPoint;
        private Point endPoint;
        private IWidthness widthness;
        private IStroke strokeStyle;
        private IColor colorValue;
        private bool isFill;
        private bool isEdit;

        public string shapeName => "Line";
        public string shapeImage => "images/shapeLine.png";

        public void addStartPoint (Point point) {startPoint = point;}
        public void addEndPoint (Point point) {endPoint = point;}
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

        public UIElement convertShapeType() {
            Line element;

            var left = Math.Min(startPoint.X, endPoint.X);
            var right = Math.Max(startPoint.X, endPoint.X);

            var top = Math.Min(startPoint.Y, endPoint.Y);
            var bottom = Math.Max(startPoint.Y, endPoint.Y);

            var width = right - left;
            var height = bottom - top;

            if (isFill)
            {
                element = new Line()
                {
                    X1 = startPoint.X,
                    Y1 = startPoint.Y,
                    X2 = endPoint.X,
                    Y2 = endPoint.Y,
                    Stroke = colorValue.colorValue,
                    StrokeThickness = widthness.widthnessValue,
                    StrokeDashArray = strokeStyle.strokeValue,
                    Fill = colorValue.colorValue,
                };
            } else
            {
                element = new Line()
                {
                    X1 = startPoint.X,
                    Y1 = startPoint.Y,
                    X2 = endPoint.X,
                    Y2 = endPoint.Y,
                    Stroke = colorValue.colorValue,
                    StrokeThickness = widthness.widthnessValue,
                    StrokeDashArray = strokeStyle.strokeValue,
                };
            }

            if (isEdit)
            {
                Canvas canvas = new Canvas();
                Button startButton = new Button();
                Button endButton = new Button();

                if ((startPoint.X < endPoint.X && startPoint.Y < endPoint.Y) ||
                    (startPoint.X > endPoint.X && startPoint.Y > endPoint.Y))
                {
                    startButton.Width = 10;
                    startButton.Height = 10;
                    startButton.Background = Brushes.White;
                    Canvas.SetLeft(startButton, left - 5);
                    Canvas.SetTop(startButton, top - 5);

                    endButton.Width = 10;
                    endButton.Height = 10;
                    endButton.Background = Brushes.White;
                    Canvas.SetLeft(endButton, right - 5);
                    Canvas.SetTop(endButton, bottom - 5);
                }
                else if ((startPoint.X < endPoint.X && startPoint.Y > endPoint.Y) ||
                    (startPoint.X > endPoint.X && startPoint.Y < endPoint.Y))
                {
                    startButton.Width = 10;
                    startButton.Height = 10;
                    startButton.Background = Brushes.White;
                    Canvas.SetLeft(startButton, left - 5);
                    Canvas.SetTop(startButton, bottom - 5);

                    endButton.Width = 10;
                    endButton.Height = 10;
                    endButton.Background = Brushes.White;
                    Canvas.SetLeft(endButton, right - 5);
                    Canvas.SetTop(endButton, top - 5);
                }
                else if (startPoint.X == endPoint.X && startPoint.Y < endPoint.Y)
                {
                    startButton.Width = 10;
                    startButton.Height = 10;
                    startButton.Background = Brushes.White;
                    Canvas.SetLeft(startButton, left + (width / 2) - 5);
                    Canvas.SetTop(startButton, top - 5);

                    endButton.Width = 10;
                    endButton.Height = 10;
                    endButton.Background = Brushes.White;
                    Canvas.SetLeft(endButton, left + (width / 2) - 5);
                    Canvas.SetTop(endButton, bottom - 5);
                }
                else if (startPoint.X == endPoint.X && startPoint.Y > endPoint.Y)
                {
                    startButton.Width = 10;
                    startButton.Height = 10;
                    startButton.Background = Brushes.White;
                    Canvas.SetLeft(startButton, left + (width / 2) - 5);
                    Canvas.SetTop(startButton, bottom - 5);

                    endButton.Width = 10;
                    endButton.Height = 10;
                    endButton.Background = Brushes.White;
                    Canvas.SetLeft(endButton, left + (width / 2) - 5);
                    Canvas.SetTop(endButton, top - 5);
                }
                else if (startPoint.Y == endPoint.Y && startPoint.X < endPoint.X)
                {
                    startButton.Width = 10;
                    startButton.Height = 10;
                    startButton.Background = Brushes.White;
                    Canvas.SetLeft(startButton, left - 5);
                    Canvas.SetTop(startButton, top + (height / 2) - 5);

                    endButton.Width = 10;
                    endButton.Height = 10;
                    endButton.Background = Brushes.White;
                    Canvas.SetLeft(endButton, right - 5);
                    Canvas.SetTop(endButton, top + (height / 2) - 5);
                }
                else if (startPoint.Y == endPoint.Y && startPoint.X > endPoint.X)
                {
                    startButton.Width = 10;
                    startButton.Height = 10;
                    startButton.Background = Brushes.White;
                    Canvas.SetLeft(startButton, right - 5);
                    Canvas.SetTop(startButton, top + (height / 2) - 5);

                    endButton.Width = 10;
                    endButton.Height = 10;
                    endButton.Background = Brushes.White;
                    Canvas.SetLeft(endButton, left - 5);
                    Canvas.SetTop(endButton, top + (height / 2) - 5);
                }

                canvas.Children.Add(element);
                canvas.Children.Add(startButton);
                canvas.Children.Add(endButton);

                return canvas;
            }

            return element;
        }
    }
}
