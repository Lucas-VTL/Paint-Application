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
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        private bool isTextFontFamilyOpen = false;
        private bool isTextFontSizeOpen = false;
        private bool isFunctionSelected = false;
        private bool isStyleWidthOpen = false;

        private List<Border> function = new List<Border>();
        private List<Font> fonts = new List<Font>();

        private string globalFontFamily;
        private int globalFontSize = 12;
        private int globalWidth;

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

            textFontCombobox.MaxDropDownHeight = 160;

            function.Add(selectionBorder);
            function.Add(textBorder);
            function.Add(lineBorder);
            function.Add(rectangleBorder);
            function.Add(ellipseBorder);
            function.Add(fourstarBorder);
            function.Add(fivestarBorder);
            function.Add(heartBorder);
            function.Add(arrowBorder);
            function.Add(triangleBorder);
            function.Add(rightTriangleBorder);
            function.Add(rhombusBorder);
            function.Add(pentagonBorder);
            function.Add(hexagonBorder);
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

            isFunctionSelected = true;
        }

        private void selectionButtonClick(object sender, RoutedEventArgs e)
        {
            if (!isSelectionOpen)
            {
                selectionCombobox.IsDropDownOpen = true;
                selectionButtonContent.Source = new BitmapImage(new Uri("images/arrow-up.png", UriKind.Relative));
                isSelectionOpen = true;

                textFontCombobox.IsDropDownOpen = false;
                isTextFontFamilyOpen = false;

                fontSizeStackpanel.Visibility = Visibility.Collapsed;
                isTextFontSizeOpen = false;

                styleWidthCombobox.IsDropDownOpen = false;
                isStyleWidthOpen = false;
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
            if (!isTextFontFamilyOpen)
            {
                textFontCombobox.IsDropDownOpen = true;
                isTextFontFamilyOpen = true;

                selectionCombobox.IsDropDownOpen = false;
                selectionButtonContent.Source = new BitmapImage(new Uri("images/arrow-down.png", UriKind.Relative));
                isSelectionOpen = false;

                fontSizeStackpanel.Visibility = Visibility.Collapsed;
                isTextFontSizeOpen = false;

                styleWidthCombobox.IsDropDownOpen = false;
                isStyleWidthOpen = false;
            } else
            {
                textFontCombobox.IsDropDownOpen = false;
                isTextFontFamilyOpen = false;
            }
        }

        private void textFontComboboxPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isTextFontFamilyOpen = false;
        }

        private void textFontComboboxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dynamic item = textFontCombobox.SelectedItem as dynamic;
            if (item != null)
            {
                globalFontFamily = item.fontName;
            }
        }

        private void fontSizeButtonClick(object sender, RoutedEventArgs e)
        {
            if (!isTextFontSizeOpen)
            {
                string text = fontSizeTextbox.Text;
                int number;

                if (int.TryParse(text, out number))
                {
                    if (number >= 1 && number <= 72)
                    {
                        globalFontSize = number;
                        fontSizeTextbox.Text = number.ToString();
                    }
                    else
                    {
                        fontSizeTextbox.Text = globalFontSize.ToString();
                    }
                }
                else
                {
                    fontSizeTextbox.Text = globalFontSize.ToString();
                }

                fontSizeStackpanel.Visibility = Visibility.Visible;
                isTextFontSizeOpen = true;

                selectionCombobox.IsDropDownOpen = false;
                selectionButtonContent.Source = new BitmapImage(new Uri("images/arrow-down.png", UriKind.Relative));
                isSelectionOpen = false;

                textFontCombobox.IsDropDownOpen = false;
                isTextFontFamilyOpen = false;

                styleWidthCombobox.IsDropDownOpen = false;
                isStyleWidthOpen = false;
            } else
            {
                fontSizeStackpanel.Visibility = Visibility.Collapsed;
                isTextFontSizeOpen = false;
            }
        }

        private void increaseFontSizeClick(object sender, RoutedEventArgs e)
        {
            string curSizeString = fontSizeTextbox.Text;
            if (int.TryParse(curSizeString, out _))
            {
                int curSizeInt = Int32.Parse(curSizeString);
                globalFontSize = Math.Min(curSizeInt + 2, 72);
                fontSizeTextbox.Text = globalFontSize.ToString();
            } else
            {
                fontSizeTextbox.Text = globalFontSize.ToString();
            }
        }

        private void decreaseFontSizeClick(object sender, RoutedEventArgs e)
        {
            string curSizeString = fontSizeTextbox.Text;
            if (int.TryParse(curSizeString, out _))
            {
                int curSizeInt = Int32.Parse(curSizeString);
                globalFontSize = Math.Max(curSizeInt - 2, 1);
                fontSizeTextbox.Text = globalFontSize.ToString();
            } else
            {
                fontSizeTextbox.Text = globalFontSize.ToString();
            }
        }

        private void fontSizeTextboxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string text = fontSizeTextbox.Text;
                int number;

                if (int.TryParse(text, out number))
                {
                    if (number >= 1 && number <= 72)
                    {
                        globalFontSize = number;
                        fontSizeTextbox.Text = number.ToString();
                    } else
                    {
                        fontSizeTextbox.Text = globalFontSize.ToString();
                    }
                } else
                {
                    fontSizeTextbox.Text = globalFontSize.ToString();
                }
            }
        }

        private void lineButtonClick(object sender, RoutedEventArgs e)
        {
            functionSelected(2);
        }

        private void rectangleButtonClick(object sender, RoutedEventArgs e)
        {
            functionSelected(3);
        }

        private void ellipseButtonClick(object sender, RoutedEventArgs e)
        {
            functionSelected(4);
        }

        private void fourStarButtonClick(object sender, RoutedEventArgs e)
        {
            functionSelected(5);
        }

        private void fiveStarButtonClick(object sender, RoutedEventArgs e)
        {
            functionSelected(6);
        }

        private void heartButtonClick(object sender, RoutedEventArgs e)
        {
            functionSelected(7);
        }

        private void arrowButtonClick(object sender, RoutedEventArgs e)
        {
            functionSelected(8);
        }

        private void triangleButtonClick(object sender, RoutedEventArgs e)
        {
            functionSelected(9);
        }

        private void rightTriangleButtonClick(object sender, RoutedEventArgs e)
        {
            functionSelected(10);
        }

        private void rhombusButtonClick(object sender, RoutedEventArgs e)
        {
            functionSelected(11);
        }

        private void pentagonButtonClick(object sender, RoutedEventArgs e)
        {
            functionSelected(12);
        }

        private void hexagonButtonClick(object sender, RoutedEventArgs e)
        {
            functionSelected(13);
        }

        private void drawAreaMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            selectionCombobox.IsDropDownOpen = false;
            selectionButtonContent.Source = new BitmapImage(new Uri("images/arrow-down.png", UriKind.Relative));
            isSelectionOpen = false;

            textFontCombobox.IsDropDownOpen = false;
            isTextFontFamilyOpen = false;
        }

        private void drawAreaMouseMove(object sender, MouseEventArgs e)
        {
            if (isFunctionSelected)
            {
                drawArea.Cursor = Cursors.Cross;
            } else
            {
                drawArea.Cursor = null;
            }
        }

        private void styleWidthButtonClick(object sender, RoutedEventArgs e)
        {
            if (!isStyleWidthOpen)
            {
                styleWidthCombobox.IsDropDownOpen = true;
                isStyleWidthOpen = true;

                selectionCombobox.IsDropDownOpen = false;
                selectionButtonContent.Source = new BitmapImage(new Uri("images/arrow-down.png", UriKind.Relative));
                isSelectionOpen = false;

                textFontCombobox.IsDropDownOpen = false;
                isTextFontFamilyOpen = false;

                fontSizeStackpanel.Visibility = Visibility.Collapsed;
                isTextFontSizeOpen = false;
            } else
            {
                styleWidthCombobox.IsDropDownOpen = false;
                isStyleWidthOpen = false;
            }
        }

        private void styleWidthComboboxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = styleWidthCombobox.SelectedIndex;

            switch (index)
            {
                case 0:
                    globalWidth = 1;
                    styleWidthImage.Source = new BitmapImage(new Uri("images/styleBaseLine.png", UriKind.Relative));
                    break;
                case 1:
                    globalWidth = 3;
                    styleWidthImage.Source = new BitmapImage(new Uri("images/styleWidth1.png", UriKind.Relative));
                    break;
                case 2:
                    globalWidth = 5;
                    styleWidthImage.Source = new BitmapImage(new Uri("images/styleWidth2.png", UriKind.Relative));
                    break;
                case 3:
                    globalWidth = 8;
                    styleWidthImage.Source = new BitmapImage(new Uri("images/styleWidth3.png", UriKind.Relative));
                    break;
                default: break;
            }
        }

        private void styleWidthComboboxPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isStyleWidthOpen = false;
        }
    }
}