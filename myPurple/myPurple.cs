using myColor;

namespace myPurple
{
    public class myPurple : IColor
    {
        public string colorName => "Purple";
        public byte colorRed => 128;
        public byte colorGreen => 0;
        public byte colorBlue => 128;
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
