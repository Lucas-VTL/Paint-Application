using myColor;

namespace myBrown
{
    public class myBrown : IColor
    {
        public string colorName => "Brown";
        public byte colorRed => 165;
        public byte colorGreen => 42;
        public byte colorBlue => 42;
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
