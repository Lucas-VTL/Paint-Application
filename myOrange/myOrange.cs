using myColor;

namespace myOrange
{
    public class myOrange : IColor
    {
        public string colorName => "Orange";
        public byte colorRed => 255;
        public byte colorGreen => 165;
        public byte colorBlue => 0;
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
