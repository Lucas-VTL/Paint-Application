using System.Windows;

namespace myShape
{
    public interface IShape : ICloneable
    {
        void addStartPoint(Point point);
        void addEndPoint(Point point);
        UIElement convertShapeType();
        string shapeName { get;}
        string shapeImage { get;}
    }
}
