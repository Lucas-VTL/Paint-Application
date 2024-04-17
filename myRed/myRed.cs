using myColor;

namespace myRed
{
    public class myRed : IColor
    {
        public string colorName => "Red";
        public byte colorRed => 255;
        public byte colorGreen => 0;
        public byte colorBlue => 0;
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
