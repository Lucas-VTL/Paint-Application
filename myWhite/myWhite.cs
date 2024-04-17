using myColor;

namespace myWhite
{
    public class myWhite : IColor
    {
        public string colorName => "White";
        public byte colorRed => 255;
        public byte colorGreen => 255;
        public byte colorBlue => 255;
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
