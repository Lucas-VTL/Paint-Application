using myColor;
using myShape;
using myStroke;
using myWidthness;
using System.Configuration;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using Point = System.Windows.Point;
using Rectangle = System.Windows.Shapes.Rectangle;

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

        private Grid EditGrid;
        private Button StartButton;
        private Button EndButton;

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
        public Grid getEditGrid() { return EditGrid; }
        public Button getStartButton()
        {
            return StartButton;
        }
        public Button getEndButton()
        {
            return EndButton;
        }
        public Button getLeftTopButton() { return null; }
        public Button getRightTopButton() { return null; }
        public Button getLeftBottomButton() { return null; }
        public Button getRightBottomButton() { return null; }
        public Button getLeftCenterButton() { return null; }
        public Button getRightCenterButton() { return null; }
        public Button getTopCenterButton() { return null; }
        public Button getBottomCenterButton() { return null; }
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

                if (startPoint.X == endPoint.X || startPoint.Y == endPoint.Y)
                {
                    if (width == 0)
                    {
                        EditGrid = new Grid()
                        {
                            Width = 20,
                            Height = height,
                            Background = Brushes.Transparent,
                        };

                        Canvas.SetLeft(EditGrid, left - 10);
                        Canvas.SetTop(EditGrid, top);
                    } else
                    {
                        EditGrid = new Grid()
                        {
                            Width = width,
                            Height = 20,
                            Background = Brushes.Transparent,
                        };

                        Canvas.SetLeft(EditGrid, left);
                        Canvas.SetTop(EditGrid, top - 10);
                    }
                } else
                {
                    EditGrid = new Grid()
                    {
                        Width = width,
                        Height = height,
                        Background = Brushes.Transparent,
                    };

                    Canvas.SetLeft(EditGrid, left);
                    Canvas.SetTop(EditGrid, top);
                }

                StartButton = new Button();
                EndButton = new Button();

                if (startPoint.X < endPoint.X && startPoint.Y < endPoint.Y)
                {
                    StartButton.Width = 10;
                    StartButton.Height = 10;
                    StartButton.Background = Brushes.White;
                    Canvas.SetLeft(StartButton, left - 5);
                    Canvas.SetTop(StartButton, top - 5);

                    EndButton.Width = 10;
                    EndButton.Height = 10;
                    EndButton.Background = Brushes.White;
                    Canvas.SetLeft(EndButton, right - 5);
                    Canvas.SetTop(EndButton, bottom - 5);
                } else if (startPoint.X > endPoint.X && startPoint.Y > endPoint.Y)
                {
                    StartButton.Width = 10;
                    StartButton.Height = 10;
                    StartButton.Background = Brushes.White;
                    Canvas.SetLeft(StartButton, right - 5);
                    Canvas.SetTop(StartButton, bottom - 5);

                    EndButton.Width = 10;
                    EndButton.Height = 10;
                    EndButton.Background = Brushes.White;
                    Canvas.SetLeft(EndButton, left - 5);
                    Canvas.SetTop(EndButton, top - 5);
                }
                else if (startPoint.X < endPoint.X && startPoint.Y > endPoint.Y)
                {
                    StartButton.Width = 10;
                    StartButton.Height = 10;
                    StartButton.Background = Brushes.White;
                    Canvas.SetLeft(StartButton, left - 5);
                    Canvas.SetTop(StartButton, bottom - 5);

                    EndButton.Width = 10;
                    EndButton.Height = 10;
                    EndButton.Background = Brushes.White;
                    Canvas.SetLeft(EndButton, right - 5);
                    Canvas.SetTop(EndButton, top - 5);
                } else if (startPoint.X > endPoint.X && startPoint.Y < endPoint.Y) 
                {
                    StartButton.Width = 10;
                    StartButton.Height = 10;
                    StartButton.Background = Brushes.White;
                    Canvas.SetLeft(StartButton, right - 5);
                    Canvas.SetTop(StartButton, top - 5);

                    EndButton.Width = 10;
                    EndButton.Height = 10;
                    EndButton.Background = Brushes.White;
                    Canvas.SetLeft(EndButton, left - 5);
                    Canvas.SetTop(EndButton, bottom - 5);
                }
                else if (startPoint.X == endPoint.X && startPoint.Y < endPoint.Y)
                {
                    StartButton.Width = 10;
                    StartButton.Height = 10;
                    StartButton.Background = Brushes.White;
                    Canvas.SetLeft(StartButton, left + (width / 2) - 5);
                    Canvas.SetTop(StartButton, top - 5);

                    EndButton.Width = 10;
                    EndButton.Height = 10;
                    EndButton.Background = Brushes.White;
                    Canvas.SetLeft(EndButton, left + (width / 2) - 5);
                    Canvas.SetTop(EndButton, bottom - 5);
                }
                else if (startPoint.X == endPoint.X && startPoint.Y > endPoint.Y)
                {
                    StartButton.Width = 10;
                    StartButton.Height = 10;
                    StartButton.Background = Brushes.White;
                    Canvas.SetLeft(StartButton, left + (width / 2) - 5);
                    Canvas.SetTop(StartButton, bottom - 5);

                    EndButton.Width = 10;
                    EndButton.Height = 10;
                    EndButton.Background = Brushes.White;
                    Canvas.SetLeft(EndButton, left + (width / 2) - 5);
                    Canvas.SetTop(EndButton, top - 5);
                }
                else if (startPoint.Y == endPoint.Y && startPoint.X < endPoint.X)
                {
                    StartButton.Width = 10;
                    StartButton.Height = 10;
                    StartButton.Background = Brushes.White;
                    Canvas.SetLeft(StartButton, left - 5);
                    Canvas.SetTop(StartButton, top + (height / 2) - 5);

                    EndButton.Width = 10;
                    EndButton.Height = 10;
                    EndButton.Background = Brushes.White;
                    Canvas.SetLeft(EndButton, right - 5);
                    Canvas.SetTop(EndButton, top + (height / 2) - 5);
                }
                else if (startPoint.Y == endPoint.Y && startPoint.X > endPoint.X)
                {
                    StartButton.Width = 10;
                    StartButton.Height = 10;
                    StartButton.Background = Brushes.White;
                    Canvas.SetLeft(StartButton, right - 5);
                    Canvas.SetTop(StartButton, top + (height / 2) - 5);

                    EndButton.Width = 10;
                    EndButton.Height = 10;
                    EndButton.Background = Brushes.White;
                    Canvas.SetLeft(EndButton, left - 5);
                    Canvas.SetTop(EndButton, top + (height / 2) - 5);
                }

                canvas.Children.Add(element);
                canvas.Children.Add(EditGrid);
                canvas.Children.Add(StartButton);
                canvas.Children.Add(EndButton);

                return canvas;
            }

            return element;
        }
    }
}
