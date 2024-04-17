using myColor;

namespace myBlack
{
    public class myBlack : IColor
    {
        public string colorName => "Black";
        public byte colorRed => 0;
        public byte colorGreen => 0;
        public byte colorBlue => 0;
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
