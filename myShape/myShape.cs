using myColor;
using myStroke;
using myWidthness;
using System.Windows;

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
        UIElement convertShapeType();
        string shapeName { get;}
        string shapeImage { get;}
    }
}
