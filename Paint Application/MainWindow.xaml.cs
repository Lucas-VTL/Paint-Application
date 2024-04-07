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
        private bool isSelectionOpen = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            selectionCombobox.SelectedIndex = 0;
        }

        private void minimizeButtonClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void closeButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void selectionButtonClick(object sender, RoutedEventArgs e)
        {
            if (!isSelectionOpen)
            {
                selectionCombobox.IsDropDownOpen = true;
                selectionButtonContent.Source = new BitmapImage(new Uri("images/arrow-up.png", UriKind.Relative));
                isSelectionOpen = true;
            } else
            {
                selectionCombobox.IsDropDownOpen = false;
                selectionButtonContent.Source = new BitmapImage(new Uri("images/arrow-down.png", UriKind.Relative));
                isSelectionOpen = false;
            }
        }

        private void selecionStackpanelMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            selectionBorder.Opacity = 1;
        }

        private void selectionComboboxPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            selectionButtonContent.Source = new BitmapImage(new Uri("images/arrow-down.png", UriKind.Relative));
            isSelectionOpen = false;
            selectionBorder.Opacity = 1;
        }

        private void selectionComboboxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = selectionCombobox.SelectedIndex;
            switch (index)
            {
                case 0:
                    selectionImage.Source = new BitmapImage(new Uri("images/rec-selection.png", UriKind.Relative));
                    break;
                case 1:
                    selectionImage.Source = new BitmapImage(new Uri("images/free-selection.png", UriKind.Relative));
                    break;
                case 2:
                    selectionImage.Source = new BitmapImage(new Uri("images/all-selection.png", UriKind.Relative));
                    break;
                default: break;
            }

            selectionButtonContent.Source = new BitmapImage(new Uri("images/arrow-down.png", UriKind.Relative));
            isSelectionOpen = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            selectionBorder.Opacity = 0;
        }
    }
}