﻿using myColor;
using System.Windows.Media;

namespace myBlack
{
    public class myBlack : IColor
    {
        public string colorName => "Black";
        public SolidColorBrush colorValue => new SolidColorBrush(Color.FromRgb(0, 0, 0));
        public void addColorRGB(byte r, byte g, byte b) {}
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
