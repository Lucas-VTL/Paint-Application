using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Paint_Application
{
    public class ShapeFactory()
    {
        public enum ShapeType
        {
            Line, Rectangle, Ellipse, Square, Circle
        }

        public static Shape getShapeType(ShapeType shapeType)
        {
            switch (shapeType)
            {
                case ShapeType.Line: return new Line();
                case ShapeType.Rectangle: return new Rectangle();
                case ShapeType.Ellipse: return new Ellipse();
                case ShapeType.Square: return new Square();
                case ShapeType.Circle: return new Circle();
                default: return null;
            }
        }
    }

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}