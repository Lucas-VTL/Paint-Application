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
using System.Windows.Media;

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

    public class Font()
    {
        public string fontName { get; set; }
    }

    public partial class MainWindow : Window
    {
        private bool isSelectionOpen = false;
        private bool isTextBold = false;
        private bool isTextItalic = false;
        private bool isTextBackgroundFill = false;
        private bool isTextFontOpen = false;

        private List<Border> function = new List<Border>();
        private List<Font> fonts = new List<Font>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            var fontList = Fonts.SystemFontFamilies;
            for (int i = 0; i < fontList.Count; i++)
            {
                Font tempFont = new Font {fontName = fontList.ElementAt(i).Source};
                fonts.Add(tempFont);
            }
            textFontCombobox.ItemsSource = fonts;

            selectionCombobox.SelectedIndex = 0;
            textFontCombobox.SelectedIndex = 0;

            textFontCombobox.MaxDropDownHeight = 200;

            function.Add(selectionBorder);
            function.Add(textBorder);
        }

        private void minimizeButtonClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void closeButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void functionSelected(int index)
        {
            function[index].Opacity = 1;

            for (int i = 0; i < function.Count; i++)
            {
                if (i != index)
                {
                    function[i].Opacity = 0;
                }
            }
        }

        private void selectionButtonClick(object sender, RoutedEventArgs e)
        {
            if (!isSelectionOpen)
            {
                selectionCombobox.IsDropDownOpen = true;
                selectionButtonContent.Source = new BitmapImage(new Uri("images/arrow-up.png", UriKind.Relative));
                isSelectionOpen = true;

                textFontCombobox.IsDropDownOpen = false;
                isTextFontOpen = false;
            } else
            {
                selectionCombobox.IsDropDownOpen = false;
                selectionButtonContent.Source = new BitmapImage(new Uri("images/arrow-down.png", UriKind.Relative));
                isSelectionOpen = false;
            }
        }

        private void selecionStackpanelMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            functionSelected(0);
        }

        private void selectionComboboxPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            selectionButtonContent.Source = new BitmapImage(new Uri("images/arrow-down.png", UriKind.Relative));
            isSelectionOpen = false;
            functionSelected(0);
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

        private void textButtonClick(object sender, RoutedEventArgs e)
        {
            functionSelected(1);
        }

        private void textBoldButtonClick(object sender, RoutedEventArgs e)
        {
            if (!isTextBold)
            {
                textBoldBorder.Opacity = 1;
                isTextBold = true;
            } else
            {
                textBoldBorder.Opacity = 0;
                isTextBold = false;
            }
        }

        private void textItalicButtonClick(object sender, RoutedEventArgs e)
        {
            if (!isTextItalic)
            {
                textItalicBorder.Opacity = 1;
                isTextItalic = true;
            } else
            {
                textItalicBorder.Opacity = 0;
                isTextItalic= false;
            }
        }

        private void textBackgroundButtonClick(object sender, RoutedEventArgs e)
        {
            if (!isTextBackgroundFill)
            {
                textBackgroundImage.Source = new BitmapImage(new Uri("images/textBackgroundEffect.png", UriKind.Relative));
                isTextBackgroundFill = true;
            } else
            {
                textBackgroundImage.Source = new BitmapImage(new Uri("images/textBackground.png", UriKind.Relative));
                isTextBackgroundFill = false;
            }
        }

        private void textFontButtonClick(object sender, RoutedEventArgs e)
        {
            if (!isTextFontOpen)
            {
                textFontCombobox.IsDropDownOpen = true;
                isTextFontOpen = true;

                selectionCombobox.IsDropDownOpen = false;
                selectionButtonContent.Source = new BitmapImage(new Uri("images/arrow-down.png", UriKind.Relative));
                isSelectionOpen = false;
            } else
            {
                textFontCombobox.IsDropDownOpen = false;
                isTextFontOpen = false;
            }
        }

        private void textFontComboboxPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}