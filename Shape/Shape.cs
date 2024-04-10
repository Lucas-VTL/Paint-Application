using System.Windows;

namespace Shape
{
    public interface IShape : ICloneable
    {
        void addStartPoint(Point point);
        void addEndPoint(Point point);

        string shapeName { get; }
        string shapeImage { get; }

        UIElement convertShape();   
    }
}
