using myColor;

namespace myBlue
{
    public class myBlue : IColor
    {
        public string colorName => "Blue";
        public byte colorRed => 0;
        public byte colorGreen => 0;
        public byte colorBlue => 255;
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
