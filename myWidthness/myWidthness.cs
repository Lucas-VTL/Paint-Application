using System.Windows;

namespace myWidthness
{ 
    public interface IWidthness : ICloneable
    {
        string widthnessName { get; }
        string widthnessImage { get; }
        Thickness widthnessValue { get; }
    }
}
