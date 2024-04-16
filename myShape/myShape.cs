using myWidthness;
using System.Windows;

namespace myShape
{
    public interface IShape : ICloneable
    {
        void addStartPoint(Point point);
        void addEndPoint(Point point);
        void addWidthness(IWidthness widthness);
        UIElement convertShapeType();
        string shapeName { get;}
        string shapeImage { get;}
    }
}
