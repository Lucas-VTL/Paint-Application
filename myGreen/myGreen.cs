using myColor;

namespace myGreen
{
    public class myGreen : IColor
    {
        public string colorName => "Green";
        public byte colorRed => 0;
        public byte colorGreen => 128;
        public byte colorBlue => 0;
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
