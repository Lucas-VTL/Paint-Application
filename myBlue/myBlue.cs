﻿using myColor;
using System.Windows.Media;

namespace myBlue
{
    public class myBlue : IColor
    {
        public string colorName => "Blue";
        public SolidColorBrush colorValue => new SolidColorBrush(Color.FromRgb(0, 0, 255));
        public void addColorRGB(byte r, byte g, byte b) { }
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
