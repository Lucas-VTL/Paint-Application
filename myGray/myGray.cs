using myColor;

namespace myGray
{
    public class myGray : IColor
    {
        public string colorName => "Gray";
        public byte colorRed => 128;
        public byte colorGreen => 128;
        public byte colorBlue => 128;
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
