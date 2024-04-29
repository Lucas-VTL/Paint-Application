using myColor;
using myStroke;
using myWidthness;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using Point = System.Windows.Point;
using Rectangle = System.Windows.Shapes.Rectangle;

namespace myShape
{
    public interface IShape : ICloneable
    {
        void addStartPoint(Point point);
        void addEndPoint(Point point);
        void addWidthness(IWidthness widthness);
        void addStrokeStyle(IStroke strokeStyle);
        void addColor(IColor color);
        void addPointList(List<Point> pointList);
        void setShapeFill(bool isShapeFill);
        void addFontSize(int fontSize);
        void addFontFamily(string fontFamily);
        TextBox getTextBox();
        void setTextString(String text);
        void setFocus(bool focus);
        void setBold(bool bold);
        void setItalic(bool italic);
        void setBackground(byte r, byte g, byte b);
        Point getStartPoint();
        Point getEndPoint();
        Point getCenterPoint();
        void setEdit(bool edit);
        Rectangle getEditRectangle();
        Button getStartButton();
        Button getEndButton();
        Button getLeftTopButton();
        Button getRightTopButton();
        Button getLeftBottomButton();
        Button getRightBottomButton();
        Button getLeftCenterButton();
        Button getRightCenterButton();
        Button getTopCenterButton();
        Button getBottomCenterButton();
        UIElement convertShapeType();
        string shapeName { get;}
        string shapeImage { get;}
    }
}
