using myColor;
using myShape;
using myStroke;
using myWidthness;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Controls;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace myText
{
    public class myText : IShape
    {
        TextBox myTextBox;
        string myTextString = "";

        bool isFocus;
        bool isBold;
        bool isItalic;

        Point startPoint;
        Point endPoint;

        string fontFamily;
        int fontSize;

        private IColor colorValue;
        private SolidColorBrush fillValue = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        
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

        private bool isFlipHorizontally;
        private bool isFlipVertically;
        private double rotateAngle = 0;
        public IWidthness getWidthness() { return null; }
        public IStroke getStrokeStyle() { return null; }
        public IColor getColor() { return colorValue; }
        public List<Point> getPointList() { return null; }
        public void setAngle(double angle)
        {
            rotateAngle = angle;
        }
        public double getAngle()
        {
            return rotateAngle;
        }
        public string shapeName => "Text";
        public string shapeImage => "";

        public void addStartPoint(Point point) 
        { 
            startPoint = point;
        }
        public void addEndPoint(Point point) 
        {
            endPoint = point;
        }
        public void addWidthness(IWidthness width) {}
        public void addStrokeStyle(IStroke stroke) {}
        public void addPointList(List<Point> pointList) { }
        public void addFontSize(int size) { fontSize = size; }
        public void addFontFamily(string family) { fontFamily = family; }
        public void setFocus(bool focus) 
        {
            isFocus = focus;   
        }
        public void setBold(bool bold) 
        {
            isBold = bold;
        }
        public void setItalic(bool italic) 
        {
            isItalic = italic;
        }
        public void addColor(IColor color)
        {
            colorValue = color;
        }
        public TextBox getTextBox() { return myTextBox; }
        public void setTextString(string text) 
        { 
            myTextString = text;
        }
        public void setShapeFill(bool isShapeFill) 
        {
            isFill = isShapeFill;
        }
        public void setBackground(byte r, byte g, byte b) 
        {
            fillValue = new SolidColorBrush(Color.FromRgb(r, g, b));
        }
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
        public void setFlipHorizontally(bool flipHorizontally)
        {
            isFlipHorizontally = flipHorizontally;
        }
        public void setFlipVertically(bool flipVertically)
        {
            isFlipVertically = flipVertically;
        }
        public bool getFlipHorizontally()
        {
            return isFlipHorizontally;
        }
        public bool getFlipVertically()
        {
            return isFlipVertically;
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

            Canvas canvas = new Canvas();

            if (isFocus)
            {
                Rectangle rectangle = new Rectangle()
                {
                    Stroke = Brushes.Black,
                    StrokeThickness = 1,
                    StrokeDashArray = new DoubleCollection() { 10, 2 },
                    Width = width + 4,
                    Height = height + 4,
                };

                Canvas.SetLeft(rectangle, left - 2);
                Canvas.SetTop(rectangle, top - 2);

                canvas.Children.Add(rectangle);
            }

            if (isFill)
            {
                if (isBold && isItalic)
                {
                    myTextBox = new TextBox()
                    {
                        FontFamily = new FontFamily(fontFamily),
                        FontSize = fontSize,
                        BorderThickness = new Thickness(0),
                        Background = fillValue,
                        Foreground = colorValue.colorValue,
                        Text = myTextString,
                        TextWrapping = TextWrapping.Wrap,
                        AcceptsReturn = true,
                        FontWeight = FontWeights.Bold,
                        FontStyle = FontStyles.Italic,
                        Width = width,
                        Height = height,
                    };
                }
                else if (!isBold && isItalic)
                {
                    myTextBox = new TextBox()
                    {
                        FontFamily = new FontFamily(fontFamily),
                        FontSize = fontSize,
                        BorderThickness = new Thickness(0),
                        Background = fillValue,
                        Foreground = colorValue.colorValue,
                        Text = myTextString,
                        TextWrapping = TextWrapping.Wrap,
                        AcceptsReturn = true,
                        FontStyle = FontStyles.Italic,
                        Width = width,
                        Height = height,
                    };
                }
                else if (isBold && !isItalic)
                {
                    myTextBox = new TextBox()
                    {
                        FontFamily = new FontFamily(fontFamily),
                        FontSize = fontSize,
                        BorderThickness = new Thickness(0),
                        Background = fillValue,
                        Foreground = colorValue.colorValue,
                        Text = myTextString,
                        TextWrapping = TextWrapping.Wrap,
                        AcceptsReturn = true,
                        FontWeight = FontWeights.Bold,
                        Width = width,
                        Height = height,
                    };
                }
                else
                {
                    myTextBox = new TextBox()
                    {
                        FontFamily = new FontFamily(fontFamily),
                        FontSize = fontSize,
                        BorderThickness = new Thickness(0),
                        Background = fillValue,
                        Foreground = colorValue.colorValue,
                        Text = myTextString,
                        TextWrapping = TextWrapping.Wrap,
                        AcceptsReturn = true,
                        Width = width,
                        Height = height,
                    };
                }
            } else
            {
                if (isBold && isItalic)
                {
                    myTextBox = new TextBox()
                    {
                        FontFamily = new FontFamily(fontFamily),
                        FontSize = fontSize,
                        BorderThickness = new Thickness(0),
                        Background = Brushes.Transparent,
                        Foreground = colorValue.colorValue,
                        Text = myTextString,
                        TextWrapping = TextWrapping.Wrap,
                        AcceptsReturn = true,
                        FontWeight = FontWeights.Bold,
                        FontStyle = FontStyles.Italic,
                        Width = width,
                        Height = height,
                    };
                }
                else if (!isBold && isItalic)
                {
                    myTextBox = new TextBox()
                    {
                        FontFamily = new FontFamily(fontFamily),
                        FontSize = fontSize,
                        BorderThickness = new Thickness(0),
                        Background = Brushes.Transparent,
                        Foreground = colorValue.colorValue,
                        Text = myTextString,
                        TextWrapping = TextWrapping.Wrap,
                        AcceptsReturn = true,
                        FontStyle = FontStyles.Italic,
                        Width = width,
                        Height = height,
                    };
                }
                else if (isBold && !isItalic)
                {
                    myTextBox = new TextBox()
                    {
                        FontFamily = new FontFamily(fontFamily),
                        FontSize = fontSize,
                        BorderThickness = new Thickness(0),
                        Background = Brushes.Transparent,
                        Foreground = colorValue.colorValue,
                        Text = myTextString,
                        TextWrapping = TextWrapping.Wrap,
                        AcceptsReturn = true,
                        FontWeight = FontWeights.Bold,
                        Width = width,
                        Height = height,
                    };
                }
                else
                {
                    myTextBox = new TextBox()
                    {
                        FontFamily = new FontFamily(fontFamily),
                        FontSize = fontSize,
                        BorderThickness = new Thickness(0),
                        Background = Brushes.Transparent,
                        Foreground = colorValue.colorValue,
                        Text = myTextString,
                        TextWrapping = TextWrapping.Wrap,
                        AcceptsReturn = true,
                        Width = width,
                        Height = height,
                    };
                }
            }

            Canvas.SetLeft(myTextBox, left);
            Canvas.SetTop(myTextBox, top);

            if (isFlipHorizontally && !isFlipVertically)
            {
                myTextBox.RenderTransformOrigin = new Point(0.5, 0.5);
                myTextBox.RenderTransform = new ScaleTransform(-1, 1);

                Canvas.SetLeft(myTextBox, left);
            }
            else if (!isFlipHorizontally && isFlipVertically)
            {
                myTextBox.RenderTransformOrigin = new Point(0.5, 0.5);
                myTextBox.RenderTransform = new ScaleTransform(1, -1);

                Canvas.SetTop(myTextBox, top);
            }
            else if (isFlipHorizontally && isFlipVertically)
            {
                myTextBox.RenderTransformOrigin = new Point(0.5, 0.5);
                myTextBox.RenderTransform = new ScaleTransform(-1, -1);

                Canvas.SetLeft(myTextBox, left);
                Canvas.SetTop(myTextBox, top);
            }

            canvas.Children.Add(myTextBox);

            if (isEdit)
            {
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

            return canvas;
        }
    }
}
