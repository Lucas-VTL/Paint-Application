using myColor;

namespace myYellow
{
    public class myYellow : IColor
    {
        public string colorName => "Yellow";
        public byte colorRed => 255;
        public byte colorGreen => 255;
        public byte colorBlue => 0;
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
