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
        UIElement convertShapeType();
        string shapeName { get;}
        string shapeImage { get;}
    }
}
