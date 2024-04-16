﻿using System.Diagnostics;
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
using System.Windows.Resources;
using System.IO;
using System.Reflection;
using System;
using myShape;
using myWidthness;
using myStroke;

namespace Paint_Application
{
    public class Font()
    {
        public string fontName { get; set; }
    }

    public partial class MainWindow : Window
    {
        //Các bước boolean kiểm tra tình trạng đóng/mở của các combobox hoặc các function
        private bool isSelectionOpen = false;
        private bool isTextBold = false;
        private bool isTextItalic = false;
        private bool isTextBackgroundFill = false;
        private bool isTextFontFamilyOpen = false;
        private bool isTextFontSizeOpen = false;
        private bool isFunctionSelected = false;
        private bool isStyleWidthOpen = false;
        private bool isStyleStrokeOpen = false;
        private bool isToolEraseOpen = false;
        private bool isDrawing = false;
        private bool isShiftDown = false;

        //Lưu giữ điểm bắt đầu và kết thúc của nét vẽ
        Point startPoint;
        Point endPoint;

        //List border giúp xác định các border khi người dùng chọn vào các function
        private List<Border> function = new List<Border>();

        //List fonts lưu giữ các kiểu fonts
        private List<Font> fonts = new List<Font>();

        //Các biến global lưu giữ các thông số của ứng dụng
        private string globalFontFamily;
        private int globalFontSize = 12;
        private int globalStroke;
        private IShape selectedShape = null;

        //List lưu giữ tất cả các loại hình vẽ được load từ file dll (bao gồm các hình vẽ + phiên bản ấn shift của chúng)
        private List<IShape> allShapeList = new List<IShape>();

        //Các list lưu trữ các data được load từ file dll
        private List<IShape> shapeList = new List<IShape>();
        private List<IWidthness> widthnessList = new List<IWidthness>();
        private List<IStroke> strokeList = new List<IStroke>();

        //List drawSurface giúp lưu trữ các nét vẽ trên 1 bề mặt
        private List<IShape> drawSurface = new List<IShape>();

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
            styleWidthCombobox.SelectedIndex = 0;
            styleStrokeCombobox.SelectedIndex = 0;

            textFontCombobox.MaxDropDownHeight = 160;

            function.Add(selectionBorder);
            function.Add(textBorder);
            function.Add(toolEraseBorder);
            function.Add(toolMouseBorder);

            string folder = AppDomain.CurrentDomain.BaseDirectory;
            var fis = new DirectoryInfo(folder).GetFiles("*.dll");

            foreach (var fi in fis)
            {
                var assembly = Assembly.LoadFrom(fi.FullName);
                var types = assembly.GetTypes();

                foreach (var type in types)
                {
                    if ((type.IsClass) && (typeof(IShape).IsAssignableFrom(type)))
                    {
                        if (!type.Name.Contains("Shift"))
                        {
                            shapeList.Add((IShape)Activator.CreateInstance(type)!);
                        }

                        allShapeList.Add((IShape)Activator.CreateInstance(type)!);
                    }

                    if ((type.IsClass) && (typeof(IWidthness).IsAssignableFrom(type)))
                    {
                        widthnessList.Add((IWidthness)Activator.CreateInstance(type)!);
                    }

                    if ((type.IsClass) && (typeof(IStroke).IsAssignableFrom(type)))
                    {
                        strokeList.Add((IStroke)Activator.CreateInstance(type)!);
                    }
                }
            }

            shapeListview.ItemsSource = shapeList;
            styleWidthCombobox.ItemsSource = widthnessList;
            styleStrokeCombobox.ItemsSource = strokeList;
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
            shapeListview.SelectedItem = null;
            selectedShape = null;
            function[index].Opacity = 1;

            for (int i = 0; i < function.Count; i++)
            {
                if (i != index && function[i].Opacity != 0)
                {
                    function[i].Opacity = 0;
                }
            }

            if (index == 2)
            {
                isToolEraseOpen = true;
            } else
            {
                isToolEraseOpen = false;
            }

            if (index == 3)
            {
                isFunctionSelected = false;
            } else
            {
                isFunctionSelected = true;
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
                isTextFontFamilyOpen = false;

                fontSizeStackpanel.Visibility = Visibility.Collapsed;
                isTextFontSizeOpen = false;

                styleWidthCombobox.IsDropDownOpen = false;
                isStyleWidthOpen = false;

                styleStrokeCombobox.IsDropDownOpen = false;
                isStyleStrokeOpen = false;
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

                styleStrokeCombobox.IsDropDownOpen = false;
                isStyleStrokeOpen = false;
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

                styleStrokeCombobox.IsDropDownOpen = false;
                isStyleStrokeOpen = false;
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

                styleStrokeCombobox.IsDropDownOpen = false;
                isStyleStrokeOpen = false;
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
                    styleWidthImage.Source = new BitmapImage(new Uri("images/styleBaseLine.png", UriKind.Relative));
                    break;
                case 1:
                    styleWidthImage.Source = new BitmapImage(new Uri("images/styleWidth1.png", UriKind.Relative));
                    break;
                case 2:
                    styleWidthImage.Source = new BitmapImage(new Uri("images/styleWidth2.png", UriKind.Relative));
                    break;
                case 3:
                    styleWidthImage.Source = new BitmapImage(new Uri("images/styleWidth3.png", UriKind.Relative));
                    break;
                default: break;
            }
        }

        private void styleWidthComboboxPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isStyleWidthOpen = false;
        }

        private void styleStrokeButtonClick(object sender, RoutedEventArgs e)
        {
            if (!isStyleStrokeOpen)
            {
                styleStrokeCombobox.IsDropDownOpen = true;
                isStyleStrokeOpen = true;

                selectionCombobox.IsDropDownOpen = false;
                selectionButtonContent.Source = new BitmapImage(new Uri("images/arrow-down.png", UriKind.Relative));
                isSelectionOpen = false;

                textFontCombobox.IsDropDownOpen = false;
                isTextFontFamilyOpen = false;

                fontSizeStackpanel.Visibility = Visibility.Collapsed;
                isTextFontSizeOpen = false;

                styleWidthCombobox.IsDropDownOpen = false;
                isStyleWidthOpen = false;
            } else
            {
                styleStrokeCombobox.IsDropDownOpen = false;
                isStyleStrokeOpen = false;
            }
        }

        private void styleStrokeComboboxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = styleStrokeCombobox.SelectedIndex;

            switch (index)
            {
                case 0:
                    globalStroke = 1;
                    styleStrokeImage.Source = new BitmapImage(new Uri("images/styleBaseLine.png", UriKind.Relative));
                    break;
                case 1:
                    globalStroke = 2;
                    styleStrokeImage.Source = new BitmapImage(new Uri("images/styleStroke1.png", UriKind.Relative));
                    break;
                case 2:
                    globalStroke = 3;
                    styleStrokeImage.Source = new BitmapImage(new Uri("images/styleStroke2.png", UriKind.Relative));
                    break;
                case 3:
                    globalStroke = 4;
                    styleStrokeImage.Source = new BitmapImage(new Uri("images/styleStroke3.png", UriKind.Relative));
                    break;
                default: break;
            }
        }

        private void styleStrokeComboboxPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isStyleStrokeOpen = false;
        }

        private void toolEraseButtonClick(object sender, RoutedEventArgs e)
        {
            functionSelected(2);
        }

        private void toolMouseButtonClick(object sender, RoutedEventArgs e)
        {
            functionSelected(3);
        }

        private void removeAllFunctionSelectedToSelectShape()
        {
            for (int i = 0; i < function.Count; i++)
            {
                if (function[i].Opacity != 0)
                {
                    function[i].Opacity = 0;
                }
            }
        }

        private void shapeListviewPreviewMouseButtonDown(object sender, MouseButtonEventArgs e)
        {
            removeAllFunctionSelectedToSelectShape();
            isFunctionSelected = true;
            isToolEraseOpen = false;
            ListViewItem selectedItem = (ListViewItem)sender;
            shapeListview.SelectedItem = selectedItem;

            selectedShape = (IShape)selectedItem.Content;
        }

        private void drawAreaMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDrawing = false;
            if (selectedShape != null)
            {
                drawSurface.Add((IShape)selectedShape.Clone());
            }
        }

        private void drawAreaMouseMove(object sender, MouseEventArgs e)
        {
            if (isFunctionSelected && !isToolEraseOpen)
            {
                drawBackGround.Cursor = Cursors.Cross;
            }
            else if (isFunctionSelected && isToolEraseOpen)
            {
                drawBackGround.Cursor = new Cursor(new MemoryStream(Properties.Resources.toolErase));
            }
            else
            {
                drawBackGround.Cursor = null;
            }

            if (isDrawing && selectedShape != null)
            {
                endPoint = e.GetPosition(drawArea);
                drawArea.Children.Clear();
                selectedShape.addWidthness((IWidthness)styleWidthCombobox.SelectedItem);

                foreach (var item in drawSurface)
                {
                    drawArea.Children.Add(item.convertShapeType());
                }

                selectedShape.addStartPoint(startPoint);
                selectedShape.addEndPoint(endPoint);

                drawArea.Children.Add(selectedShape.convertShapeType());
            }
        }

        private void drawAreaMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            selectionCombobox.IsDropDownOpen = false;
            selectionButtonContent.Source = new BitmapImage(new Uri("images/arrow-down.png", UriKind.Relative));
            isSelectionOpen = false;

            textFontCombobox.IsDropDownOpen = false;
            isTextFontFamilyOpen = false;

            fontSizeStackpanel.Visibility = Visibility.Collapsed;
            isTextFontSizeOpen = false;

            styleWidthCombobox.IsDropDownOpen = false;
            isStyleWidthOpen = false;

            styleStrokeCombobox.IsDropDownOpen = false;
            isStyleStrokeOpen = false;

            isDrawing = true;
            startPoint = e.GetPosition(drawArea);
        }

        private void WindowKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftShift || e.Key == Key.RightShift) 
            { 
                isShiftDown = true;
            }
        }

        private void WindowKeyUp(object sender, KeyEventArgs e)
        {
            isShiftDown = false;
        }

        private void WindowMouseLeave(object sender, MouseEventArgs e)
        {
            isDrawing = false;
            if (selectedShape != null)
            {
                drawSurface.Add((IShape)selectedShape.Clone());
            }
        }

        private void toolBarMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isDrawing)
            {
                isDrawing = false;
                if (selectedShape != null)
                {
                    drawSurface.Add((IShape)selectedShape.Clone());
                }
            }
        }
    }
}