using myColor;
using myShape;
using myStroke;
using myWidthness;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace myRightTriangle
{
    public class myRightTriangle : IShape
    {
        private Point startPoint;
        private Point endPoint;
        private IWidthness widthness;
        private IStroke strokeStyle;
        private IColor colorValue;
        private bool isFill;
        private bool isEdit;

        private Grid EditGrid;
        private Button LeftTopButton;
        private Button RightTopButton;
        private Button LeftBottomButton;
        private Button RightBottomButton;
        private Button LeftCenterButton;
        private Button RightCenterButton;
        private Button TopCenterButton;
        private Button BottomCenterButton;
        private Button RotateButton;

        public string shapeName => "RightTriangle";
        public string shapeImage => "images/shapeRightTriangle.png";

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
            var left = Math.Min(startPoint.X, endPoint.X);
            var right = Math.Max(startPoint.X, endPoint.X);

            var top = Math.Min(startPoint.Y, endPoint.Y);
            var bottom = Math.Max(startPoint.Y, endPoint.Y);

            var width = right - left;
            var height = bottom - top;

            return new Point(left + width / 3, bottom - height / 3);
        }
        public void setShapeFill(bool isShapeFill)
        {
            isFill = isShapeFill;
        }
        public void setEdit(bool edit)
        {
            isEdit = edit;
        }
        public Grid getEditGrid()
        {
            return EditGrid;
        }
        public Button getStartButton()
        {
            return null;
        }
        public Button getEndButton()
        {
            return null;
        }
        public Button getLeftTopButton()
        {
            return LeftTopButton;
        }
        public Button getRightTopButton()
        {
            return RightTopButton;
        }
        public Button getLeftBottomButton()
        {
            return LeftBottomButton;
        }
        public Button getRightBottomButton()
        {
            return RightBottomButton;
        }
        public Button getLeftCenterButton()
        {
            return LeftCenterButton;
        }
        public Button getRightCenterButton()
        {
            return RightCenterButton;
        }
        public Button getTopCenterButton()
        {
            return TopCenterButton;
        }
        public Button getBottomCenterButton()
        {
            return BottomCenterButton;
        }
        public Button getRotateButton()
        {
            return RotateButton;
        }
        public object Clone()
        {
            return MemberwiseClone();
        }

        public UIElement convertShapeType()
        {
            var left = Math.Min(startPoint.X, endPoint.X);
            var right = Math.Max(startPoint.X, endPoint.X);

            var top = Math.Min(startPoint.Y, endPoint.Y);
            var bottom = Math.Max(startPoint.Y, endPoint.Y);

            var width = right - left;
            var height = bottom - top;

            var center = new Point((startPoint.X + endPoint.X) / 2, (startPoint.Y + endPoint.Y) / 2);

            var hypotenuseLength = Math.Sqrt(width * width + height * height);

            Polygon element;

            if (isFill)
            {
                element = new Polygon
                {
                    Stroke = colorValue.colorValue,
                    StrokeThickness = widthness.widthnessValue,
                    StrokeDashArray = strokeStyle.strokeValue,
                    Fill = colorValue.colorValue,
                    Points = CreateRightTrianglePoints(center, width, height, hypotenuseLength)
                };
            } else
            {
                element = new Polygon
                {
                    Stroke = colorValue.colorValue,
                    StrokeThickness = widthness.widthnessValue,
                    StrokeDashArray = strokeStyle.strokeValue,
                    Points = CreateRightTrianglePoints(center, width, height, hypotenuseLength)
                };
            }

            if (isEdit)
            {
                Canvas canvas = new Canvas();

                EditGrid = new Grid()
                {
                    Width = width,
                    Height = height,
                    Background = Brushes.Transparent,
                };

                Canvas.SetLeft(EditGrid, left);
                Canvas.SetTop(EditGrid, top);

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

                LeftTopButton = new Button();
                LeftTopButton.Width = 10;
                LeftTopButton.Height = 10;
                LeftTopButton.Background = Brushes.White;
                Canvas.SetLeft(LeftTopButton, left - 5);
                Canvas.SetTop(LeftTopButton, top - 5);

                RightTopButton = new Button();
                RightTopButton.Width = 10;
                RightTopButton.Height = 10;
                RightTopButton.Background = Brushes.White;
                Canvas.SetLeft(RightTopButton, right - 5);
                Canvas.SetTop(RightTopButton, top - 5);

                LeftBottomButton = new Button();
                LeftBottomButton.Width = 10;
                LeftBottomButton.Height = 10;
                LeftBottomButton.Background = Brushes.White;
                Canvas.SetLeft(LeftBottomButton, left - 5);
                Canvas.SetTop(LeftBottomButton, bottom - 5);

                RightBottomButton = new Button();
                RightBottomButton.Width = 10;
                RightBottomButton.Height = 10;
                RightBottomButton.Background = Brushes.White;
                Canvas.SetLeft(RightBottomButton, right - 5);
                Canvas.SetTop(RightBottomButton, bottom - 5);

                LeftCenterButton = new Button();
                LeftCenterButton.Width = 10;
                LeftCenterButton.Height = 10;
                LeftCenterButton.Background = Brushes.White;
                Canvas.SetLeft(LeftCenterButton, left - 5);
                Canvas.SetTop(LeftCenterButton, top + (height / 2) - 5);

                RightCenterButton = new Button();
                RightCenterButton.Width = 10;
                RightCenterButton.Height = 10;
                RightCenterButton.Background = Brushes.White;
                Canvas.SetLeft(RightCenterButton, right - 5);
                Canvas.SetTop(RightCenterButton, top + (height / 2) - 5);

                TopCenterButton = new Button();
                TopCenterButton.Width = 10;
                TopCenterButton.Height = 10;
                TopCenterButton.Background = Brushes.White;
                Canvas.SetLeft(TopCenterButton, left + (width / 2) - 5);
                Canvas.SetTop(TopCenterButton, top - 5);

                BottomCenterButton = new Button();
                BottomCenterButton.Width = 10;
                BottomCenterButton.Height = 10;
                BottomCenterButton.Background = Brushes.White;
                Canvas.SetLeft(BottomCenterButton, left + (width / 2) - 5);
                Canvas.SetTop(BottomCenterButton, bottom - 5);

                RotateButton = new Button();
                RotateButton.Width = 20;
                RotateButton.Height = 20;
                RotateButton.Background = Brushes.White;
                Canvas.SetLeft(RotateButton, left + (width / 2) - 10);
                Canvas.SetTop(RotateButton, top - 40);

                canvas.Children.Add(rectangle);
                canvas.Children.Add(element);
                canvas.Children.Add(EditGrid);

                canvas.Children.Add(LeftTopButton);
                canvas.Children.Add(RightTopButton);
                canvas.Children.Add(LeftBottomButton);
                canvas.Children.Add(RightBottomButton);

                canvas.Children.Add(LeftCenterButton);
                canvas.Children.Add(RightCenterButton);
                canvas.Children.Add(TopCenterButton);
                canvas.Children.Add(BottomCenterButton);

                canvas.Children.Add(RotateButton);

                return canvas;
            }

            return element;
        }

        private PointCollection CreateRightTrianglePoints(Point center, double width, double height, double hypotenuseLength)
        {
            var points = new PointCollection();

            points.Add(new Point(center.X - width / 2, center.Y + height / 2));
            points.Add(new Point(center.X - width / 2, center.Y - height / 2));
            points.Add(new Point(center.X + width / 2, center.Y + height / 2));

            return points;
        }
    }
}
