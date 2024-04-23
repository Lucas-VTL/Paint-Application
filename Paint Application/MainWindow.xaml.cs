using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using System.Reflection;
using myShape;
using myWidthness;
using myStroke;
using myColor;

using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;
using Cursors = System.Windows.Input.Cursors;
using Cursor = System.Windows.Input.Cursor;
using ListViewItem = System.Windows.Controls.ListViewItem;
using Point = System.Windows.Point;
using System.Diagnostics;
using System.Windows.Shapes;
using Brushes = System.Windows.Media.Brushes;

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
        private bool isShapeFill = false;

        //Lưu giữ điểm bắt đầu và kết thúc của nét vẽ
        Point startPoint;
        Point endPoint;
        IColor customColor;
        IShape freeLine;

        //List border giúp xác định các border khi người dùng chọn vào các function
        private List<Border> function = new List<Border>();

        //List fonts lưu giữ các kiểu fonts
        private List<Font> fonts = new List<Font>();

        //Các biến global lưu giữ các thông số của ứng dụng
        private string globalFontFamily;
        private int globalFontSize = 12;
        private IShape selectedShape = null;
        private IColor selectedColor = null;

        //List lưu giữ tất cả các loại hình vẽ được load từ file dll (bao gồm các hình vẽ + phiên bản ấn shift của chúng)
        private List<IShape> allShapeList = new List<IShape>();

        //Các list lưu trữ các data được load từ file dll
        private List<IShape> shapeList = new List<IShape>();
        private List<IWidthness> widthnessList = new List<IWidthness>();
        private List<IStroke> strokeList = new List<IStroke>();
        private List<IColor> colorList = new List<IColor>();

        //List drawSurface giúp lưu trữ các nét vẽ trên 1 bề mặt
        private List<IShape> drawSurface = new List<IShape>();

        //List recoverList giúp lưu trữ lại các nét vẽ đã bị xóa hoặc undo
        private List<IShape> recoverList = new List<IShape>();

        //List eraseList giúp lưu giữ các hình vẽ bị xóa
        private List<Point> eraseList = new List<Point>();

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
                        if (!type.Name.Contains("Shift") && !type.Name.Equals("myFreeLine"))
                        {
                            shapeList.Add((IShape)Activator.CreateInstance(type)!);
                        }

                        if (type.Name.Equals("myFreeLine"))
                        {
                            freeLine = (IShape)Activator.CreateInstance(type)!;
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

                    if ((type.IsClass) && (typeof(IColor).IsAssignableFrom(type)))
                    {
                        if (!type.Name.Equals("myCustomColor"))
                        {
                            colorList.Add((IColor)Activator.CreateInstance(type)!);
                        } else
                        {
                            customColor = (IColor)Activator.CreateInstance(type)!;
                        }
                    }
                }
            }

            shapeListview.ItemsSource = shapeList;
            styleWidthCombobox.ItemsSource = widthnessList;
            styleStrokeCombobox.ItemsSource = strokeList;
            colorListview.ItemsSource = colorList;

            selectedColor = colorList[0];
            colorListview.SelectedIndex = 0;
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
            IWidthness selectedWidthness = (IWidthness)styleWidthCombobox.SelectedItem;
            styleWidthImage.Source = new BitmapImage(new Uri(selectedWidthness.widthnessImage, UriKind.Relative));
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
            IStroke selectedStroke = (IStroke)styleStrokeCombobox.SelectedItem;
            styleStrokeImage.Source = new BitmapImage(new Uri(selectedStroke.strokeImage, UriKind.Relative));
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
                toolUndoButton.Opacity = 1;
                toolRedoButton.Opacity = 0.3;
                recoverList.Clear();
            }

            if (selectedShape == null && isToolEraseOpen)
            {
                selectedShape = freeLine;
                IShape newFreeLine = (IShape)selectedShape.Clone();
                newFreeLine.addPointList(eraseList);

                if (!checkIfDrawSurfaceEmpty(drawSurface))
                {
                    drawSurface.Add(newFreeLine);

                    toolUndoButton.Opacity = 1;
                    toolRedoButton.Opacity = 0.3;
                    recoverList.Clear();
                } else
                {
                    toolUndoButton.Opacity = 0.3;
                    toolRedoButton.Opacity = 0.3;
                    recoverList.Clear();
                }

                eraseList.Clear();
                selectedShape = null;
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

                foreach (var item in drawSurface)
                {
                    drawArea.Children.Add(item.convertShapeType());
                }

                if (isShiftDown)
                {
                    if (!selectedShape.shapeName.Contains("Shift"))
                    {
                        for (int i = 0; i < allShapeList.Count; i++)
                        {
                            string shiftShapeName = "Shift" + selectedShape.shapeName;
                            if (allShapeList[i].shapeName.Equals(shiftShapeName))
                            {
                                selectedShape = allShapeList[i];
                                break;
                            }
                        }
                    }
                } else
                {
                    if (selectedShape.shapeName.Contains("Shift"))
                    {
                        for (int i = 0; i < allShapeList.Count; i++)
                        {
                            string[] shapeNameSplit = selectedShape.shapeName.Split("Shift");
                            string shapeName = shapeNameSplit[1];
                            if (allShapeList[i].shapeName.Equals(shapeName))
                            {
                                selectedShape = allShapeList[i];
                                break;
                            }
                        }
                    }
                }

                selectedShape.addStartPoint(startPoint);
                selectedShape.addEndPoint(endPoint);
                selectedShape.addWidthness((IWidthness)styleWidthCombobox.SelectedItem);
                selectedShape.addStrokeStyle((IStroke)styleStrokeCombobox.SelectedItem);
                selectedShape.addColor(selectedColor);
                selectedShape.setShapeFill(isShapeFill);

                drawArea.Children.Add(selectedShape.convertShapeType());
            }

            if (isDrawing && selectedShape == null && isToolEraseOpen) 
            {
                Point point = e.GetPosition(drawArea);
                eraseList.Add(point);
                
                 Ellipse dot = new Ellipse();
                 dot.Fill = Brushes.White;
                 dot.Width = dot.Height = 20;
                 Canvas.SetLeft(dot, point.X);
                if (point.Y >= 20) 
                {
                    Canvas.SetTop(dot, point.Y - 20);
                }

                 drawArea.Children.Add(dot);
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

            if (selectedShape == null && isToolEraseOpen)
            {
                eraseList.Add(startPoint);

                Ellipse dot = new Ellipse();
                dot.Fill = Brushes.White;
                dot.Width = dot.Height = 20;
                Canvas.SetLeft(dot, startPoint.X);
                if (startPoint.Y >= 20)
                {
                    Canvas.SetTop(dot, startPoint.Y - 20);
                }

                drawArea.Children.Add(dot);
            }
        }

        private void WindowKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftShift || e.Key == Key.RightShift) 
            { 
                isShiftDown = true;
            }

            if ((e.Key == Key.Z) && (System.Windows.Forms.Control.ModifierKeys & Keys.Control) == Keys.Control) 
            {
                if (toolUndoButton.Opacity != 0.3)
                {
                    recoverList.Add(drawSurface[drawSurface.Count - 1]);
                    drawSurface.RemoveAt(drawSurface.Count - 1);

                    drawArea.Children.Clear();

                    foreach (var item in drawSurface)
                    {
                        drawArea.Children.Add(item.convertShapeType());
                    }

                    if (drawSurface.Count == 0)
                    {
                        toolUndoButton.Opacity = 0.3;
                    }

                    toolRedoButton.Opacity = 1;
                }
            }

            if ((e.Key == Key.Y) && (System.Windows.Forms.Control.ModifierKeys & Keys.Control) == Keys.Control)
            {
                if (toolRedoButton.Opacity != 0.3)
                {
                    drawSurface.Add(recoverList[recoverList.Count - 1]);
                    recoverList.RemoveAt(recoverList.Count - 1);

                    drawArea.Children.Clear();

                    foreach (var item in drawSurface)
                    {
                        drawArea.Children.Add(item.convertShapeType());
                    }

                    if (recoverList.Count == 0)
                    {
                        toolRedoButton.Opacity = 0.3;
                    }

                    toolUndoButton.Opacity = 1;
                }
            }
        }

        private void WindowKeyUp(object sender, KeyEventArgs e)
        {
            isShiftDown = false;
        }

        private void WindowMouseLeave(object sender, MouseEventArgs e)
        {
            if (isDrawing) {
                isDrawing = false;
                if (selectedShape != null)
                {
                    drawSurface.Add((IShape)selectedShape.Clone());
                    toolUndoButton.Opacity = 1;
                    toolRedoButton.Opacity = 0.3;
                    recoverList.Clear();
                }
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
                    toolUndoButton.Opacity = 1;
                    toolRedoButton.Opacity = 0.3;
                    recoverList.Clear();
                }
            }
        }

        private void colorListviewPreviewMouseButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListViewItem selectedItem = (ListViewItem)sender;
            colorListview.SelectedItem = selectedItem;

            selectedColor = (IColor)selectedItem.Content;
        }

        private void styleFillButtonClick(object sender, RoutedEventArgs e)
        {
            if (isShapeFill)
            {
                styleFillButton.Opacity = 0.3;
                isShapeFill = false;
            } else
            {
                styleFillButton.Opacity = 1;
                isShapeFill = true;
            }
        }

        private void customColorButtonClick(object sender, RoutedEventArgs e)
        {
            selectedColor = customColor;
            ColorDialog colorDialog = new ColorDialog();

            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.Drawing.Color color = colorDialog.Color;

                byte colorRed = color.R;
                byte colorGreen = color.G;
                byte colorBlue = color.B;
            
                if (!checkExistedColor(colorRed, colorGreen, colorBlue))
                {
                    IColor newCustomColor = (IColor)selectedColor.Clone();

                    newCustomColor.addColorRGB(colorRed, colorGreen, colorBlue);
                    colorList.Add(newCustomColor);

                    colorListview.ItemsSource = "";
                    colorListview.ItemsSource = colorList;

                    selectedColor = colorList[colorList.Count - 1];
                    colorListview.SelectedIndex = colorList.Count - 1;

                    colorListview.ScrollIntoView(colorListview.Items[colorListview.Items.Count - 1]);
                } else
                {
                    int index = getExistedColorByRGB(colorRed, colorGreen, colorBlue);
                    if (index != -1)
                    {
                        selectedColor = colorList[index];
                        colorListview.SelectedIndex = index;
                        colorListview.ScrollIntoView(colorListview.Items[index]);
                    }
                }
            }
        }

        private bool checkExistedColor(byte r, byte g, byte b)
        {
            for (int i = 0; i < colorList.Count; i++)
            {
                SolidColorBrush colorBrush = colorList[i].colorValue;
                System.Windows.Media.Color color = colorBrush.Color;

                byte colorRed = color.R;
                byte colorGreen = color.G;
                byte colorBlue = color.B;

                if (colorRed == r && colorGreen == g && colorBlue == b)
                {
                    return true;
                }
            }
            return false;
        }

        private int getExistedColorByRGB(byte r, byte g, byte b) 
        {
            for (int i = 0; i < colorList.Count; i++)
            {
                SolidColorBrush colorBrush = colorList[i].colorValue;
                System.Windows.Media.Color color = colorBrush.Color;

                byte colorRed = color.R;
                byte colorGreen = color.G;
                byte colorBlue = color.B;

                if (colorRed == r && colorGreen == g && colorBlue == b)
                {
                    return i;
                }
            }
            return -1;
        }

        private void toolUndoButtonMouseEnter(object sender, MouseEventArgs e)
        {
            if (toolUndoButton.Opacity == 0.3)
            {
                toolUndoButton.Cursor = null;
            } else
            {
                toolUndoButton.Cursor = Cursors.Hand;
            }
        }

        private void toolUndoButtonMouseLeave(object sender, MouseEventArgs e)
        {
            toolUndoButton.Cursor = null;
        }

        private void toolRedoButtonMouseEnter(object sender, MouseEventArgs e)
        {
            if (toolRedoButton.Opacity == 0.3)
            {
                toolRedoButton.Cursor = null;
            }
            else
            {
                toolRedoButton.Cursor = Cursors.Hand;
            }
        }

        private void toolRedoButtonMouseLeave(object sender, MouseEventArgs e)
        {
            toolRedoButton.Cursor = null;
        }

        private void toolUndoButtonClick(object sender, RoutedEventArgs e)
        {
            if (toolUndoButton.Opacity != 0.3)
            {
                recoverList.Add(drawSurface[drawSurface.Count - 1]);
                drawSurface.RemoveAt(drawSurface.Count - 1);

                drawArea.Children.Clear();

                foreach (var item in drawSurface)
                {
                    drawArea.Children.Add(item.convertShapeType());
                }

                if (checkIfDrawSurfaceEmpty(drawSurface))
                {
                    toolUndoButton.Opacity = 0.3;
                }

                toolRedoButton.Opacity = 1;
            }
        }

        private void toolRedoButtonClick(object sender, RoutedEventArgs e)
        {
            if (toolRedoButton.Opacity != 0.3)
            {
                drawSurface.Add(recoverList[recoverList.Count - 1]);
                recoverList.RemoveAt(recoverList.Count - 1);

                drawArea.Children.Clear();

                foreach (var item in drawSurface)
                {
                    drawArea.Children.Add(item.convertShapeType());
                }

                if (recoverList.Count == 0)
                {
                    toolRedoButton.Opacity = 0.3;
                }

                toolUndoButton.Opacity = 1;
            }
        }

        private bool checkIfDrawSurfaceEmpty(List<IShape> surface)
        {
            if (surface.Count == 0)
            {
                return true;
            }

            foreach (IShape shape in surface)
            {
                if (shape.shapeName != "Free Line")
                {
                    return false;
                }
            }

            return true;
        }
    }
}