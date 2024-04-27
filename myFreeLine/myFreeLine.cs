using myColor;
using myShape;
using myStroke;
using myWidthness;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Point = System.Windows.Point;

namespace myFreeLine
{
    public class myFreeLine : IShape
    {
        List<Point> list;
        Canvas cloneDrawSurface;
        public string shapeName => "Free Line";
        public string shapeImage => "";

        public void addStartPoint(Point point) {}
        public void addEndPoint(Point point) {}
        public void addWidthness(IWidthness width) {}
        public void addStrokeStyle(IStroke stroke) {}
        public void addColor(IColor color) {}
        public void setShapeFill(bool isShapeFill) {}
        public void addFontSize(int fontSize) {}
        public void addFontFamily(string fontFamily) {}
        public TextBox getTextBox() { return null; }
        public void setTextString(string text) { }
        public void setFocus(bool focus) { }
        public void setBold(bool bold) { }
        public void setItalic(bool italic) { }
        public void setBackground(byte r, byte g, byte b) { }
        public void addPointList(List<Point> pointList) 
        {
            list = new List<Point>(0);
            list.AddRange(pointList);
        }
        public object Clone()
        {
            return MemberwiseClone();
        }

        public UIElement convertShapeType()
        {
            if (cloneDrawSurface == null)
            {
                cloneDrawSurface = new Canvas();
                cloneDrawSurface.Background = Brushes.Transparent;
                cloneDrawSurface.IsHitTestVisible = false;

                for (int i = 0; i < list.Count; i++)
                {
                    Ellipse dot = new Ellipse();
                    dot.Fill = Brushes.White;
                    dot.Width = dot.Height = 20;
                    Canvas.SetLeft(dot, list[i].X);
                    if (list[i].Y >= 20)
                    {
                        Canvas.SetTop(dot, list[i].Y - 20);
                    }

                    cloneDrawSurface.Children.Add(dot);
                }

                return cloneDrawSurface;
            } else
            {
                return cloneDrawSurface;
            }
        }
    }
}
